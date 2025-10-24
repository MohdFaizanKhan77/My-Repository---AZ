using EmolyeePortal.Models;
using Microsoft.EntityFrameworkCore;

namespace EmolyeePortal.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Designation> Designations { get; set; }

        public DbSet<EmployeeType> EmployeeTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //prevent cascade delete on designation fk
            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Designation)
                .WithMany()
                .HasForeignKey(e => e.DesignationId)
                .OnDelete(DeleteBehavior.Restrict);

            //Seed EmployeeTypes
            modelBuilder.Entity<EmployeeType>().HasData(
                new EmployeeType { Id = 1, Name = "Permanent" },
                new EmployeeType { Id = 2, Name = "Temporary" },
                new EmployeeType { Id = 3, Name = "Contract" },
                new EmployeeType { Id = 4, Name = "Intern" }
                );

            //Seed Departments
            modelBuilder.Entity<Department>().HasData(
                new EmployeeType { Id = 1, Name = "IT" },
                new EmployeeType { Id = 2, Name = "HR" },
                new EmployeeType { Id = 3, Name = "Sales" },
                new EmployeeType { Id = 4, Name = "Admin" }
                );

            //Seed Designation with DeparmentId
            modelBuilder.Entity<Designation>().HasData(
                new Designation { Id = 1, Name = "Software Developer", DepartmentId = 1 },
                new Designation { Id = 2, Name = "System Administrator", DepartmentId = 1 },
                new Designation { Id = 3, Name = "NetWork  Engineer", DepartmentId = 1 },

                new Designation { Id = 4, Name = "HR Specialist", DepartmentId = 2 },
                new Designation { Id = 5, Name = "HR Manager", DepartmentId = 2 },
                new Designation { Id = 6, Name = "Talent Acquisition Coordinator", DepartmentId = 2 },

                new Designation { Id = 7, Name = "Sales Executive ", DepartmentId = 3 },
                new Designation { Id = 8, Name = "Sales Manager ", DepartmentId = 3 },
                new Designation { Id = 9, Name = "Account Executive ", DepartmentId = 3 },

                new Designation { Id = 10, Name = "Office Manager ", DepartmentId = 4 },
                new Designation { Id = 11, Name = "Executive Assistence ", DepartmentId = 4 },
                new Designation { Id = 12, Name = "Receptionist ", DepartmentId = 4 }
                );
            //Seed initial Data
            modelBuilder.Entity<Employee>().HasData(
                new Employee { Id = 1, FullName = "John Snow", Email = "John@example.com", DepartmentId = 1, DesignationId = 1, HireDate = new DateTime(2020, 1, 30), DateOfBirth = new DateTime(1990, 3, 12), EmployeeTypeId = 1, Gender = "Male", Salary = 60000m },
                new Employee { Id = 2, FullName = "Patrik BedDavid", Email = "Patrik@example.com", DepartmentId = 2, DesignationId = 3, HireDate = new DateTime(2018, 4, 19), DateOfBirth = new DateTime(1985, 8, 6), EmployeeTypeId = 1, Gender = "Female", Salary = 80000m },
                new Employee { Id = 3, FullName = "Shamil", Email = "Shamil@example.com", DepartmentId = 3, DesignationId = 5, HireDate = new DateTime(2019, 4, 28), DateOfBirth = new DateTime(1991, 6, 9), EmployeeTypeId = 3, Gender = "Male", Salary = 61000m },
                new Employee { Id = 4, FullName = "Chris Willx", Email = "Chris@example.com", DepartmentId = 4, DesignationId = 8, HireDate = new DateTime(2020, 12, 22), DateOfBirth = new DateTime(1993, 11, 12), EmployeeTypeId = 3, Gender = "Male", Salary = 30000m },
                new Employee { Id = 5, FullName = "Hamza Ahmed", Email = "Hamza@example.com", DepartmentId = 1, DesignationId = 9, HireDate = new DateTime(2017, 11, 14), DateOfBirth = new DateTime(1982, 7, 17), EmployeeTypeId = 2, Gender = "Female", Salary = 67000m },
                new Employee { Id = 6, FullName = "Iman Gadzi", Email = "Iman@example.com", DepartmentId = 2, DesignationId = 10, HireDate = new DateTime(2020, 6, 19), DateOfBirth = new DateTime(1984, 5, 18), EmployeeTypeId = 1, Gender = "Male", Salary = 63000m },
                new Employee { Id = 7, FullName = "David Goggins", Email = "David@example.com", DepartmentId = 3, DesignationId = 12, HireDate = new DateTime(2021, 3, 20), DateOfBirth = new DateTime(1993, 3, 22), EmployeeTypeId = 2, Gender = "Female", Salary = 69000m },
                new Employee { Id = 8, FullName = "Adrew T", Email = "Andrew@example.com", DepartmentId = 4, DesignationId = 9, HireDate = new DateTime(2018, 7, 17), DateOfBirth = new DateTime(1994, 2, 8), EmployeeTypeId = 1, Gender = "Male", Salary = 68000m },
                new Employee { Id = 9, FullName = "Tristan T", Email = "Tristan@example.com", DepartmentId = 1, DesignationId = 7, HireDate = new DateTime(2019, 8, 21), DateOfBirth = new DateTime(1996, 12, 15), EmployeeTypeId = 3, Gender = "Female", Salary = 65000m },
                new Employee { Id = 10, FullName = "Luke Balmer", Email = "Luke@example.com", DepartmentId = 2, DesignationId = 4, HireDate = new DateTime(2022, 11, 26), DateOfBirth = new DateTime(1988, 10, 21), EmployeeTypeId = 1, Gender = "Male", Salary = 72000m },
                new Employee { Id = 11, FullName = "Khabib NM", Email = "khabib@example.com", DepartmentId = 3, DesignationId = 6, HireDate = new DateTime(2021, 10, 18), DateOfBirth = new DateTime(1987, 4, 16), EmployeeTypeId = 1, Gender = "Male", Salary = 76000m },
                new Employee { Id = 12, FullName = "John Jones", Email = "Jones@example.com", DepartmentId = 4, DesignationId = 5, HireDate = new DateTime(2019, 12, 23), DateOfBirth = new DateTime(1995, 5, 11), EmployeeTypeId = 2, Gender = "Female", Salary = 74000m },
                new Employee { Id = 13, FullName = "Mike Tyson", Email = "Mike@example.com", DepartmentId = 1, DesignationId = 2, HireDate = new DateTime(2018, 11, 25), DateOfBirth = new DateTime(1997, 9, 12), EmployeeTypeId = 1, Gender = "Male", Salary = 79000m },
                new Employee { Id = 14, FullName = "Bruce Wayne", Email = "Bruce@example.com", DepartmentId = 2, DesignationId = 9, HireDate = new DateTime(2017, 9, 21), DateOfBirth = new DateTime(1989, 3, 23), EmployeeTypeId = 1, Gender = "Female", Salary = 75000m },
                new Employee { Id = 15, FullName = "Batman", Email = "Batman@example.com", DepartmentId = 3, DesignationId = 4, HireDate = new DateTime(2021, 5, 20), DateOfBirth = new DateTime(1992, 12, 25), EmployeeTypeId = 3, Gender = "Male", Salary = 62000m }
                 );

        }
    }
}
