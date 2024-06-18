var builder = DistributedApplication.CreateBuilder(args);

var apiService = builder.AddProject<Projects.PetAdoptionSystem_ApiService>("apiservice");

var api = builder.AddProject<Projects.PetAdoptionSystem_Api>("api");

builder.AddProject<Projects.PetAdoptionSystem_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(apiService);

builder.Build().Run();
