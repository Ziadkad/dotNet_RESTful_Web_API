using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace dotNet_RESTful_Web_API.Migrations
{
    /// <inheritdoc />
    public partial class first : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Disability = table.Column<bool>(type: "bit", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Age", "CreatedDate", "Disability", "Email", "ImageUrl", "Name", "UpdatedDate", "password" },
                values: new object[,]
                {
                    { 1, 10, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "u1@u1", "https://fakeimg.pl/300/", "u1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "u1" },
                    { 2, 20, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "u2@u2", "https://fakeimg.pl/300/", "u2", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "u2" },
                    { 3, 30, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "u3@u3", "https://fakeimg.pl/300/", "u3", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "u3" },
                    { 4, 40, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "u4@u4", "https://fakeimg.pl/300/", "u4", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "u4" },
                    { 5, 50, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "u5@u5", "https://fakeimg.pl/300/", "u5", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "u5" },
                    { 6, 60, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "u6@u6", "https://fakeimg.pl/300/", "u6", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "u6" },
                    { 7, 70, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "u7@u7", "https://fakeimg.pl/300/", "u7", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "u7" },
                    { 8, 80, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "u8@u8", "https://fakeimg.pl/300/", "u8", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "u8" },
                    { 9, 90, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "u9@u9", "https://fakeimg.pl/300/", "u9", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "u9" },
                    { 10, 100, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "u10@u10", "https://fakeimg.pl/300/", "u10", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "u10" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
