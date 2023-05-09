<h1 align="left">
    SimpleCustomerAPI
</h1>

<h2>
    Introduction
</h2>
A simple Customer API to manage customers and notes about customers.

<h2>
Used technologies and approaches
</h2>
    <ul>
        <li>C# / .NET Core</li>
        <li>CQRS and MediatR with Clean Architercture</li>
        <li>SQL lite & Dapper</li>
        <li>CRUD API & Swagger</li>
    </ul>

<h2>
    Running the Solution
</h2>


<div>

    # Clone to repository
    $ git clone https://github.com/Pixilator-85/SimpleCustomerAPI.git
    
    # Go to the folder you cloned
    $ cd SimpleCustomerAPI
    
    # Install dependencies
    $ dotnet restore
    
    # Run the application from the WebApi layer
 </div>

<h2>
Architecture
</h2>

<p>
    Clean Architecture. 
    It's a layered solution splitted into five layers:
</p>

<ul>
    <li>Domain</li>
    <li>Application</li>
    <li>Infrastructure</li>
    <li>Presentation</li>
    <li>WebApi (entry point)</li>
</ul>

Each layer presented as one separate project in the solution.

<h3>
    Domain:
</h3>

This layer contains all entities, enums, exceptions, interfaces, types and logic specific to the domain layer.

<h3>
    Application:
</h3>

Orchestrate the application. This layer contains all application logic.
It has dependency on the domain layer, but has no dependencies on any other layer or project.
This layer defines interfaces that are implemented by outside layers and DTOs.
For example, if the application need to access a notification service, a new interface would be added to application and an implementation would be created within infrastructure.
Also contains MediatR frameworks and logic for work with them.

<h3>
    Infrastructure:
</h3>

This layer can contain classes for accessing external resources such as:

<ul>
    <li>Databases - PostgreSQL, MongoDB, ClickHouse, etc</li>
    <li>Identity providers - Auth0, Keycloak</li>
    <li>Emails providers</li>
    <li>Storage services - AWS S3, Azure Blob Storage</li>
    <li>Message queues - Kafka or Rabbit MQ </li>
</ul>

These classes implement all interfaces defined within the application layer.

<h3>
    Presentation:
</h3>

The most important part of the layer is the Controllers, which define the API endpoints in solution.
Controllers

<h3>
    WebApi:
</h3>    

It's entry point of solution and contains service registration and configs in appsettings.json.

<h3>
    Tests:
</h3>

Solution contains set of architecture tests that allows to control dependencies between layers, Handlers and controllers.

<h3>
    P.S:
</h3>

<p>
    If I will have more time I'm going to add some FluentValidation rules to entities in Application layer and also going to add some archtecture and logic unit tests.
</p>