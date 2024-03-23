using e24_rare_meta_server.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;


namespace E24RareMetaServer.API
{
    public static class TagAPI
    {
        public static void Map(WebApplication app)
        {
            // GET All Tags
            app.MapGet("/tags", (E24RareMetaServerDbContext db) =>
            {
                return db.Tag.ToList();
            });

            // GET Single Tag
            app.MapGet("/tags/{tagId}", (E24RareMetaServerDbContext db, int tagId) =>
            {
                return db.Tag.FirstOrDefault(s => s.Id == tagId);
            });

            // CREATE a tag
            app.MapPost("/tags", (E24RareMetaServerDbContext db, Tag newTag) =>
            {
                db.Tag.Add(newTag);
                db.SaveChanges();
                return Results.Created($"/api/Tag/{newTag.Id}", newTag);
            });

            // DELETE a Tag
            app.MapDelete("/tags/{id}", (E24RareMetaServerDbContext db, int id) =>
            {
                Tag tagToDelete = db.Tag.SingleOrDefault(tagToDelete => tagToDelete.Id == id);
                if (tagToDelete == null)
                {
                    return Results.NotFound("Tag Not Found.");
                }
                db.Tag.Remove(tagToDelete);
                db.SaveChanges();
                return Results.NoContent();
            });

            // UPDATE a Tag
            app.MapPut("/tags/{id}", (E24RareMetaServerDbContext db, int id, Tag tag) =>
            {
                Tag tagToUpdate = db.Tag.SingleOrDefault(tag => tag.Id == id);

                if (tagToUpdate == null)
                {
                    return Results.NotFound("Tag Not Found.");
                }

                tagToUpdate.Label = tag.Label;

                db.SaveChanges();
                return Results.NoContent();
            });
        }
    }
}
