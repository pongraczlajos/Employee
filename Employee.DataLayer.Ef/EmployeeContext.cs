using Microsoft.EntityFrameworkCore;

namespace Employee.DataLayerEf
{
    public class EmployeeContext : DbContext
    {
        public DbSet<DataLayer.Model.Employee> Employees { get; set; }

        public EmployeeContext()
        {
        }

        public EmployeeContext(DbContextOptions<EmployeeContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DataLayer.Model.Employee>()
                .HasKey(e => e.EmployeeId);
            modelBuilder.Entity<DataLayer.Model.Employee>()
                .Property(e => e.FirstName)
                .IsRequired();
            modelBuilder.Entity<DataLayer.Model.Employee>()
                .Property(e => e.LastName)
                .IsRequired();
        }
    }
}
