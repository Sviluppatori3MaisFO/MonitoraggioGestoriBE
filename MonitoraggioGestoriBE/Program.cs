using Microsoft.EntityFrameworkCore;
using MonitoraggioGestoriBE.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<UtilsService>();


//  Configura il DbContext Oracle
var connectionString = builder.Configuration.GetConnectionString("FlussiFinDB");
/*
builder.Services.AddDbContext<FlussiFinContext>(options =>
    options.UseLazyLoadingProxies()
        .UseOracle(connectionString));*/
builder.Services.AddDbContext<FlussiFinContext>(opt =>
    opt.UseOracle(connectionString, oracleOptionsAction => oracleOptionsAction.UseOracleSQLCompatibility(OracleSQLCompatibility.DatabaseVersion19)));

builder.Services.AddCors(o => o.AddPolicy("AllowFrontend", builderCors =>
{
    builderCors.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader();
}));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseCors("AllowFrontend");

app.UseAuthorization();

app.MapControllers();

app.Run();