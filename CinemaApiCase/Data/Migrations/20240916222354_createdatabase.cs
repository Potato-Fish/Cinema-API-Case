using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CinemaApiCase.Data.Migrations
{
    /// <inheritdoc />
    public partial class createdatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cinemas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cinemas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Movies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Duration = table.Column<int>(type: "int", nullable: false),
                    Genre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Language = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Screens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ScreenName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CinemaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Screens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Screens_Cinemas_CinemaId",
                        column: x => x.CinemaId,
                        principalTable: "Cinemas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Seats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ScreenId = table.Column<int>(type: "int", nullable: false),
                    Row = table.Column<string>(type: "nvarchar(1)", nullable: false),
                    Column = table.Column<int>(type: "int", nullable: false),
                    SeatType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Seats_Screens_ScreenId",
                        column: x => x.ScreenId,
                        principalTable: "Screens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Showtimes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MovieId = table.Column<int>(type: "int", nullable: false),
                    ScreenId = table.Column<int>(type: "int", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Showtimes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Showtimes_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Showtimes_Screens_ScreenId",
                        column: x => x.ScreenId,
                        principalTable: "Screens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SeatBooking",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShowtimeId = table.Column<int>(type: "int", nullable: false),
                    SeatId = table.Column<int>(type: "int", nullable: false),
                    BookingTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeatBooking", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SeatBooking_Seats_SeatId",
                        column: x => x.SeatId,
                        principalTable: "Seats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SeatBooking_Showtimes_ShowtimeId",
                        column: x => x.ShowtimeId,
                        principalTable: "Showtimes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Cinemas",
                columns: new[] { "Id", "Location", "Name" },
                values: new object[,]
                {
                    { 1, "Amager", "AmagerMovies" },
                    { 2, "Copenhagen", "InderByShows" }
                });

            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "Id", "Duration", "Genre", "Language", "Title" },
                values: new object[,]
                {
                    { 1, 130, "Romance", "English", "It Ends With Us" },
                    { 2, 127, "Action", "English", "Deadpool & Wolverine" },
                    { 3, 94, "Animated Comedy", "Danish", "Grusomme mig 4" }
                });

            migrationBuilder.InsertData(
                table: "Screens",
                columns: new[] { "Id", "CinemaId", "ScreenName" },
                values: new object[,]
                {
                    { 1, 1, "Screen Alfa" },
                    { 2, 1, "Screen Bravo" },
                    { 3, 2, "Screen Carlie" }
                });

            migrationBuilder.InsertData(
                table: "Seats",
                columns: new[] { "Id", "Column", "Row", "ScreenId", "SeatType" },
                values: new object[,]
                {
                    { 78, 1, "A", 1, 2 },
                    { 79, 2, "A", 1, 2 },
                    { 80, 3, "A", 1, 0 },
                    { 81, 4, "A", 1, 0 },
                    { 82, 5, "A", 1, 0 },
                    { 83, 1, "B", 1, 0 },
                    { 84, 2, "B", 1, 0 },
                    { 85, 3, "B", 1, 0 },
                    { 86, 4, "B", 1, 0 },
                    { 87, 5, "B", 1, 0 },
                    { 88, 1, "C", 1, 0 },
                    { 89, 2, "C", 1, 1 },
                    { 90, 3, "C", 1, 1 },
                    { 91, 4, "C", 1, 1 },
                    { 92, 5, "C", 1, 0 },
                    { 93, 1, "A", 2, 2 },
                    { 94, 2, "A", 2, 1 },
                    { 95, 3, "A", 2, 1 },
                    { 96, 4, "A", 2, 1 },
                    { 97, 5, "A", 2, 2 },
                    { 98, 1, "B", 2, 0 },
                    { 99, 2, "B", 2, 0 },
                    { 100, 3, "B", 2, 0 },
                    { 101, 4, "B", 2, 0 },
                    { 102, 5, "B", 2, 0 },
                    { 103, 1, "C", 2, 0 },
                    { 104, 2, "C", 2, 0 },
                    { 105, 3, "C", 2, 0 },
                    { 106, 4, "C", 2, 0 },
                    { 107, 5, "C", 2, 0 },
                    { 108, 1, "D", 2, 0 },
                    { 109, 2, "D", 2, 0 },
                    { 110, 3, "D", 2, 0 },
                    { 111, 4, "D", 2, 0 },
                    { 112, 5, "D", 2, 0 },
                    { 113, 1, "A", 3, 2 },
                    { 114, 2, "A", 3, 2 },
                    { 115, 3, "A", 3, 2 },
                    { 116, 4, "A", 3, 0 },
                    { 117, 5, "A", 3, 0 },
                    { 118, 6, "A", 3, 0 },
                    { 119, 7, "A", 3, 0 },
                    { 120, 1, "B", 3, 0 },
                    { 121, 2, "B", 3, 0 },
                    { 122, 3, "B", 3, 0 },
                    { 123, 4, "B", 3, 0 },
                    { 124, 5, "B", 3, 0 },
                    { 125, 6, "B", 3, 0 },
                    { 126, 7, "B", 3, 0 },
                    { 127, 1, "C", 3, 0 },
                    { 128, 2, "C", 3, 0 },
                    { 129, 3, "C", 3, 0 },
                    { 130, 4, "C", 3, 0 },
                    { 131, 5, "C", 3, 0 },
                    { 132, 6, "C", 3, 0 },
                    { 133, 7, "C", 3, 0 },
                    { 134, 1, "D", 3, 0 },
                    { 135, 2, "D", 3, 0 },
                    { 136, 3, "D", 3, 0 },
                    { 137, 4, "D", 3, 0 },
                    { 138, 5, "D", 3, 0 },
                    { 139, 6, "D", 3, 0 },
                    { 140, 7, "D", 3, 0 },
                    { 141, 1, "E", 3, 0 },
                    { 142, 2, "E", 3, 0 },
                    { 143, 3, "E", 3, 0 },
                    { 144, 4, "E", 3, 0 },
                    { 145, 5, "E", 3, 0 },
                    { 146, 6, "E", 3, 0 },
                    { 147, 7, "E", 3, 0 },
                    { 148, 1, "F", 3, 1 },
                    { 149, 2, "F", 3, 1 },
                    { 150, 3, "F", 3, 1 },
                    { 151, 4, "F", 3, 1 },
                    { 152, 5, "F", 3, 1 },
                    { 153, 6, "F", 3, 1 },
                    { 154, 7, "F", 3, 1 }
                });

            migrationBuilder.InsertData(
                table: "Showtimes",
                columns: new[] { "Id", "MovieId", "ScreenId", "StartTime" },
                values: new object[,]
                {
                    { 1, 1, 1, new DateTime(2024, 9, 18, 0, 23, 54, 207, DateTimeKind.Local).AddTicks(9362) },
                    { 2, 2, 1, new DateTime(2024, 9, 18, 5, 23, 54, 207, DateTimeKind.Local).AddTicks(9404) },
                    { 3, 2, 2, new DateTime(2024, 9, 18, 0, 23, 54, 207, DateTimeKind.Local).AddTicks(9406) },
                    { 4, 3, 2, new DateTime(2024, 9, 18, 5, 23, 54, 207, DateTimeKind.Local).AddTicks(9409) },
                    { 5, 1, 2, new DateTime(2024, 9, 19, 0, 23, 54, 207, DateTimeKind.Local).AddTicks(9411) },
                    { 6, 3, 3, new DateTime(2024, 9, 18, 0, 23, 54, 207, DateTimeKind.Local).AddTicks(9413) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Screens_CinemaId",
                table: "Screens",
                column: "CinemaId");

            migrationBuilder.CreateIndex(
                name: "IX_SeatBooking_SeatId",
                table: "SeatBooking",
                column: "SeatId");

            migrationBuilder.CreateIndex(
                name: "IX_SeatBooking_ShowtimeId",
                table: "SeatBooking",
                column: "ShowtimeId");

            migrationBuilder.CreateIndex(
                name: "IX_Seats_ScreenId",
                table: "Seats",
                column: "ScreenId");

            migrationBuilder.CreateIndex(
                name: "IX_Showtimes_MovieId",
                table: "Showtimes",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_Showtimes_ScreenId",
                table: "Showtimes",
                column: "ScreenId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SeatBooking");

            migrationBuilder.DropTable(
                name: "Seats");

            migrationBuilder.DropTable(
                name: "Showtimes");

            migrationBuilder.DropTable(
                name: "Movies");

            migrationBuilder.DropTable(
                name: "Screens");

            migrationBuilder.DropTable(
                name: "Cinemas");
        }
    }
}
