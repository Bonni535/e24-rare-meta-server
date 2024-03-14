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
            app.MapGet("/api/tags", (E24RareMetaServerDbContext db) =>
            {
                return db.Tag.ToList();
            });

            // CREATE a tag
            app.MapPost("/api/tags", (E24RareMetaServerDbContext db, Tag newTag) =>
            {
                db.Tag.Add(newTag);
                db.SaveChanges();
                return Results.Created($"/api/Tag/{newTag.Id}", newTag);
            });

        }
    }
}