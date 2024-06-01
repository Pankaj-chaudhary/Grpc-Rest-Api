using Microsoft.EntityFrameworkCore;
using MyGrpcService.Persistence;
using MyGrpcService.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddGrpc(); 
var conn = builder.Configuration.GetConnectionString("DbConnection");

// Register AppDbContext
builder.Services.AddDbContext<AppDbContext>(db => db.UseSqlServer(conn));
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// builder.Services.AddGrpc().AddJsonTranscoding();
var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<CategoryGrpcService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
