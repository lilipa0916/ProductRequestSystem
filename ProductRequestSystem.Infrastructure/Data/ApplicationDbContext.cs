using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProductRequestSystem.Domain.Entities;
using ProductRequestSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductRequestSystem.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<ProductRequest> ProductRequests => Set<ProductRequest>();
        public DbSet<Offer> Offers => Set<Offer>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // User Configuration
            builder.Entity<User>(entity =>
            {
                entity.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .IsRequired();

                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .IsRequired();

                entity.Property(e => e.Role)
                    .HasConversion<int>()
                    .IsRequired();

                entity.Property(e => e.CreatedAt)
                    .IsRequired();
            });

            // ProductRequest Configuration
            builder.Entity<ProductRequest>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.ProductName)
                    .HasMaxLength(100)
                    .IsRequired();

                entity.Property(e => e.Description)
                    .HasMaxLength(500)
                    .IsRequired();

                entity.Property(e => e.Quantity)
                    .IsRequired();

                entity.Property(e => e.RequiredDate)
                    .IsRequired();

                entity.Property(e => e.Status)
                    .HasConversion<int>()
                    .IsRequired();

                entity.Property(e => e.CreatedAt)
                    .IsRequired();

                entity.Property(e => e.ClientId)
                    .IsRequired();

                // Relationships
                entity.HasOne(e => e.Client)
                    .WithMany(u => u.ProductRequests)
                    .HasForeignKey(e => e.ClientId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(e => e.Offers)
                    .WithOne(o => o.ProductRequest)
                    .HasForeignKey(o => o.ProductRequestId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Offer Configuration
            builder.Entity<Offer>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Price)
                    .HasPrecision(18, 2)
                    .IsRequired();

                entity.Property(e => e.EstimatedDays)
                    .IsRequired();

                entity.Property(e => e.Comments)
                    .HasMaxLength(500);

                entity.Property(e => e.Status)
                    .HasConversion<int>()
                    .IsRequired();

                entity.Property(e => e.CreatedAt)
                    .IsRequired();

                entity.Property(e => e.ProductRequestId)
                    .IsRequired();

                entity.Property(e => e.ProviderId)
                    .IsRequired();

                // Relationships
                entity.HasOne(e => e.Provider)
                    .WithMany(u => u.Offers)
                    .HasForeignKey(e => e.ProviderId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.ProductRequest)
                    .WithMany(pr => pr.Offers)
                    .HasForeignKey(e => e.ProductRequestId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Seed Data
            SeedData(builder);
        }

        private static void SeedData(ModelBuilder builder)
        {
            // Seed some test users
            var hasher = new Microsoft.AspNetCore.Identity.PasswordHasher<User>();

            var clientUser = new User
            {
                Id = "client-1",
                UserName = "client@test.com",
                NormalizedUserName = "CLIENT@TEST.COM",
                Email = "client@test.com",
                NormalizedEmail = "CLIENT@TEST.COM",
                EmailConfirmed = true,
                FirstName = "Juan",
                LastName = "Pérez",
                Role = UserRole.Client,
                CreatedAt = DateTime.UtcNow
            };
            clientUser.PasswordHash = hasher.HashPassword(clientUser, "Password123!");

            var providerUser = new User
            {
                Id = "provider-1",
                UserName = "provider@test.com",
                NormalizedUserName = "PROVIDER@TEST.COM",
                Email = "provider@test.com",
                NormalizedEmail = "PROVIDER@TEST.COM",
                EmailConfirmed = true,
                FirstName = "María",
                LastName = "García",
                Role = UserRole.Provider,
                CreatedAt = DateTime.UtcNow
            };
            providerUser.PasswordHash = hasher.HashPassword(providerUser, "Password123!");

            builder.Entity<User>().HasData(clientUser, providerUser);
        }
    }
}
