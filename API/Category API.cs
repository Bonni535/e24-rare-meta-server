using e24_rare_meta_server.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;


namespace E24RareMetaServer.API
{
    public static class CategoryAPI
    {
        public static void Map(WebApplication app)
        {

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
                return Results.Json(category);
            });


            app.MapDelete("/categories/{id}", (E24RareMetaServerDbContext db, int id) =>
            {
                var category = db.Category.Find(id);
                if (category == null) return Results.NotFound();

                db.Category.Remove(category);
                db.SaveChanges();
                return Results.NoContent();
            });

        }
    }
}
