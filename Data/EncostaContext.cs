using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using MVC_Test.Models;

namespace MVC_Test.Data
{
    public partial class EncostaContext : DbContext
    {
        public EncostaContext()
        {
        }

        public EncostaContext(DbContextOptions<EncostaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<BillOfMaterialsExpanded> BillOfMaterialsExpanded { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=server3;Database=Encosta;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BillOfMaterialsExpanded>(entity =>
            {
                entity.Property(e => e.BillOfMaterialsExpandedId)
                    .HasColumnName("BillOfMaterialsExpandedID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.BomDate).HasColumnType("date");

                entity.Property(e => e.Bomlevel).HasColumnName("BOMLevel");

                entity.Property(e => e.Bomreference)
                    .HasColumnName("BOMReference")
                    .HasMaxLength(100);

                entity.Property(e => e.Bomrelease)
                    .HasColumnName("BOMRelease")
                    .HasMaxLength(100);

                entity.Property(e => e.ComponentDescription).IsRequired();

                entity.Property(e => e.ComponentItem)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.FullSequence).HasMaxLength(100);

                entity.Property(e => e.HasChild).HasMaxLength(1);

                entity.Property(e => e.LineCost).HasColumnType("numeric(16, 6)");

                entity.Property(e => e.ParentDescription).IsRequired();

                entity.Property(e => e.ParentId)
                    .HasColumnName("ParentID")
                    .HasMaxLength(50);

                entity.Property(e => e.ParentItem)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.PurchasedOrManufactured).HasMaxLength(1);

                entity.Property(e => e.QuantityPerParent).HasColumnType("numeric(16, 6)");

                entity.Property(e => e.QuantityPerTop).HasColumnType("numeric(16, 6)");

                entity.Property(e => e.ScrapPercentage).HasColumnType("numeric(5, 2)");

                entity.Property(e => e.StandardCost).HasColumnType("numeric(16, 6)");

                entity.Property(e => e.TopLevelDescription).IsRequired();

                entity.Property(e => e.TopLevelItem)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
