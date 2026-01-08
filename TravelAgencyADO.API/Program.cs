using TravelAgencyADO.BLL.Services.Implementations;
using TravelAgencyADO.BLL.Services.Interfaces;
using TravelAgencyADO.DAL.Repositories;
using TravelAgencyADO.Domain.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// DAL + BLL
var cs = builder.Configuration.GetConnectionString("DefaultConnection")
         ?? throw new InvalidOperationException("Missing DefaultConnection");

builder.Services.AddScoped<IActivityRepo>(_ => new SqlActivityRepo(cs));
builder.Services.AddScoped<IBookingRepo>(_ => new SqlBookingRepo(cs));
builder.Services.AddScoped<IDestinationRepo>(_ => new SqlDestinationRepo(cs));
builder.Services.AddScoped<IActivityService, ActivityService>();
builder.Services.AddScoped<IBookingService, BookingService>();
builder.Services.AddScoped<IDestinationService, DestinationService>();

// ==============================
// CORS (Cross-Origin Resource Sharing)
// ==============================
//
// Le CORS est un mécanisme de sécurité APPLIQUÉ PAR LE NAVIGATEUR.
// Il empêche une page web (HTML/JS) hébergée sur une origine
// (schéma + domaine + port) d'appeler une API située sur une autre
// origine, sauf si le serveur l'autorise explicitement.
//
// Exemple :
// - MVC (navigateur) : http://localhost:5188
// - Web API          : http://localhost:5062
// → origines différentes ⇒ CORS bloqué par défaut
//
// Ce middleware indique AU NAVIGATEUR quelles origines
// ont le droit d'appeler l'API.
//

builder.Services.AddCors(opt =>
{
    // Déclaration d'une policy CORS nommée "MvcCors"
    opt.AddPolicy("MvcCors", p =>
    {
        // Origines autorisées à appeler l'API
        // Une origine = scheme + host + port
        //
        // Exemple :
        //   http://localhost:5188
        //   https://localhost:5188
        //
        // IMPORTANT :
        // - Ce n'est PAS une URL complète
        // - Pas de /api, pas de path
        // - Le port DOIT correspondre exactement
        //
        var origins = builder.Configuration
            .GetSection("Cors:AllowedOrigins")
            .Get<string[]>() ?? [];

        p.WithOrigins(origins)

         // Autorise tous les headers HTTP envoyés par le navigateur
         // Exemples :
         // - Content-Type
         // - Authorization (JWT)
         // - Accept
         //
         // Sans ça :
         // → les requêtes POST/PUT avec JSON sont bloquées
         .AllowAnyHeader()

         // Autorise toutes les méthodes HTTP
         // GET, POST, PUT, DELETE, OPTIONS...
         //
         // Sans ça :
         // → seules les requêtes GET fonctionnent
         //
         // Le navigateur fait aussi une requête "OPTIONS"
         // (préflight) avant certains appels
         .AllowAnyMethod();
        //.WithMethods("GET", "POST", "PUT");
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("MvcCors");

app.UseAuthorization();

app.MapControllers();

app.Run();
