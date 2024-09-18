using Microsoft.EntityFrameworkCore;

using CinemaApiCase.Models;

namespace CinemaApiCase.Data
{
    public class CinemaDbContext : DbContext
    {
        public CinemaDbContext(DbContextOptions<CinemaDbContext> options) : base(options) 
        { 
        }

        public DbSet<Cinema> Cinemas { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Screen> Screens { get; set; }
        public DbSet<Seat> Seats { get; set; }
        public DbSet<SeatBooking> SeatBooking { get; set; }
        public DbSet<Showtime> Showtimes { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //// Map the relations
            // Define one-to-many relationship between Cinema and Screens
            modelBuilder.Entity<Screen>()
                .HasOne(s => s.Cinema) // a Screen has one Cinema
                .WithMany(c => c.Screens) // a Cinema has many Screens
                .HasForeignKey(s => s.CinemaId); // foreign key in Screen table

            // Showtime to Screen relationship
            modelBuilder.Entity<Showtime>()
                .HasOne(st => st.Screen)
                .WithMany(scr => scr.Showtimes)
                .HasForeignKey(st => st.ScreenId);

            // Showtime to Movie relationship
            modelBuilder.Entity<Showtime>()
                .HasOne(st => st.Movie)
                .WithMany(m => m.Showtimes)
                .HasForeignKey(st => st.MovieId);

            // SeatBooking to Showtime relationship
            modelBuilder.Entity<SeatBooking>()
                .HasOne(sb => sb.Showtime)
                .WithMany(st => st.SeatBookings)
                .HasForeignKey(sb => sb.ShowtimeId)
                .OnDelete(DeleteBehavior.Restrict); // prevent deleting cascase

            // SeatBooking to Seat relationship
            modelBuilder.Entity<SeatBooking>()
                .HasOne(sb => sb.Seat)
                .WithMany(s => s.SeatBookings)
                .HasForeignKey(sb => sb.SeatId)
                .OnDelete(DeleteBehavior.Restrict); // prevent deleting cascase

            modelBuilder.Entity<Cinema>().HasData(
                new Cinema { Id = 1, Name = "AmagerMovies", Location = "Amager" },  // has two screens
                new Cinema { Id = 2, Name = "InderByShows", Location = "Copenhagen" } // has one screen
            );

            modelBuilder.Entity<Screen>().HasData(
                new Screen { Id = 1, ScreenName = "Screen Alfa", CinemaId = 1 },
                new Screen { Id = 2, ScreenName = "Screen Bravo", CinemaId = 1 },
                new Screen { Id = 3, ScreenName = "Screen Carlie", CinemaId = 2 }
            );

            modelBuilder.Entity<Movie>().HasData(
                new Movie { Id = 1, Title = "It Ends With Us", Duration = 130, Genre = "Romance", Language = "English" },
                new Movie { Id = 2, Title = "Deadpool & Wolverine", Duration = 127, Genre = "Action", Language = "English" },
                new Movie { Id = 3, Title = "Grusomme mig 4", Duration = 94, Genre = "Animated Comedy", Language = "Danish" }
            );

            modelBuilder.Entity<Showtime>().HasData(
                new Showtime { Id = 1, MovieId = 1, StartTime = DateTime.Now.AddDays(1), ScreenId = 1 },             // IEWU, starts tomorrow, on screen alpha
                new Showtime { Id = 2, MovieId = 2, StartTime = DateTime.Now.AddDays(1).AddHours(5), ScreenId = 1 }, // D&W, starts tomorrow+hours, on screen alpha
                new Showtime { Id = 3, MovieId = 2, StartTime = DateTime.Now.AddDays(1), ScreenId = 2 },             // D&W, starts tomorrow, on screen bravo
                new Showtime { Id = 4, MovieId = 3, StartTime = DateTime.Now.AddDays(1).AddHours(5), ScreenId = 2 },  //GM4, starts tomorrow+hours, on screen bravo
                new Showtime { Id = 5, MovieId = 1, StartTime = DateTime.Now.AddDays(2), ScreenId = 2 },             // IEWU, starts tomorrow+1, on screen bravo
                new Showtime { Id = 6, MovieId = 3, StartTime = DateTime.Now.AddDays(1), ScreenId = 3 }             // GM4, starts tomorrow, on screen charlie
            );

            // adding the seats to the different screen
            modelBuilder.Entity<Seat>().HasData(GenerateSeatsForScreen1());
            modelBuilder.Entity<Seat>().HasData(GenerateSeatsForScreen2());
            modelBuilder.Entity<Seat>().HasData(GenerateSeatsForScreen3());
        }

        int seatId = 1; // this is used to give all seats a unique ID
        private List<Seat> GenerateSeatsForScreen1() // 3 rows, 5 colums, 15 seats
        {
            var seats = new List<Seat>();
            var screenId = 1;

            // Row A
            seats.Add(new Seat { Id = seatId++, ScreenId = screenId, Row = 'A', Column = 1, SeatType = SeatType.Handicap });
            seats.Add(new Seat { Id = seatId++, ScreenId = screenId, Row = 'A', Column = 2, SeatType = SeatType.Handicap });
            for (int i = 3; i <= 5; i++)
            {
                seats.Add(new Seat { Id = seatId++, ScreenId = screenId, Row= 'A', Column = i, SeatType = SeatType.Standard });
            }

            // Row B
            for ( int i = 1; i <= 5; i++)
            {
                seats.Add(new Seat { Id = seatId++, ScreenId = screenId, Row = 'B', Column = i, SeatType = SeatType.Standard });
            }

            // Row C
            seats.Add(new Seat { Id = seatId++, ScreenId = screenId, Row = 'C', Column = 1, SeatType = SeatType.Standard});
            seats.Add(new Seat { Id = seatId++, ScreenId = screenId, Row = 'C', Column = 2, SeatType = SeatType.Delux});
            seats.Add(new Seat { Id = seatId++, ScreenId = screenId, Row = 'C', Column = 3, SeatType = SeatType.Delux});
            seats.Add(new Seat { Id = seatId++, ScreenId = screenId, Row = 'C', Column = 4, SeatType = SeatType.Delux});
            seats.Add(new Seat { Id = seatId++, ScreenId = screenId, Row = 'C', Column = 5, SeatType = SeatType.Standard});

            return seats;
        }

        private List<Seat> GenerateSeatsForScreen2() // 4 rows, 5 columns, 20 seats
        {
            var seats = new List<Seat>();
            var screenId = 2;

            // Row A
            seats.Add(new Seat { Id = seatId++, ScreenId = screenId, Row = 'A', Column = 1, SeatType = SeatType.Handicap });
            seats.Add(new Seat { Id = seatId++, ScreenId = screenId, Row = 'A', Column = 2, SeatType = SeatType.Delux });
            seats.Add(new Seat { Id = seatId++, ScreenId = screenId, Row = 'A', Column = 3, SeatType = SeatType.Delux });
            seats.Add(new Seat { Id = seatId++, ScreenId = screenId, Row = 'A', Column = 4, SeatType = SeatType.Delux });
            seats.Add(new Seat { Id = seatId++, ScreenId = screenId, Row = 'A', Column = 5, SeatType = SeatType.Handicap });

            // Row B, C, D (Standard seats)
            for (char row = 'B'; row <= 'D'; row++) 
            {
                for ( int col = 1; col <= 5; col++ )
                {
                    seats.Add(new Seat { Id = seatId++, ScreenId = screenId, Row = row, Column = col, SeatType = SeatType.Standard });
                }
            }

            return seats;
        }

        private List<Seat> GenerateSeatsForScreen3() // 6 rows, 7 columns, 42 seats
        {
            var seats = new List<Seat>();
            var screenId = 3;

            // Row A
            seats.Add(new Seat { Id = seatId++, ScreenId = screenId, Row = 'A', Column = 1, SeatType= SeatType.Handicap });
            seats.Add(new Seat { Id = seatId++, ScreenId = screenId, Row = 'A', Column = 2, SeatType = SeatType.Handicap });
            seats.Add(new Seat { Id = seatId++, ScreenId = screenId, Row = 'A', Column = 3, SeatType = SeatType.Handicap });
            for ( int col = 4; col <= 7; col++ )
            {
                seats.Add(new Seat { Id = seatId++, ScreenId = screenId, Row = 'A', Column = col, SeatType = SeatType.Standard });
            }
            
            // Row B to E (standard seats)
            for (char row = 'B'; row <= 'E'; row++) 
            {
                for (int col = 1; col <= 7; col++)
                {
                    seats.Add(new Seat { Id = seatId++, ScreenId = screenId, Row = row, Column = col, SeatType = SeatType.Standard });
                }
            }

            // Row F (delux seats)
            for ( int col = 1; col <= 7; col++)
            {
                seats.Add(new Seat { Id = seatId++, ScreenId = screenId, Row = 'F', Column = col, SeatType = SeatType.Delux });
            }

            return seats;
        }
    }
}
