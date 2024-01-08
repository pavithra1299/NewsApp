using UserRegistration.Models;
using System;
using Microsoft.EntityFrameworkCore;

namespace UserRegistration.Repository
{
    public class UserDbContext:DbContext
    {
        public UserDbContext() { }
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
    }
}
