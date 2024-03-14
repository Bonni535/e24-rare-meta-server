using e24_rare_meta_server.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;


namespace E24RareMetaServer.API
{
    public static class UserAPI
    {
        public static void Map(WebApplication app)
        {

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
                return Results.Created($"/user/{newUser.Id}", newUser);
            });


        }
    }
}

