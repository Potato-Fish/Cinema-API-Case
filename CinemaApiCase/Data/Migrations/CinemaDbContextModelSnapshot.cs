﻿// <auto-generated />
using System;
using CinemaApiCase.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CinemaApiCase.Data.Migrations
{
    [DbContext(typeof(CinemaDbContext))]
    partial class CinemaDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CinemaApiCase.Models.Cinema", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Cinemas");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Location = "Amager",
                            Name = "AmagerMovies"
                        },
                        new
                        {
                            Id = 2,
                            Location = "Copenhagen",
                            Name = "InderByShows"
                        });
                });

            modelBuilder.Entity("CinemaApiCase.Models.Movie", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Duration")
                        .HasColumnType("int");

                    b.Property<string>("Genre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Language")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Movies");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Duration = 130,
                            Genre = "Romance",
                            Language = "English",
                            Title = "It Ends With Us"
                        },
                        new
                        {
                            Id = 2,
                            Duration = 127,
                            Genre = "Action",
                            Language = "English",
                            Title = "Deadpool & Wolverine"
                        },
                        new
                        {
                            Id = 3,
                            Duration = 94,
                            Genre = "Animated Comedy",
                            Language = "Danish",
                            Title = "Grusomme mig 4"
                        });
                });

            modelBuilder.Entity("CinemaApiCase.Models.Screen", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CinemaId")
                        .HasColumnType("int");

                    b.Property<string>("ScreenName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CinemaId");

                    b.ToTable("Screens");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CinemaId = 1,
                            ScreenName = "Screen Alfa"
                        },
                        new
                        {
                            Id = 2,
                            CinemaId = 1,
                            ScreenName = "Screen Bravo"
                        },
                        new
                        {
                            Id = 3,
                            CinemaId = 2,
                            ScreenName = "Screen Carlie"
                        });
                });

            modelBuilder.Entity("CinemaApiCase.Models.Seat", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Column")
                        .HasColumnType("int");

                    b.Property<string>("Row")
                        .IsRequired()
                        .HasColumnType("nvarchar(1)");

                    b.Property<int>("ScreenId")
                        .HasColumnType("int");

                    b.Property<int>("SeatType")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ScreenId");

                    b.ToTable("Seats");

                    b.HasData(
                        new
                        {
                            Id = 78,
                            Column = 1,
                            Row = "A",
                            ScreenId = 1,
                            SeatType = 2
                        },
                        new
                        {
                            Id = 79,
                            Column = 2,
                            Row = "A",
                            ScreenId = 1,
                            SeatType = 2
                        },
                        new
                        {
                            Id = 80,
                            Column = 3,
                            Row = "A",
                            ScreenId = 1,
                            SeatType = 0
                        },
                        new
                        {
                            Id = 81,
                            Column = 4,
                            Row = "A",
                            ScreenId = 1,
                            SeatType = 0
                        },
                        new
                        {
                            Id = 82,
                            Column = 5,
                            Row = "A",
                            ScreenId = 1,
                            SeatType = 0
                        },
                        new
                        {
                            Id = 83,
                            Column = 1,
                            Row = "B",
                            ScreenId = 1,
                            SeatType = 0
                        },
                        new
                        {
                            Id = 84,
                            Column = 2,
                            Row = "B",
                            ScreenId = 1,
                            SeatType = 0
                        },
                        new
                        {
                            Id = 85,
                            Column = 3,
                            Row = "B",
                            ScreenId = 1,
                            SeatType = 0
                        },
                        new
                        {
                            Id = 86,
                            Column = 4,
                            Row = "B",
                            ScreenId = 1,
                            SeatType = 0
                        },
                        new
                        {
                            Id = 87,
                            Column = 5,
                            Row = "B",
                            ScreenId = 1,
                            SeatType = 0
                        },
                        new
                        {
                            Id = 88,
                            Column = 1,
                            Row = "C",
                            ScreenId = 1,
                            SeatType = 0
                        },
                        new
                        {
                            Id = 89,
                            Column = 2,
                            Row = "C",
                            ScreenId = 1,
                            SeatType = 1
                        },
                        new
                        {
                            Id = 90,
                            Column = 3,
                            Row = "C",
                            ScreenId = 1,
                            SeatType = 1
                        },
                        new
                        {
                            Id = 91,
                            Column = 4,
                            Row = "C",
                            ScreenId = 1,
                            SeatType = 1
                        },
                        new
                        {
                            Id = 92,
                            Column = 5,
                            Row = "C",
                            ScreenId = 1,
                            SeatType = 0
                        },
                        new
                        {
                            Id = 93,
                            Column = 1,
                            Row = "A",
                            ScreenId = 2,
                            SeatType = 2
                        },
                        new
                        {
                            Id = 94,
                            Column = 2,
                            Row = "A",
                            ScreenId = 2,
                            SeatType = 1
                        },
                        new
                        {
                            Id = 95,
                            Column = 3,
                            Row = "A",
                            ScreenId = 2,
                            SeatType = 1
                        },
                        new
                        {
                            Id = 96,
                            Column = 4,
                            Row = "A",
                            ScreenId = 2,
                            SeatType = 1
                        },
                        new
                        {
                            Id = 97,
                            Column = 5,
                            Row = "A",
                            ScreenId = 2,
                            SeatType = 2
                        },
                        new
                        {
                            Id = 98,
                            Column = 1,
                            Row = "B",
                            ScreenId = 2,
                            SeatType = 0
                        },
                        new
                        {
                            Id = 99,
                            Column = 2,
                            Row = "B",
                            ScreenId = 2,
                            SeatType = 0
                        },
                        new
                        {
                            Id = 100,
                            Column = 3,
                            Row = "B",
                            ScreenId = 2,
                            SeatType = 0
                        },
                        new
                        {
                            Id = 101,
                            Column = 4,
                            Row = "B",
                            ScreenId = 2,
                            SeatType = 0
                        },
                        new
                        {
                            Id = 102,
                            Column = 5,
                            Row = "B",
                            ScreenId = 2,
                            SeatType = 0
                        },
                        new
                        {
                            Id = 103,
                            Column = 1,
                            Row = "C",
                            ScreenId = 2,
                            SeatType = 0
                        },
                        new
                        {
                            Id = 104,
                            Column = 2,
                            Row = "C",
                            ScreenId = 2,
                            SeatType = 0
                        },
                        new
                        {
                            Id = 105,
                            Column = 3,
                            Row = "C",
                            ScreenId = 2,
                            SeatType = 0
                        },
                        new
                        {
                            Id = 106,
                            Column = 4,
                            Row = "C",
                            ScreenId = 2,
                            SeatType = 0
                        },
                        new
                        {
                            Id = 107,
                            Column = 5,
                            Row = "C",
                            ScreenId = 2,
                            SeatType = 0
                        },
                        new
                        {
                            Id = 108,
                            Column = 1,
                            Row = "D",
                            ScreenId = 2,
                            SeatType = 0
                        },
                        new
                        {
                            Id = 109,
                            Column = 2,
                            Row = "D",
                            ScreenId = 2,
                            SeatType = 0
                        },
                        new
                        {
                            Id = 110,
                            Column = 3,
                            Row = "D",
                            ScreenId = 2,
                            SeatType = 0
                        },
                        new
                        {
                            Id = 111,
                            Column = 4,
                            Row = "D",
                            ScreenId = 2,
                            SeatType = 0
                        },
                        new
                        {
                            Id = 112,
                            Column = 5,
                            Row = "D",
                            ScreenId = 2,
                            SeatType = 0
                        },
                        new
                        {
                            Id = 113,
                            Column = 1,
                            Row = "A",
                            ScreenId = 3,
                            SeatType = 2
                        },
                        new
                        {
                            Id = 114,
                            Column = 2,
                            Row = "A",
                            ScreenId = 3,
                            SeatType = 2
                        },
                        new
                        {
                            Id = 115,
                            Column = 3,
                            Row = "A",
                            ScreenId = 3,
                            SeatType = 2
                        },
                        new
                        {
                            Id = 116,
                            Column = 4,
                            Row = "A",
                            ScreenId = 3,
                            SeatType = 0
                        },
                        new
                        {
                            Id = 117,
                            Column = 5,
                            Row = "A",
                            ScreenId = 3,
                            SeatType = 0
                        },
                        new
                        {
                            Id = 118,
                            Column = 6,
                            Row = "A",
                            ScreenId = 3,
                            SeatType = 0
                        },
                        new
                        {
                            Id = 119,
                            Column = 7,
                            Row = "A",
                            ScreenId = 3,
                            SeatType = 0
                        },
                        new
                        {
                            Id = 120,
                            Column = 1,
                            Row = "B",
                            ScreenId = 3,
                            SeatType = 0
                        },
                        new
                        {
                            Id = 121,
                            Column = 2,
                            Row = "B",
                            ScreenId = 3,
                            SeatType = 0
                        },
                        new
                        {
                            Id = 122,
                            Column = 3,
                            Row = "B",
                            ScreenId = 3,
                            SeatType = 0
                        },
                        new
                        {
                            Id = 123,
                            Column = 4,
                            Row = "B",
                            ScreenId = 3,
                            SeatType = 0
                        },
                        new
                        {
                            Id = 124,
                            Column = 5,
                            Row = "B",
                            ScreenId = 3,
                            SeatType = 0
                        },
                        new
                        {
                            Id = 125,
                            Column = 6,
                            Row = "B",
                            ScreenId = 3,
                            SeatType = 0
                        },
                        new
                        {
                            Id = 126,
                            Column = 7,
                            Row = "B",
                            ScreenId = 3,
                            SeatType = 0
                        },
                        new
                        {
                            Id = 127,
                            Column = 1,
                            Row = "C",
                            ScreenId = 3,
                            SeatType = 0
                        },
                        new
                        {
                            Id = 128,
                            Column = 2,
                            Row = "C",
                            ScreenId = 3,
                            SeatType = 0
                        },
                        new
                        {
                            Id = 129,
                            Column = 3,
                            Row = "C",
                            ScreenId = 3,
                            SeatType = 0
                        },
                        new
                        {
                            Id = 130,
                            Column = 4,
                            Row = "C",
                            ScreenId = 3,
                            SeatType = 0
                        },
                        new
                        {
                            Id = 131,
                            Column = 5,
                            Row = "C",
                            ScreenId = 3,
                            SeatType = 0
                        },
                        new
                        {
                            Id = 132,
                            Column = 6,
                            Row = "C",
                            ScreenId = 3,
                            SeatType = 0
                        },
                        new
                        {
                            Id = 133,
                            Column = 7,
                            Row = "C",
                            ScreenId = 3,
                            SeatType = 0
                        },
                        new
                        {
                            Id = 134,
                            Column = 1,
                            Row = "D",
                            ScreenId = 3,
                            SeatType = 0
                        },
                        new
                        {
                            Id = 135,
                            Column = 2,
                            Row = "D",
                            ScreenId = 3,
                            SeatType = 0
                        },
                        new
                        {
                            Id = 136,
                            Column = 3,
                            Row = "D",
                            ScreenId = 3,
                            SeatType = 0
                        },
                        new
                        {
                            Id = 137,
                            Column = 4,
                            Row = "D",
                            ScreenId = 3,
                            SeatType = 0
                        },
                        new
                        {
                            Id = 138,
                            Column = 5,
                            Row = "D",
                            ScreenId = 3,
                            SeatType = 0
                        },
                        new
                        {
                            Id = 139,
                            Column = 6,
                            Row = "D",
                            ScreenId = 3,
                            SeatType = 0
                        },
                        new
                        {
                            Id = 140,
                            Column = 7,
                            Row = "D",
                            ScreenId = 3,
                            SeatType = 0
                        },
                        new
                        {
                            Id = 141,
                            Column = 1,
                            Row = "E",
                            ScreenId = 3,
                            SeatType = 0
                        },
                        new
                        {
                            Id = 142,
                            Column = 2,
                            Row = "E",
                            ScreenId = 3,
                            SeatType = 0
                        },
                        new
                        {
                            Id = 143,
                            Column = 3,
                            Row = "E",
                            ScreenId = 3,
                            SeatType = 0
                        },
                        new
                        {
                            Id = 144,
                            Column = 4,
                            Row = "E",
                            ScreenId = 3,
                            SeatType = 0
                        },
                        new
                        {
                            Id = 145,
                            Column = 5,
                            Row = "E",
                            ScreenId = 3,
                            SeatType = 0
                        },
                        new
                        {
                            Id = 146,
                            Column = 6,
                            Row = "E",
                            ScreenId = 3,
                            SeatType = 0
                        },
                        new
                        {
                            Id = 147,
                            Column = 7,
                            Row = "E",
                            ScreenId = 3,
                            SeatType = 0
                        },
                        new
                        {
                            Id = 148,
                            Column = 1,
                            Row = "F",
                            ScreenId = 3,
                            SeatType = 1
                        },
                        new
                        {
                            Id = 149,
                            Column = 2,
                            Row = "F",
                            ScreenId = 3,
                            SeatType = 1
                        },
                        new
                        {
                            Id = 150,
                            Column = 3,
                            Row = "F",
                            ScreenId = 3,
                            SeatType = 1
                        },
                        new
                        {
                            Id = 151,
                            Column = 4,
                            Row = "F",
                            ScreenId = 3,
                            SeatType = 1
                        },
                        new
                        {
                            Id = 152,
                            Column = 5,
                            Row = "F",
                            ScreenId = 3,
                            SeatType = 1
                        },
                        new
                        {
                            Id = 153,
                            Column = 6,
                            Row = "F",
                            ScreenId = 3,
                            SeatType = 1
                        },
                        new
                        {
                            Id = 154,
                            Column = 7,
                            Row = "F",
                            ScreenId = 3,
                            SeatType = 1
                        });
                });

            modelBuilder.Entity("CinemaApiCase.Models.SeatBooking", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("BookingTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("SeatId")
                        .HasColumnType("int");

                    b.Property<int>("ShowtimeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SeatId");

                    b.HasIndex("ShowtimeId");

                    b.ToTable("SeatBooking");
                });

            modelBuilder.Entity("CinemaApiCase.Models.Showtime", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("MovieId")
                        .HasColumnType("int");

                    b.Property<int>("ScreenId")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("MovieId");

                    b.HasIndex("ScreenId");

                    b.ToTable("Showtimes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            MovieId = 1,
                            ScreenId = 1,
                            StartTime = new DateTime(2024, 9, 18, 0, 23, 54, 207, DateTimeKind.Local).AddTicks(9362)
                        },
                        new
                        {
                            Id = 2,
                            MovieId = 2,
                            ScreenId = 1,
                            StartTime = new DateTime(2024, 9, 18, 5, 23, 54, 207, DateTimeKind.Local).AddTicks(9404)
                        },
                        new
                        {
                            Id = 3,
                            MovieId = 2,
                            ScreenId = 2,
                            StartTime = new DateTime(2024, 9, 18, 0, 23, 54, 207, DateTimeKind.Local).AddTicks(9406)
                        },
                        new
                        {
                            Id = 4,
                            MovieId = 3,
                            ScreenId = 2,
                            StartTime = new DateTime(2024, 9, 18, 5, 23, 54, 207, DateTimeKind.Local).AddTicks(9409)
                        },
                        new
                        {
                            Id = 5,
                            MovieId = 1,
                            ScreenId = 2,
                            StartTime = new DateTime(2024, 9, 19, 0, 23, 54, 207, DateTimeKind.Local).AddTicks(9411)
                        },
                        new
                        {
                            Id = 6,
                            MovieId = 3,
                            ScreenId = 3,
                            StartTime = new DateTime(2024, 9, 18, 0, 23, 54, 207, DateTimeKind.Local).AddTicks(9413)
                        });
                });

            modelBuilder.Entity("CinemaApiCase.Models.Screen", b =>
                {
                    b.HasOne("CinemaApiCase.Models.Cinema", "Cinema")
                        .WithMany("Screens")
                        .HasForeignKey("CinemaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cinema");
                });

            modelBuilder.Entity("CinemaApiCase.Models.Seat", b =>
                {
                    b.HasOne("CinemaApiCase.Models.Screen", "Screen")
                        .WithMany()
                        .HasForeignKey("ScreenId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Screen");
                });

            modelBuilder.Entity("CinemaApiCase.Models.SeatBooking", b =>
                {
                    b.HasOne("CinemaApiCase.Models.Seat", "Seat")
                        .WithMany("SeatBookings")
                        .HasForeignKey("SeatId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("CinemaApiCase.Models.Showtime", "Showtime")
                        .WithMany("SeatBookings")
                        .HasForeignKey("ShowtimeId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Seat");

                    b.Navigation("Showtime");
                });

            modelBuilder.Entity("CinemaApiCase.Models.Showtime", b =>
                {
                    b.HasOne("CinemaApiCase.Models.Movie", "Movie")
                        .WithMany("Showtimes")
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CinemaApiCase.Models.Screen", "Screen")
                        .WithMany("Showtimes")
                        .HasForeignKey("ScreenId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Movie");

                    b.Navigation("Screen");
                });

            modelBuilder.Entity("CinemaApiCase.Models.Cinema", b =>
                {
                    b.Navigation("Screens");
                });

            modelBuilder.Entity("CinemaApiCase.Models.Movie", b =>
                {
                    b.Navigation("Showtimes");
                });

            modelBuilder.Entity("CinemaApiCase.Models.Screen", b =>
                {
                    b.Navigation("Showtimes");
                });

            modelBuilder.Entity("CinemaApiCase.Models.Seat", b =>
                {
                    b.Navigation("SeatBookings");
                });

            modelBuilder.Entity("CinemaApiCase.Models.Showtime", b =>
                {
                    b.Navigation("SeatBookings");
                });
#pragma warning restore 612, 618
        }
    }
}
