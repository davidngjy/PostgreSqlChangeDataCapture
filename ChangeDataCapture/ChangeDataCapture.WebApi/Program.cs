using ChangeDataCapture.WebApi.Persistence;
using ChangeDataCapture.WebApi.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var serviceCollection = builder.Services;
var configuration = builder.Configuration;

serviceCollection.AddControllers();
serviceCollection.AddEndpointsApiExplorer();
serviceCollection.AddSwaggerGen();

serviceCollection.AddHostedService<DataCaptureService>();

serviceCollection.AddDbContext<ExampleDbContext>(
    opt => opt.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();
