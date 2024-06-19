using Microsoft.Extensions.Configuration;

var builder = DistributedApplication.CreateBuilder(args);

var sqlPassword = builder.AddParameter("sql-password", secret: true);

var sql = builder
    .AddSqlServer("marlohnSqlServer", password: sqlPassword, port: 58396)
    .WithDataVolume("AppHost-MSSQL-data")
    .AddDatabase("marlohnDb");

var apiService = builder.AddProject<Projects.PetAdoptionSystem_ApiService>("apiservice");

var api = builder.AddProject<Projects.PetAdoptionSystem_Api>("api")
    .WithReference(sql)
    .WithEnvironment("ConnectionStrings:marlohnDb", builder.Configuration.GetConnectionString("marlohnDb"));

builder.AddProject<Projects.PetAdoptionSystem_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(apiService);

builder.Build().Run();
