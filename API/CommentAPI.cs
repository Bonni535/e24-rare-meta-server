using e24_rare_meta_server.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;
using System.Xml.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;


namespace E24RareMetaServer.API
{
    public static class CommentAPI
    {
        public static void Map(WebApplication app)
        {

            // Post your API calls here

            // Create a Comment
            app.MapPost("api/comments", (E24RareMetaServerDbContext db, Comment comment) =>
            {
                try
                {
                    db.Comments.Add(comment);
                    db.SaveChanges();
                    return Results.Created($"api/comments/{comment.Id}", comment);
                }
                catch (DbUpdateException)
                {
                    return Results.BadRequest("Invalid data submitted");
                }
            });

            // Read all the Post's Comments
            app.MapGet("api/posts/{postId}/comments", (E24RareMetaServerDbContext db, int postId) =>
            {
                var postComments = db.Posts
                .Include(p => p.Comments) //we need a : public ICollection<Comment> Comments { get; set;} line in the Posts.cs model
                .FirstOrDefault(p => p.Id == postId);
                return Results.Ok(postComments);
            });


            // Delete a Comment on a Post
            app.MapDelete("/posts/{id}/comments/{commentId}", (E24RareMetaServerDbContext db, int commentId) =>
            {
                var commentToDelete = db.Comments.FirstOrDefault(c => c.Id == commentId);
                if (commentToDelete != null)
                {
                    return Results.NotFound("The comment was not found.");
                }

                db.Comments.Remove(commentToDelete);
                return Results.Ok("This Comment was successfully deleted!");
            });

            // Update a Comment
            app.MapPut("/api/comments/{id}", (E24RareMetaServerDbContext db, int id, Comment comment) =>
{
                var commentToUpdate = db.Comments.SingleOrDefault(c => c.Id == id);
                if (commentToUpdate == null)
                {
                    return Results.NotFound();
                }
                commentToUpdate.AuthorId = comment.AuthorId;
                commentToUpdate.PostId = comment.PostId;
                commentToUpdate.Content = comment.Content;
                commentToUpdate.CreatedOn = comment.CreatedOn;

                db.SaveChanges();
                return Results.NoContent();
            });

        }
    }
}
