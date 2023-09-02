using System;
using System.Collections.Generic;
using BookStore.WebApi.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookStore.WebApi.Models;

public partial class BookStoreAppDbContext : IdentityDbContext<ApiUser>
{
    public BookStoreAppDbContext()
    {
    }

    public BookStoreAppDbContext(DbContextOptions<BookStoreAppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Author> Authors { get; set; }

    public virtual DbSet<Book> Books { get; set; }

    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)

    //    => optionsBuilder.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Author>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Authors__3214EC07E7617A19");

            entity.Property(e => e.Bio).HasMaxLength(250);
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(50);
        });

        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Books__3214EC07ECFDB71A");

            entity.HasIndex(e => e.Isbn, "UQ__Books__447D36EA5490EF71").IsUnique();

            entity.Property(e => e.Image).HasMaxLength(250);
            entity.Property(e => e.Isbn)
                .HasMaxLength(50)
                .HasColumnName("ISBN");
            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)"); 
            entity.Property(e => e.Summery).HasMaxLength(250);
            entity.Property(e => e.Title).HasMaxLength(50);

            entity.HasOne(d => d.Author).WithMany(p => p.Books)
                .HasForeignKey(d => d.AuthorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Books_ToTable");
        });

        var passwordHasher = new PasswordHasher<ApiUser>();
        modelBuilder.Entity<ApiUser>().HasData(
            new ApiUser()
            {
                Id = "B675C141-A780-4571-B5FB-333366B84D77",
                Email = "admin@bookstore.com",
                NormalizedEmail = "ADMIN@BOOKSTORE.COM",
                UserName = "admin@bookstore.com",
                NormalizedUserName = "ADMIN@BOOKSTORE.COM",
                FirstName = "system",
                LastName = "admin",
                PasswordHash = passwordHasher.HashPassword(null, "p@ssword1")
            },
            new ApiUser()
            {
                Id = "E57CB190-4DBE-433A-8A1D-F5C9FCE96663",
                Email = "user@bookstore.com",
                NormalizedEmail = "USER@BOOKSTORE.COM",
                UserName = "user@bookstore.com",
                NormalizedUserName = "USER@BOOKSTORE.COM",
                FirstName = "system",
                LastName = "user",
                PasswordHash = passwordHasher.HashPassword(null, "p@ssword1")
            }
        );

        modelBuilder.Entity<IdentityRole>().HasData(
            new IdentityRole()
            {
                Name = "Admin",
                NormalizedName = "ADMIN",
                Id = "2EC3020A-3C29-4BB3-AEC6-68733E6485CB"
            },
            new IdentityRole()
            {
                Name = "User",
                NormalizedName = "USER",
                Id = "EB29ACBA-8D33-400A-95FD-ED32114DF66E"
            }
        );

        modelBuilder.Entity<IdentityUserRole<string>>().HasData(
            new IdentityUserRole<string>()
            {
                RoleId = "2EC3020A-3C29-4BB3-AEC6-68733E6485CB",
                UserId = "B675C141-A780-4571-B5FB-333366B84D77"
            },
            new IdentityUserRole<string>()
            {
                RoleId = "EB29ACBA-8D33-400A-95FD-ED32114DF66E",
                UserId = "E57CB190-4DBE-433A-8A1D-F5C9FCE96663"
            }
        );

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
