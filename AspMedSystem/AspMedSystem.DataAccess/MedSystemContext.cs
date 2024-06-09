using AspMedSystem.Domain;
using Microsoft.EntityFrameworkCore;
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

        public DbSet<Group> Groups { get; set; }
        public DbSet<GroupPermission> GroupPermissions { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserPermission> UserPermissions { get; set; }
        public DbSet<ExaminationTerm> ExaminationTerms { get; set; }
        public DbSet<Examination> Examinations { get; set; }
        public DbSet<Report> Reports { get; set; }

        public DbSet<TreatmentCounterindication> TreatmentCounterindications { get; set;}
    }
}
