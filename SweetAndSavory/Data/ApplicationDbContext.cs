using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SweetAndSavory.Models;
namespace SweetAndSavory.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public virtual DbSet<Flavor> Flavors { get; set; }
        public DbSet<Treat> Treats { get; set; }
        public DbSet<FlavorTreat> FlavorTreat { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
