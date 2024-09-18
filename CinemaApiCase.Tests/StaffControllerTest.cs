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
    public class StaffControllerTest
    {
        [Fact]
        public async Task GetScreenMetrics_ReturnsNotFound_WhenNoScreensExist()
        {
            // setup in memory database
            var options = new DbContextOptionsBuilder<CinemaDbContext>()
                .UseInMemoryDatabase(databaseName: "TestCinemaDb")
                .Options;

            using (var context = new CinemaDbContext(options))
            {
                // ensure screens db is empty
                context.Screens.RemoveRange(context.Screens);
                await context.SaveChangesAsync();

                // create the new context for the staff controller
                var controller = new StaffController(context);

                var result = await controller.GetScreenMetrics(1); // Assume screenId = 1

                Assert.IsType<NotFoundObjectResult>(result);
            }
        }

        // test unauthorized

        // test ok
    }
}
