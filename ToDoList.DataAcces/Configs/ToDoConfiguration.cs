using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToDoList.Models.Entities;
using ToDoList.Models.Enums;

namespace ToDoList.DataAcces.Configs;

public class ToDoConfiguration : IEntityTypeConfiguration<ToDo>
{
    public void Configure(EntityTypeBuilder<ToDo> builder)
    {
        builder.ToTable("ToDos").HasKey(x => x.Id);

        builder.Property(t => t.Id).HasColumnName("ToDoId");
        builder.Property(t => t.Title).HasColumnName("Title");
        builder.Property(t => t.Description).HasColumnName("Description");
        builder.Property(t => t.Completed).HasColumnName("Completed");
        builder.Property(t => t.CreatedDate).HasColumnName("CrateTime");
        builder.Property(t => t.UpdatedDate).HasColumnName("UpdateTime");
        builder.Property(t => t.StartDate).HasColumnName("StartDate");
        builder.Property(t => t.EndDate).HasColumnName("EndDate");
        builder.Property(t => t.UserId).HasColumnName("User_Id");
        builder.Property(t => t.CategoryId).HasColumnName("Category_Id");
        builder.Property(t => t.Priority)
            .HasColumnName("Priority")
            .HasConversion(
                t => t.ToString(),                     // Enum'u string'e dönüştür
                t => (Priority)Enum.Parse(typeof(Priority), t) // String'i enum'a dönüştür
            );

        builder
           .HasOne(t => t.User)                  //User var
           .WithMany(t => t.ToDos)               //Todosları var 
           .HasForeignKey(t => t.UserId)       //ForeignKey verdik 
           .OnDelete(DeleteBehavior.NoAction);     //CasCade hatası almamak adına.

        builder
           .HasOne(t => t.Category)
           .WithMany(t => t.ToDos)
           .HasForeignKey(t => t.CategoryId)
           .OnDelete(DeleteBehavior.NoAction);

        builder.Navigation(x => x.User).AutoInclude();
        builder.Navigation(x => x.Category).AutoInclude();
    }
}
