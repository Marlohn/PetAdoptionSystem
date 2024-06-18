var builder = DistributedApplication.CreateBuilder(args);

var apiService = builder.AddProject<Projects.PetAdoptionSystem_ApiService>("apiservice");

builder.AddProject<Projects.PetAdoptionSystem_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(apiService);

builder.Build().Run();
