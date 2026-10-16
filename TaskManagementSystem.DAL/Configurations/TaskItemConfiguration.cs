using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManagementSystem.Core.Entities;

namespace TaskManagementSystem.DAL.Configurations;

public class TaskItemConfiguration : IEntityTypeConfiguration<TaskItem>
{
  public void Configure(EntityTypeBuilder<TaskItem> builder)
  {
    builder.HasKey(t => t.Id);
    builder.Property(t => t.Title).IsRequired().HasMaxLength(100);
    builder.Property(t => t.Description).HasMaxLength(500);
    builder.Property(t => t.CreatedAt).HasDefaultValueSql("GETDATE()");
  }
}
