using CarmenSchool.Infrastructure;
using CarmenSchool.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRouting(options =>
{
  options.LowercaseQueryStrings = true;
  options.LowercaseUrls = true;
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddInfrastructureLayer(builder.Configuration);
builder.Services.AddServicesLayer();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
