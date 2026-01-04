using OrderService.Infrastructure;
using OrderService.Application;
using OrderService.API.Middlewares;


var builder = WebApplication.CreateBuilder(args);
//DI orchestration
builder.Services.AddInfrastructure(builder.Configuration);
// Controllers
builder.Services.AddControllers();

// Application layer (MediatR)
builder.Services.AddApplication();

// Infrastructure layer (DbContext, Repositories)
builder.Services.AddInfrastructure(builder.Configuration);
// Optional: CORS (useful later for gateway)
builder.Services.AddCors(options =>
{
    options.AddPolicy("default", policy =>
        policy.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod());
});

var app = builder.Build();

app.UseMiddleware<GlobalExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseCors("default");

app.UseAuthorization();

app.MapControllers();



app.Run();
