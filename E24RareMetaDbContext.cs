using Microsoft.EntityFrameworkCore;
using e24_rare_meta_server.Models;
using System.Runtime.CompilerServices;

public class E24RareMetaServerDbContext : DbContext
{

    public DbSet<Post> Posts { get; set; }
    public DbSet<Category> Category { get; set; }

    public DbSet<Comment> Comments { get; set; }
    public DbSet<Tag> Tag { get; set; }
    public DbSet<User> Users { get; set; }

    public E24RareMetaServerDbContext(DbContextOptions<E24RareMetaServerDbContext> context) : base(context)
    {

    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<Post>().HasData(new Post[]
        {
        new Post {Id = 1, UserId = 1, CategoryId = 1, Title = "Had surgery today", PublicationDate=new DateTime(2023,03,05), ImageUrl="https://t4.ftcdn.net/jpg/02/76/02/13/360_F_276021384_p9d1Hkgn4d5LYX1FnCQRSLw76YX7754n.jpg",Content="WENT WELL!"},
        new Post {Id = 2, UserId = 1, CategoryId = 1, Title = "Took the dog for a walk", PublicationDate = new DateTime(2023,05,07), ImageUrl="https://media.istockphoto.com/id/1418164172/photo/cute-labrador-puppy-walks-by-feet.webp?b=1&s=170667a&w=0&k=20&c=mTPWEClVa7Q9pRmM594U56o29ThMfnhO7SteK1MwmIk=",Content="IT WENT WELL"},
        new Post {Id = 3, UserId = 2, CategoryId = 2, Title = "Job interview today", PublicationDate = new DateTime(2024,01,03), ImageUrl="https://fjwp.s3.amazonaws.com/blog/wp-content/uploads/2023/02/13061142/30-of-the-Most-Common-Job-Interview-Questions-With-Example-Answers.jpg", Content= "still no job"}

        });
        modelBuilder.Entity<Comment>().HasData(new Comment[]
        {
            new Comment { Id = 1, AuthorId = 2, PostId = 1, Content = "feel better soon", CreatedOn = new DateTime(2023, 03, 05) },
            new Comment { Id = 2, AuthorId = 1, PostId = 2, Content = "cute dog", CreatedOn = new DateTime(2023, 05, 07) },
            new Comment { Id = 3, AuthorId = 1, PostId = 3, Content = "whoops", CreatedOn = new DateTime(2024, 01, 03) }
        });
        modelBuilder.Entity<Category>().HasData(new Category[]
        {
            new Category { Id = 1, Label = "Personal" },
            new Category { Id = 2, Label = "Work"}
        });
        modelBuilder.Entity<Tag>().HasData(new Tag[]
        {
            new Tag { Id = 1, Label = "Pets"},
            new Tag { Id = 2, Label = "Plants"},
            new Tag { Id = 3, Label = "Art"}
        });
        modelBuilder.Entity<User>().HasData(new User[]
        {
            new User { Id = 1, FirstName = "George", LastName = "Rainbob", Bio = "Cool dude", ProfileImageUrl="https://static01.nyt.com/newsgraphics/2020/11/12/fake-people/4b806cf591a8a76adfc88d19e90c8c634345bf3d/fallbacks/mobile-05.jpg", Email = "Cooldude@gmail.com", CreatedOn = new DateTime(2022,02,22), Uid = "lkj;23jhdh2" },
            new User { Id = 2, FirstName = "Greg", LastName = "Notgreg", Bio = "Good Guy", ProfileImageUrl= "https://static01.nyt.com/newsgraphics/2020/11/12/fake-people/4b806cf591a8a76adfc88d19e90c8c634345bf3d/fallbacks/mobile-06.jpg", Email = "goodguygreg@yahoo.com", CreatedOn = new DateTime(2021,02,09), Uid = "876ds78s8632"}
        });
    }
}