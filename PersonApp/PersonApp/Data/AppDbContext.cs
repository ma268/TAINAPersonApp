using Microsoft.EntityFrameworkCore;
using PersonApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Person> Person { get; set; }
        public DbSet<Gender> Gender { get; set; }
    }
}
