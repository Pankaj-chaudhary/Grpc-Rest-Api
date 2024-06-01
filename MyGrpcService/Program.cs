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

builder.Services.AddGrpc().AddJsonTranscoding(); 
builder.Services.AddGrpcSwagger();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1",
        new Microsoft.OpenApi.Models.OpenApiInfo { Title = "gRPC Api via transcoding", Version = "v1" });

    var filePath = Path.Combine(System.AppContext.BaseDirectory, "MyGrpcService.xml");
    c.IncludeXmlComments(filePath);
    c.IncludeGrpcXmlComments(filePath, includeControllerXmlComments: true);
});
var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
if (app.Environment.IsDevelopment())
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My Grpc Transcoding API V1");
    });


app.MapGrpcService<CategoryGrpcService>();

app.Run();
