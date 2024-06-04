using dotNet_RESTful_Web_API.models;
using dotNet_RESTful_Web_API.models.Dto;
using Microsoft.EntityFrameworkCore;

namespace dotNet_RESTful_Web_API.Data;

public class AppDbContext : DbContext
{
     public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
     {
     }

     public DbSet<User> Users { get; set; }
     public DbSet<UserNumber> UserNumbers { get; set; }

     protected override void OnModelCreating(ModelBuilder modelBuilder)
     {
          modelBuilder.Entity<User>().HasData(
                new User()
                {
                    Id = 1,
                    Age = 10,
                    Disability = true,
                    Name = "u1",
                    Email = "u1@u1",
                    Password = "u1",
                    ImageUrl = "https://fakeimg.pl/300/"
                },
                new User()
                {
                    Id = 2,
                    Age = 20,
                    Disability = false,
                    Name = "u2",
                    Email = "u2@u2",
                    Password = "u2",
                    ImageUrl = "https://fakeimg.pl/300/"
                },
                new User()
                {
                    Id = 3,
                    Age = 30,
                    Disability = true,
                    Name = "u3",
                    Email = "u3@u3",
                    Password = "u3",
                    ImageUrl = "https://fakeimg.pl/300/"
                },
                new User()
                {
                    Id = 4,
                    Age = 40,
                    Disability = false,
                    Name = "u4",
                    Email = "u4@u4",
                    Password = "u4",
                    ImageUrl = "https://fakeimg.pl/300/"
                },
                new User()
                {
                    Id = 5,
                    Age = 50,
                    Disability = true,
                    Name = "u5",
                    Email = "u5@u5",
                    Password = "u5",
                    ImageUrl = "https://fakeimg.pl/300/"
                },
                new User()
                {
                    Id = 6,
                    Age = 60,
                    Disability = false,
                    Name = "u6",
                    Email = "u6@u6",
                    Password = "u6",
                    ImageUrl = "https://fakeimg.pl/300/"
                },
                new User()
                {
                    Id = 7,
                    Age = 70,
                    Disability = true,
                    Name = "u7",
                    Email = "u7@u7",
                    Password = "u7",
                    ImageUrl = "https://fakeimg.pl/300/"
                },
                new User()
                {
                    Id = 8,
                    Age = 80,
                    Disability = false,
                    Name = "u8",
                    Email = "u8@u8",
                    Password = "u8",
                    ImageUrl = "https://fakeimg.pl/300/"
                },
                new User()
                {
                    Id = 9,
                    Age = 90,
                    Disability = true,
                    Name = "u9",
                    Email = "u9@u9",
                    Password = "u9",
                    ImageUrl = "https://fakeimg.pl/300/"
                },
                new User()
                {
                    Id = 10,
                    Age = 100,
                    Disability = false,
                    Name = "u10",
                    Email = "u10@u10",
                    Password = "u10",
                    ImageUrl = "https://fakeimg.pl/300/"
                }
          );
     }
     
     
}