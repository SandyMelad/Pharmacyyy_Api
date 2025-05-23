using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Pharmacy_ASP_API.Models.Entities;

namespace Pharmacy_ASP_API.Models
{
    public class PharmacyDbContext : DbContext
    {
        public PharmacyDbContext(DbContextOptions<PharmacyDbContext> options): base(options)
        {
        }

        public DbSet<Entities.Stock> Stocks { get; set; }
        public DbSet<Entities.Report> Reports { get; set; }
        public DbSet<Entities.Patient> Patients { get; set; }
        public DbSet<Entities.Order> Orders { get; set; }
        public DbSet<Entities.MedicationRequest> MedicationRequests { get; set; }
        public DbSet<Entities.MedicationKnowledge> MedicationKnowledges { get; set; }
        public DbSet<Entities.Finance> Finances { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Explicitly configure primary keys
            modelBuilder.Entity<Entities.Stock>()
            .HasKey(s => s.StockId);

            modelBuilder.Entity<Entities.Report>()
            .HasKey(r => r.ReportId);

            modelBuilder.Entity<Entities.Patient>()
            .HasKey(p => p.PatientId);

            modelBuilder.Entity<Entities.Order>()
            .HasKey(o => o.OrderId);

            modelBuilder.Entity<Entities.MedicationRequest>()
            .HasKey(mr => mr.RequestId);

            modelBuilder.Entity<Entities.MedicationKnowledge>()
            .HasKey(mk => mk.MedicationId);
            
            modelBuilder.Entity<Entities.Finance>()
            .HasKey(finance => new { finance.ReportId, finance.OrderId, finance.PatientId });

            // Configure relationships
            // Patient - Order (One-to-Many)
            modelBuilder.Entity<Entities.Order>()
                .HasOne(o => o.Patient)
                .WithMany(p => p.Orders)
                .HasForeignKey(o => o.PatientId);

            // Stock - Order (One-to-Many)
            modelBuilder.Entity<Entities.Stock>()
                .HasMany(s => s.Orders)
                .WithOne(o => o.Stock)
                .HasForeignKey(o => o.StockId);

            // Stock - MedicationKnowledge (One-to-Many)
            modelBuilder.Entity<Entities.Stock>()
                .HasMany(s => s.MedicationKnowledges)
                .WithOne(mk => mk.Stock)
                .HasForeignKey(mk => mk.StockId);

            // Report - Order (One-to-Many)
            modelBuilder.Entity<Entities.Report>()
                .HasMany(r => r.Orders)
                .WithOne(o => o.Report)
                .HasForeignKey(o => o.ReportId);

            // Configure many-to-many relationship between MedicationKnowledge and Order
            modelBuilder.Entity<MedicationKnowledge>()
                .HasMany(m => m.Orders)
                .WithMany(o => o.MedicationKnowledges)
                .UsingEntity(j => j.ToTable("MedicationKnowledgeOrders"));

            // Configure one-to-one relationship between Order and MedicationRequest
            modelBuilder.Entity<Order>()
                .HasOne(o => o.MedicationRequest)
                .WithOne()
                .HasForeignKey<MedicationRequest>(mr => mr.OrderId);

            // Configure one-to-many relationship between Order and MedicationKnowledge
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Medication)
                .WithMany()
                .HasForeignKey(o => o.MedicationId);

            // Configure one-to-many relationship between Patient and Finance
            modelBuilder.Entity<Patient>()
                .HasMany(p => p.Finances)
                .WithOne()
                .HasForeignKey("PatientId");
        }
    }
}
