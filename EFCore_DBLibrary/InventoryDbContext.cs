using InventoryModels;
using InventoryModels.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;

namespace EFCore_DBLibrary
{
    public class InventoryDbContext : DbContext
    {
        private static IConfigurationRoot _configuration;

        private const string _systemUserId = "2fd28110-93d0-427d-9207-d55dbca680fa";


        public DbSet<Item> Items { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CategoryDetail> CategoryDetails { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Genre> Genres { get; set; } // without adding the ItemGenre entity as a DbSet

        // Procedures
        public DbSet<GetItemsForListingDTO> ItemsForListing { get; set; }

        // Scalar Functions
        public DbSet<AllItemsPipeDelimitedStringDTO> AllItemsOutput { get; set; }
        // Valued-Table Functions
        public DbSet<GetItemsTotalValueDTO> ItemsTotalValue { get; set; }

        // Views
        public DbSet<FullItemDetailDTO> FullItemDetailDtos { get; set; }



        //Add a default constructor if scaffolding is needed
        public InventoryDbContext() { }

        //Add the complex constructor for allowing Dependency Injection
        public InventoryDbContext(DbContextOptions options)
            : base(options)
        {
            //intentionally empty. 
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange:true);
            _configuration = builder.Build();
            var cnstr = _configuration.GetConnectionString("InventoryManager");
            optionsBuilder.UseSqlServer(cnstr);
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Item>()
                .HasMany(x => x.Players)
                .WithMany(p => p.Items)
                .UsingEntity<Dictionary<string, object>>(
                    "ItemPlayers",  // to name the fields as expected
                    ip => ip.HasOne<Player>()
                        .WithMany()
                        .HasForeignKey("PlayerId")
                        .HasConstraintName("FK_ItemPlayer_Players_PlayerId")
                        .OnDelete(DeleteBehavior.Cascade),
                    ip => ip.HasOne<Item>()
                        .WithMany()
                        .HasForeignKey("ItemId")
                        .HasConstraintName("FK_PlayerItem_Items_ItemId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                );


            modelBuilder.Entity<GetItemsForListingDTO>( x =>
            {
                x.HasNoKey();
                x.ToView("ItemsForListing");
            });
                
            //Scalar Function
            modelBuilder.Entity<AllItemsPipeDelimitedStringDTO>(x =>
            {
                x.HasNoKey();
                x.ToView("AllItemsOutput");
            });

            //Valued-Table Function
            modelBuilder.Entity<GetItemsTotalValueDTO>(x =>
            {
                x.HasNoKey();
                x.ToView("ItemsTotalValue");
            });

            // View
            modelBuilder.Entity<FullItemDetailDTO>(x =>
            {
                x.HasNoKey();
                x.ToView("FullItemDetailDtos");
            });



            // Seed data to the Genres table using the Fluent API
            // to add that data, need to create a migration
            var genreCreateDate = new DateTime(2024, 01, 01);
            modelBuilder.Entity<Genre>(x =>
            {
                x.HasData(
                new Genre()
                {
                    Id = 1,
                    CreatedDate = genreCreateDate,
                    CreatedByUserId = _systemUserId,
                    IsActive = true,
                    IsDeleted = false,
                    Name = "Fantasy"
                },
                new Genre()
                {
                    Id = 2,
                    CreatedDate = genreCreateDate,
                    CreatedByUserId = _systemUserId,
                    IsActive = true,
                    IsDeleted = false,
                    Name = "Sci/Fi"
                },
                new Genre()
                {
                    Id = 3,
                    CreatedDate = genreCreateDate,
                    CreatedByUserId = _systemUserId,
                    IsActive = true,
                    IsDeleted = false,
                    Name = "Horror"
                },
                new Genre()
                {
                    Id = 4,
                    CreatedDate = genreCreateDate,
                    CreatedByUserId = _systemUserId,
                    IsActive = true,
                    IsDeleted = false,
                    Name = "Comedy"
                },
                new Genre()
                {
                    Id = 5,
                    CreatedDate = genreCreateDate,
                    CreatedByUserId = _systemUserId,
                    IsActive = true,
                    IsDeleted = false,
                    Name = "Drama"
                }
                );
            });

            /*
            [Index(nameof(ItemId), nameof(GenreId), IsUnique = true)]   in ItemGenre.cs
            OR
            modelBuilder.Entity<ItemGenre>()
                .HasIndex(ig => new { ig.ItemId, ig.GenreId })
                .IsUnique()
                .IsClustered(false);
            */
        }


        public override int SaveChanges()
        {
            var tracker = ChangeTracker;
            foreach (var entry in tracker.Entries())
            {
                if (entry.Entity is FullAuditModel)
                {
                    var referenceEntity = entry.Entity as FullAuditModel;
                    switch (entry.State) 
                    {
                        case EntityState.Added:
                            referenceEntity.CreatedDate = DateTime.Now;
                            if (string.IsNullOrWhiteSpace(referenceEntity.CreatedByUserId))
                            {
                                referenceEntity.CreatedByUserId = _systemUserId;
                            }
                            break;
                        case EntityState.Deleted:
                        case EntityState.Modified:
                            referenceEntity.LastModifiedDate = DateTime.Now;
                            if (string.IsNullOrWhiteSpace(referenceEntity.LastModifiedUserId))
                            {
                                referenceEntity.LastModifiedUserId = _systemUserId;
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
            return base.SaveChanges();
        }




    }
}
