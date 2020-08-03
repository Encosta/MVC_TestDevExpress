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

        public virtual DbSet<TreeList> TreeList { get; set; }

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
            modelBuilder.Entity<TreeList>(entity =>
            {
                entity.HasKey(e => e.TreeListId);

                entity.Property(e => e.TreeListId)
                    .HasColumnName("TreeListID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.Id).HasMaxLength(50);

                entity.Property(e => e.Items).HasMaxLength(50);

                entity.Property(e => e.ParentId).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
