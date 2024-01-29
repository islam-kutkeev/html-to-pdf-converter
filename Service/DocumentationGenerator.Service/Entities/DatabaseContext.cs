using DocumentationGenerator.Service.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DocumentationGenerator.Service.Entities;

public class DatabaseContext : DbContext
{
    public DatabaseContext() { }

    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

    public virtual DbSet<FileEntity> Files { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<FileEntity>(entity =>
            {
                entity.Property(e => e.Status)
                    .HasConversion(
                        s => s.ToString(),
                        s => (FileStatus)Enum.Parse(typeof(FileStatus), s));

                entity.Property(e => e.Id)
                    .HasConversion(new GuidToStringConverter());

                entity.Property(e => e.CreatedAt)
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");
            });
    }
}
