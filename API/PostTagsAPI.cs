using e24_rare_meta_server.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;
using System.Xml.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;


namespace E24RareMetaServer.API
{

    public static class PostTagAPI
    {
        public static void Map(WebApplication app)
        {
            // Post your API calls here


            // Add Tag to a Post
            app.MapPatch("/api/posts/{postId}/tag", (E24RareMetaServerDbContext db, PostTagDto postTag) =>
            {
                Post post = db.Posts
             .Include(p => p.Tags)
             .SingleOrDefault(p => p.Id == postTag.PostId);

                Tag tag = db.Tag
             .SingleOrDefault(t => t.Id == postTag.TagId);
                if (post == null)
                {
                    return Results.BadRequest();
                }
                try
                {
                    post.Tags.Add(tag);
                    db.SaveChanges();
                    return Results.Ok(postTag);
                }
                catch (Exception ex)
                {
                    return Results.BadRequest(ex);
                }
            });

            // Get PostTags
            app.MapGet("/api/posts/tags/{tagId}", (E24RareMetaServerDbContext db, int tagId) =>
            {
                try
                {
                    var postWithTag = db.Posts.Include(p => p.Tags);
                    var postWithoutTag = postWithTag.Where(p => p.Tags.Where(t => t.Id == tagId).Count() != 0);
                    return Results.Ok(postWithoutTag);
                }
                catch
                {
                    return Results.NotFound();
                }
            });
        }

    }
}