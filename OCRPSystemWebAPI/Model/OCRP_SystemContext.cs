using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace OCRPSystemWebAPI.Model
{
    public partial class OCRP_SystemContext : DbContext
    {
        public OCRP_SystemContext()
        {
        }

        public OCRP_SystemContext(DbContextOptions<OCRP_SystemContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<Recipe> Recipe { get; set; }
        public virtual DbSet<State> State { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("server=.\\SQLExpress;Trusted_Connection=True;Database=OCRP_System");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.CategoryId).HasColumnName("Category_ID");

                entity.Property(e => e.CategoryName)
                    .IsRequired()
                    .HasColumnName("Category_Name")
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Recipe>(entity =>
            {
                entity.Property(e => e.RecipeId).HasColumnName("Recipe_Id");

                entity.Property(e => e.CategoryId).HasColumnName("Category_ID");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnType("text");

                entity.Property(e => e.ImageUrl)
                    .HasColumnName("Image_Url")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Ingredients).IsUnicode(false);

                entity.Property(e => e.RecipeStatus)
                    .IsRequired()
                    .HasColumnName("Recipe_status")
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('Pending')");

                entity.Property(e => e.RecipeSteps)
                    .HasColumnName("Recipe_steps")
                    .IsUnicode(false);

                entity.Property(e => e.StateId).HasColumnName("State_ID");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UserId).HasColumnName("User_ID");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Recipe)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Recipe__Category__46E78A0C");

                entity.HasOne(d => d.State)
                    .WithMany(p => p.Recipe)
                    .HasForeignKey(d => d.StateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Recipe__State_ID__48CFD27E");

                //entity.HasOne(d => d.User)
                //    .WithMany(p => p.Recipe)
                //    .HasForeignKey(d => d.UserId)
                //    .OnDelete(DeleteBehavior.ClientSetNull)
                //    .HasConstraintName("FK__Recipe__User_ID__47DBAE45");
            });

            modelBuilder.Entity<State>(entity =>
            {
                entity.Property(e => e.StateId).HasColumnName("state_ID");

                entity.Property(e => e.StateName)
                    .IsRequired()
                    .HasColumnName("State_Name")
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK__Users__206D9190ED116D69");

                entity.Property(e => e.UserId).HasColumnName("User_ID");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasColumnName("User_Name")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.UserType)
                    .IsRequired()
                    .HasColumnName("User_Type")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.EmailId)
                   .IsRequired()
                   .HasColumnName("Email_Id")
                   .HasMaxLength(255)
                   .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
