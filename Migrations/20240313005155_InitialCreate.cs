using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace e24_rare_meta_server.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Label = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Comment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AuthorId = table.Column<int>(type: "integer", nullable: false),
                    PostId = table.Column<int>(type: "integer", nullable: false),
                    Content = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comment", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    CategoryId = table.Column<int>(type: "integer", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    PublicationDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ImageUrl = table.Column<string>(type: "text", nullable: false),
                    Content = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tag",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Label = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tag", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    Bio = table.Column<string>(type: "text", nullable: false),
                    ProfileImageUrl = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Uid = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "Id", "Label" },
                values: new object[,]
                {
                    { 1, "Personal" },
                    { 2, "Work" }
                });

            migrationBuilder.InsertData(
                table: "Comment",
                columns: new[] { "Id", "AuthorId", "Content", "CreatedOn", "PostId" },
                values: new object[,]
                {
                    { 1, 2, "feel better soon", new DateTime(2023, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 2, 1, "cute dog", new DateTime(2023, 5, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 3, 1, "whoops", new DateTime(2024, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 3 }
                });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "CategoryId", "Content", "ImageUrl", "PublicationDate", "Title", "UserId" },
                values: new object[,]
                {
                    { 1, 1, "WENT WELL!", "https://t4.ftcdn.net/jpg/02/76/02/13/360_F_276021384_p9d1Hkgn4d5LYX1FnCQRSLw76YX7754n.jpg", new DateTime(2023, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Had surgery today", 1 },
                    { 2, 1, "IT WENT WELL", "https://media.istockphoto.com/id/1418164172/photo/cute-labrador-puppy-walks-by-feet.webp?b=1&s=170667a&w=0&k=20&c=mTPWEClVa7Q9pRmM594U56o29ThMfnhO7SteK1MwmIk=", new DateTime(2023, 5, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Took the dog for a walk", 1 },
                    { 3, 2, "still no job", "https://fjwp.s3.amazonaws.com/blog/wp-content/uploads/2023/02/13061142/30-of-the-Most-Common-Job-Interview-Questions-With-Example-Answers.jpg", new DateTime(2024, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Job interview today", 2 }
                });

            migrationBuilder.InsertData(
                table: "Tag",
                columns: new[] { "Id", "Label" },
                values: new object[,]
                {
                    { 1, "Pets" },
                    { 2, "Plants" },
                    { 3, "Art" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Bio", "CreatedOn", "Email", "FirstName", "LastName", "ProfileImageUrl", "Uid" },
                values: new object[,]
                {
                    { 1, "Cool dude", new DateTime(2022, 2, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Cooldude@gmail.com", "George", "Rainbob", "https://static01.nyt.com/newsgraphics/2020/11/12/fake-people/4b806cf591a8a76adfc88d19e90c8c634345bf3d/fallbacks/mobile-05.jpg", "lkj;23jhdh2" },
                    { 2, "Good Guy", new DateTime(2021, 2, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "goodguygreg@yahoo.com", "Greg", "Notgreg", "https://static01.nyt.com/newsgraphics/2020/11/12/fake-people/4b806cf591a8a76adfc88d19e90c8c634345bf3d/fallbacks/mobile-06.jpg", "876ds78s8632" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "Comment");

            migrationBuilder.DropTable(
                name: "Posts");

            migrationBuilder.DropTable(
                name: "Tag");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
