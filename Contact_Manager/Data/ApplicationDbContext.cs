using Contact_Manager.Models;
using Humanizer;
using Microsoft.EntityFrameworkCore;

namespace Contact_Manager.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
            .Property(e => e.Salary)
            .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<Employee>().HasData(
                new Employee { Id = 1, Name = "Albus Dumbledore", DateOfBirth = new DateOnly(1881, 06, 30), Married = false, Phone = "0101221232", Salary = 10000M },
                new Employee { Id = 2, Name = "Severus Snape", DateOfBirth = new DateOnly(1960, 09, 01), Married = false, Phone = "0104324323", Salary = 7050M },
                new Employee { Id = 3, Name = "Minerva McGonagall", DateOfBirth = new DateOnly(1890, 04, 10), Married = true, Phone = "0105676567", Salary = 7070M }
                );
        }
    }
}
