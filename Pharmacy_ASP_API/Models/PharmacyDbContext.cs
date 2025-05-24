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

        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<MedicationRequest> MedicationRequests { get; set; }
        public DbSet<MedicationKnowledge> MedicationKnowledges { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure Stock - MedicationKnowledge relationship (One-to-Many)
            modelBuilder.Entity<Stock>()
                .HasMany(s => s.MedicationKnowledges)
                .WithOne(m => m.Stock)
                .HasForeignKey(m => m.StockId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure Order relationships
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Patient)
                .WithMany(p => p.Orders)
                .HasForeignKey(o => o.PatientId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Medication)
                .WithMany(m => m.Orders)
                .HasForeignKey(o => o.MedicationId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Stock)
                .WithMany(s => s.Orders)
                .HasForeignKey(o => o.StockId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Report)
                .WithMany(r => r.Orders)
                .HasForeignKey(o => o.ReportId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure Order - MedicationRequest relationship (One-to-One)
            modelBuilder.Entity<Order>()
                .HasOne(o => o.MedicationRequest)
                .WithOne(mr => mr.Order)
                .HasForeignKey<Order>(o => o.MedicationRequestId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
