using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Presentation.Models;

public partial class TaxTestDbContext : DbContext
{
    public TaxTestDbContext()
    {
    }

    public TaxTestDbContext(DbContextOptions<TaxTestDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TaxPeriod> TaxPeriods { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=(localdb)\\LocalDB;Initial Catalog=TaxTestDB");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TaxPeriod>(entity =>
        {
            entity.Property(e => e.EndTime).HasColumnType("datetime");
            entity.Property(e => e.StartTime).HasColumnType("datetime");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
