using Microsoft.EntityFrameworkCore;
using MonitoraggioGestoriBE.Data;

var builder = WebApplication.CreateBuilder(args);

// ✅ Aggiungi servizi
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<UtilsService>();


// ✅ Configura il DbContext Oracle
var connectionString = builder.Configuration.GetConnectionString("FlussiFinDB");
builder.Services.AddDbContext<FlussiFinContext>(options =>
    options.UseLazyLoadingProxies()
        .UseOracle(connectionString));

// ✅ CONFIGURA CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:4200") // Sostituisci con l'URL corretto
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

// ✅ Configura la pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// ✅ ATTIVA CORS
app.UseCors("AllowFrontend");

app.UseAuthorization();

app.MapControllers(); // ✅ Mappa i controller

app.Run();