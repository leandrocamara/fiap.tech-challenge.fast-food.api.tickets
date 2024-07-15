using Adapters.Extensions;
using API.HealthChecks;
using Application.Extensions;
using External.Extensions;

var builder = WebApplication.CreateBuilder(args);

#region Configure Services

var configuration = builder.Configuration;

builder.Services.AddCustomHealthChecks(configuration);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => {
    c.EnableAnnotations();
});
builder.Services.AddControllers();

builder.Services.AddExternalDependencies(configuration);
builder.Services.AddAdaptersDependencies();
builder.Services.AddApplicationDependencies();

#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    using (var scope = app.Services.CreateScope())
        app.CreateDatabase(scope.ServiceProvider);
    
}

app.UseCustomHealthChecks();
app.UseHttpsRedirection();
app.MapControllers();

app.Run();