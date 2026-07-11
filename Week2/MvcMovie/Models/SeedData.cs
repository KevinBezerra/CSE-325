using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MvcMovie.Data;
using System;
using System.Linq;

namespace MvcMovie.Models;

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new MvcMovieContext(
            serviceProvider.GetRequiredService<DbContextOptions<MvcMovieContext>>()))
        {
            if (context.Movie.Any())
            {
                return; 
            }

            context.Movie.AddRange(
                new Movie
                {
                    Title = "The Lord of the Rings: The Fellowship of the Ring",
                    ReleaseDate = DateTime.Parse("2001-12-19"),
                    Genre = "Fantasy",
                    Rating = "PG-13",
                    Price = 12.99M
                },
                new Movie
                {
                    Title = "Dungeons & Dragons: Honor Among Thieves",
                    ReleaseDate = DateTime.Parse("2023-03-31"),
                    Genre = "Fantasy",
                    Rating = "PG-13",
                    Price = 14.99M
                },
                new Movie
                {
                    Title = "Interstellar",
                    ReleaseDate = DateTime.Parse("2014-11-07"),
                    Genre = "Sci-Fi",
                    Rating = "PG-13",
                    Price = 9.99M
                },
                new Movie
                {
                    Title = "Inception",
                    ReleaseDate = DateTime.Parse("2010-07-16"),
                    Genre = "Sci-Fi",
                    Rating = "PG-13",
                    Price = 7.99M
                }
            );
            context.SaveChanges();
        }
    }
}