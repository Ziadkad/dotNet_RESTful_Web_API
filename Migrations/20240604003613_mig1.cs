using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace dotNet_RESTful_Web_API.Migrations
{
    /// <inheritdoc />
    public partial class mig1 : Migration
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
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
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

            migrationBuilder.CreateTable(
                name: "UserNumbers",
                columns: table => new
                {
                    UserNo = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserNumbers", x => x.UserNo);
                    table.ForeignKey(
                        name: "FK_UserNumbers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Age", "CreatedDate", "Disability", "Email", "ImageUrl", "Name", "Password", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, 10, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "u1@u1", "https://fakeimg.pl/300/", "u1", "u1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, 20, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "u2@u2", "https://fakeimg.pl/300/", "u2", "u2", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, 30, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "u3@u3", "https://fakeimg.pl/300/", "u3", "u3", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, 40, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "u4@u4", "https://fakeimg.pl/300/", "u4", "u4", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, 50, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "u5@u5", "https://fakeimg.pl/300/", "u5", "u5", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 6, 60, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "u6@u6", "https://fakeimg.pl/300/", "u6", "u6", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 7, 70, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "u7@u7", "https://fakeimg.pl/300/", "u7", "u7", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 8, 80, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "u8@u8", "https://fakeimg.pl/300/", "u8", "u8", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 9, 90, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "u9@u9", "https://fakeimg.pl/300/", "u9", "u9", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 10, 100, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "u10@u10", "https://fakeimg.pl/300/", "u10", "u10", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserNumbers_UserId",
                table: "UserNumbers",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserNumbers");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
