var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<PackageStorageService>();
builder.Services.AddControllers();

var app = builder.Build();
app.MapControllers();
app.Run();
