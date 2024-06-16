using AspMedSystem.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspMedSystem.DataAccess
{
    public class MedSystemContext : DbContext
    {
        private readonly string _connectionString;

        public MedSystemContext()
        {
            _connectionString = "Server=localhost\\SQLEXPRESS;Database=AspMedSystem;Trusted_Connection=True;TrustServerCertificate=true";
        }

        public MedSystemContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);

            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            IEnumerable<EntityEntry> entries = this.ChangeTracker.Entries();

            foreach (EntityEntry entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    if (entry.Entity is Entity e)
                    {
                        e.CreatedAt = DateTime.UtcNow;
                    }

                    if (entry.Entity is PermissionEffect permissionEffect)
                    {
                        permissionEffect.Permission = permissionEffect.Permission.ToLower();
                    }
                }

                if (entry.State == EntityState.Modified)
                {
                    if (entry.Entity is PermissionEffect permissionEffect)
                    {
                        permissionEffect.Permission = permissionEffect.Permission.ToLower();
                    }
                }
            }

            return base.SaveChanges();
        }

        public DbSet<Group> Groups { get; set; }
        public DbSet<GroupPermission> GroupPermissions { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserPermission> UserPermissions { get; set; }
        public DbSet<ExaminationTerm> ExaminationTerms { get; set; }
        public DbSet<Examination> Examinations { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<TreatmentCounterindication> TreatmentCounterindications { get; set;}
        public DbSet<UserTreatment> UserTreatments { get; set; }

        public DbSet<UseCaseLog> UseCaseLogs { get; set; }

        public DbSet<ErrorLog> ErrorLogs { get; set; }
    }
}
