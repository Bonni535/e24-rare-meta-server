using e24_rare_meta_server.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http.Json;

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

app.MapGet("/checkuser/{uid}", (E24RareMetaServerDbContext db, string uid) => //check for user
{
    var user = db.Users.Where(user => user.Uid == uid).ToList();

    if (uid == null)
    {
        return Results.NotFound();
    }
    else
    {
        return Results.Ok(user);
    }
});

app.MapPost("/user", (E24RareMetaServerDbContext db, User newUser) => // creates user entity
{
    db.Users.Add(newUser);
    db.SaveChanges();
    return Results.Created($"/user/{newUser.Id}",newUser);
});

app.MapGet("/posts", (E24RareMetaServerDbContext db) => //gets all posts
{
    return db.Posts.ToList();
});

app.MapDelete("/posts", (E24RareMetaServerDbContext db, int id) => //delete a post
{
    var postToDelete = db.Posts.FirstOrDefault(s => s.Id == id);
    if (postToDelete == null)
    {
        return Results.NotFound("There was an issue with deleting the post.");
    }
    else
    {
        db.Posts.Remove(postToDelete);

        db.SaveChanges();
    }
    return Results.Ok();
});

app.MapPut("/posts", (E24RareMetaServerDbContext db, int postId, PostDto updatedPost) => //update Post
{
    var postToUpdate = db.Posts.Single(a => a.Id == postId);
    postToUpdate.Title = updatedPost.Title;
    postToUpdate.ImageUrl = updatedPost.ImgUrl;
    postToUpdate.Content = updatedPost.Content;
    db.SaveChanges();
    return Results.Created($"/artists/{postToUpdate.Id}", updatedPost);

});

app.MapGet("/posts/{postId}", (E24RareMetaServerDbContext db, int postId) => // get single post by id
{
    return db.Posts.FirstOrDefault(s => s.Id == postId);
});

app.Run();

