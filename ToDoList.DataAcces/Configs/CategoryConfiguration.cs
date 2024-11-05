using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToDoList.Models.Entities;

namespace ToDoList.DataAcces.Configs;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("Categories").HasKey(c=>c.Id);

        builder.Property(c => c.Id).HasColumnName("CategoryId");
        builder.Property(c => c.CreatedDate).HasColumnName("CrateTime");
        builder.Property(c => c.UpdatedDate).HasColumnName("UpdateTime");
        builder.Property(c=>c.Name).HasColumnName("CategoryName");

        builder.HasMany(x=>x.ToDos)
            .WithOne(x=>x.Category)
            .HasForeignKey(x=>x.CategoryId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
