using Moq;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using CinemaApiCase.Controllers;
using CinemaApiCase.Data;
using CinemaApiCase.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace CinemaApiCase.Tests
{
    public class ScreensControllerTests
    {
        [Fact]
        public async Task GetCinemas_ReturnsNotFound_WhenNoCinemasExist()
        {
            // setup in memory database
            var options = new DbContextOptionsBuilder<CinemaDbContext>()
                .UseInMemoryDatabase(databaseName: "TestCinemaDb")
                .Options;

            // initialise using the in memory database
            using (var context = new CinemaDbContext(options))
            {
                // Ensure the database is empty (testing for a 404 not found)
                context.Cinemas.RemoveRange(context.Cinemas);
                await context.SaveChangesAsync();

                // create instance of a screen controller
                var controller = new ScreensController(context);

                // call GetCinema method on the ScreenController instance (return a result based on the state of the database)
                var result = await controller.GetCinemas();

                // Check the result type
                Assert.IsType<NotFoundObjectResult>(result);
            }
        }

        [Fact]
        public async Task GetCinemas_ReturnsOkResult_WithCinemas()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<CinemaDbContext>()
                .UseInMemoryDatabase(databaseName: "TestCinemaDb")
                .Options;

            using (var context = new CinemaDbContext(options))
            {
                // Seed the in memory database with some cinema data
                var cinema1 = new Cinema
                {
                    Id = 1,
                    Name = "AmagerMovies",
                    Location = "Amager",
                    Screens = new List<Screen>
                    {
                        new Screen { Id = 1, ScreenName = "Screen Alfa" },
                        new Screen { Id = 2, ScreenName = "Screen Bravo" }
                    }
                };

                var cinema2 = new Cinema
                {
                    Id = 2,
                    Name = "InderByShows",
                    Location = "Copenhagen",
                    Screens = new List<Screen>
                    {
                        new Screen { Id = 3, ScreenName = "Screen Carlie" }
                    }
                };

                context.Cinemas.AddRange(cinema1, cinema2);
                await context.SaveChangesAsync();

                var controller = new ScreensController(context);

                // Act
                var result = await controller.GetCinemas();

                // Assert
                var okResult = Assert.IsType<OkObjectResult>(result); // Check if the result is Ok
                var cinemas = Assert.IsType<List<object>>(okResult.Value); // Ensure the result is a list of cinema objects

                Assert.Equal(2, cinemas.Count); // We expect two cinemas in the response
            }
        }

        [Fact]
        public async Task GetScreen_ReturnsNotFound_WhenScreenDoesNotExist()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<CinemaDbContext>()
                .UseInMemoryDatabase(databaseName: "TestCinemaDb")
                .Options;

            using (var context = new CinemaDbContext(options))
            {
                // Ensure the database is empty
                context.Screens.RemoveRange(context.Screens);
                await context.SaveChangesAsync();

                var controller = new ScreensController(context);

                // Act
                var result = await controller.GetScreen(1); // Assume screenId = 1

                // Assert
                Assert.IsType<NotFoundObjectResult>(result);
            }
        }

        [Fact]
        public async Task GetScreen_ReturnsOk_WhenScreenExists()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<CinemaDbContext>()
                .UseInMemoryDatabase(databaseName: "TestCinemaDb")
                .Options;

            using (var context = new CinemaDbContext(options))
            {
                // Add sample cinema, screen, movie, and showtime data to the in-memory database
                var cinema = new Cinema { Id = 1, Name = "AmagerMovies", Location = "Amager" };
                var screen = new Screen { Id = 1, ScreenName = "Screen Alfa", CinemaId = cinema.Id, Cinema = cinema };
                var movie = new Movie { Id = 1, Title = "My Favorite Movie!", Duration = 420, Genre = "Action", Language = "English" };
                var showtime = new Showtime { Id = 1, MovieId = movie.Id, ScreenId = screen.Id, StartTime = System.DateTime.Now, Movie = movie, Screen = screen };

                context.Cinemas.Add(cinema);
                context.Screens.Add(screen);
                context.Movies.Add(movie);
                context.Showtimes.Add(showtime);

                await context.SaveChangesAsync();

                var controller = new ScreensController(context);

                // Act
                var result = await controller.GetScreen(screen.Id);

                // Assert
                var okResult = Assert.IsType<OkObjectResult>(result); // Check if the result is of type OkObjectResult
                var resultValue = okResult.Value; // Extract the returned object

                // Ensure the response contains the correct screen and showtime details
                Assert.NotNull(resultValue);
                Assert.Contains("ScreenName", resultValue.ToString());
                Assert.Contains("TotalSeats", resultValue.ToString());
                Assert.Contains("Showtimes", resultValue.ToString());
            }
        }

        // test GetSeats not found

        // test GetSeats ok
    }
}
