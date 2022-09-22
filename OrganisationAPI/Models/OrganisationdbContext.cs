using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace OrganisationAPI.Models
{
    public partial class OrganisationdbContext : DbContext
    {
        public OrganisationdbContext()
        {
        }

        public OrganisationdbContext(DbContextOptions<OrganisationdbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Department> Departments { get; set; } = null!;
        public virtual DbSet<DepartmentToEmployee> DepartmentToEmployees { get; set; } = null!;
        public virtual DbSet<Employee> Employees { get; set; } = null!;

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {
        //        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=organisationdb;Username=postgres;Password=secret");
        //    }
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Department>(entity =>
            {
                entity.Property(e => e.Id).UseIdentityAlwaysColumn();

                entity.Property(e => e.Name).HasMaxLength(100);
            });

            modelBuilder.Entity<DepartmentToEmployee>(entity =>
            {
                entity.ToTable("DepartmentToEmployee");

                entity.Property(e => e.Id).UseIdentityAlwaysColumn();
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.ToTable("Employees");

                entity.Property(e => e.Id).UseIdentityAlwaysColumn();

                entity.Property(e => e.Name).HasMaxLength(100);

                entity.Property(e => e.Post).HasMaxLength(100);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
