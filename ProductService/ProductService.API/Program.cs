using ProductService.Infrastructure;
using ProductService.Application;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
// Controllers
builder.Services.AddControllers();

// Application layer (MediatR)
builder.Services.AddApplication();

// Infrastructure layer (DbContext, repositories)
builder.Services.AddInfrastructure(builder.Configuration);


var app = builder.Build();

app.UseHttpsRedirection();
app.MapControllers();


app.Run();
