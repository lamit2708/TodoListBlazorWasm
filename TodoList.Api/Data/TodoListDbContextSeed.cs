using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using TodoList.Api.Entities;
using TodoList.Models.Enums;
using Task = System.Threading.Tasks.Task;

namespace TodoList.Api.Data
{
    public class TodoListDbContextSeed
    {
        private readonly IPasswordHasher<User> _passwordHasher = new PasswordHasher<User>();

        public async Task SeedAsync(TodoListDbContext context, ILogger<TodoListDbContextSeed> logger)
        {
            if (!context.Users.Any())
            {
                var user = new User()
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Mr",
                    LastName = "A",
                    Email = "lam@gmail.com",
                    NormalizedEmail = "lam@GMAIL.COM",
                    PhoneNumber = "032132131",
                    UserName = "admin",
                    NormalizedUserName = "ADMIN",
                    SecurityStamp = Guid.NewGuid().ToString()
                };
                user.PasswordHash = _passwordHasher.HashPassword(user, "Admin@123$");
                context.Users.Add(user);
            }

            if (!context.Tasks.Any())
            {
                for (var i=0;i< 1000;i++)
                {
                    context.Tasks.Add(new Entities.Task()
                    {
                        Id = Guid.NewGuid(),
                        Name = $"Same tasks {i+2}",
                        
                        CreatedDate = DateTime.Now,
                        Priority = Priority.High,
                        Status = Status.Open
                    });
                }
               
            }

            await context.SaveChangesAsync();
        }
    }
}
