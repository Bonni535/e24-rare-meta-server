using e24_rare_meta_server.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http.Json;
using E24RareMetaServer.API;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// allows passing datetimes without time zone data 
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

// allows our api endpoints to access the database through Entity Framework Core
builder.Services.AddNpgsql<E24RareMetaServerDbContext>(builder.Configuration["E24RareMetaServerDbConnectionString"]);

// Set the JSON serializer options
builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/categories", (E24RareMetaServerDbContext db) =>
{
    return db.Category.ToList();
});

app.MapGet("/categories/{id}", (E24RareMetaServerDbContext db, int id) =>
{
    var category = db.Category.Find(id);
    if (category == null) return Results.NotFound();
    return Results.Ok(category);
});

app.MapPost("/categories", (E24RareMetaServerDbContext db, Category category) =>
{
    db.Category.Add(category);
    db.SaveChanges();
    return Results.Created($"/categories/{category.Id}", category);
});

app.MapPut("/categories/{id}", (E24RareMetaServerDbContext db, int id, Category updatedCategory) =>
{
    var category = db.Category.Find(id);
    if (category == null) return Results.NotFound();

    category.Label = updatedCategory.Label;

    db.SaveChanges();
    return Results.NoContent();
});

app.MapDelete("/categories/{id}", (E24RareMetaServerDbContext db, int id) =>
{
    var category = db.Category.Find(id);
    if (category == null) return Results.NotFound();

    db.Category.Remove(category);
    db.SaveChanges();
    return Results.NoContent();
});



CategoryAPI.Map(app);
CommentAPI.Map(app);
PostAPI.Map(app);
TagAPI.Map(app);
UserAPI.Map(app);




app.Run();

