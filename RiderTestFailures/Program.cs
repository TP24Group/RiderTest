using Carter;
using RiderTestFailures.Configuration;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.ConfigureApi();

app.Run();