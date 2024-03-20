using e24_rare_meta_server.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;


namespace E24RareMetaServer.API
{
    public static class PostAPI
    {
        public static void Map(WebApplication app)
        {

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
                return Results.Created($"/posts/{postToUpdate.Id}", updatedPost);

            });

            app.MapGet("/posts/{postId}", (E24RareMetaServerDbContext db, int postId) => // get single post by id
            {
                return db.Posts.FirstOrDefault(s => s.Id == postId);
            });

            app.MapPost("/posts", (E24RareMetaServerDbContext db, Post newPost) =>
            {
                db.Posts.Add(newPost); 
                db.SaveChanges();
                return Results.Created($"/posts/{newPost.Id}", newPost);
            });


        }
    }
}


