using SimpleAPI.Domain.Repositories;
using SimpleAPI.Infrastructure.Persistence;
using SimpleAPI.Infrastructure.Persistence.Handlers;
using SimpleAPI.Infrastructure.Persistence.Repositories;
using Microsoft.OpenApi.Models;

const string ApiTile = "SimpleAPI";
const string SwaggerApiVersion = "v1";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(swaggerOptions =>
{
    swaggerOptions.SwaggerDoc(SwaggerApiVersion,
        new OpenApiInfo
        {
            Title = ApiTile,
            Version = SwaggerApiVersion
        });
    swaggerOptions.DescribeAllParametersInCamelCase();
    swaggerOptions.UseAllOfForInheritance();
});

builder.Services.AddControllers().AddApplicationPart(typeof(SimpleAPI.Presentation.AssemblyReference).Assembly);

builder.Services.AddMediatR(cfg=>
    cfg.RegisterServicesFromAssemblies(SimpleAPI.Application.AssemblyReference.Assembly));

builder.Services.AddSingleton<ICustomerRepository, CustomerRepository>();
builder.Services.AddSingleton<INoteRepository, NoteRepository>();
builder.Services.AddSingleton<DbContext>();

Dapper.SqlMapper.AddTypeHandler(new SQLiteGuidTypeHandler());
Dapper.SqlMapper.RemoveTypeMap(typeof(Guid));
Dapper.SqlMapper.RemoveTypeMap(typeof(Guid?));

var app = builder.Build();

using var scope = app.Services.CreateScope();
var context = scope.ServiceProvider.GetRequiredService<DbContext>();
await context.InitDataBase();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.DefaultModelsExpandDepth(-1);
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();