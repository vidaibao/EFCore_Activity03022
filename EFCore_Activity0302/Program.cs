using AutoMapper;
using AutoMapper.QueryableExtensions;
using EFCore_DBLibrary;
using InventoryHelpers;
using InventoryModels;
using InventoryModels.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EFCore_Activity0302
{
    public class Program
    {
        private static IConfigurationRoot _configuration;
        private static DbContextOptionsBuilder<InventoryDbContext> _optionsBuilder;

        private const string _systemUserId = "2fd28110-93d0-427d-9207-d55dbca680fa";
        private const string _loggedInUserId = "e2eb8989-a81a-4151-8e86-eb95a7961da2";

        // AutoMapper
        private static MapperConfiguration _mapperConfig;
        private static IMapper _mapper;
        private static IServiceProvider _serviceProvider;


        static void Main(string[] args)
        {
            BuildOptions();
            BuildMapper();


            //DeleteAllItems();
            //EnsureItems();
            //UpdateItems();

            //ListInventory();

            GetItemsForListingLinqDTO();
            
            ListInventoryWithProjection();


            //GetItemsForListing();


            //GetAllActiveItemsAsPipeDelimitedString();

            //GetItemsTotalValues();

            //GetFullItemDetails();

            // 09.02
            ListCategoriesAndColors();

        }

        private static void ListCategoriesAndColors()
        {
            using var db = new InventoryDbContext(_optionsBuilder.Options);
            var results = db.Categories
                .Include(x => x.CategoryDetail)
                .ProjectTo<CategoryDTO>(_mapper.ConfigurationProvider)
                .ToList();
            Console.WriteLine();
            Console.WriteLine("ListCategoriesAndColors");
            foreach (var c in results)
            {
                Console.WriteLine($"Category [{c.Category}] is {c.CategoryDetail.Color}");
            }
            /*  Note that in this method you did use the Include syntax as the original code is grabbing categories and their details.
             *  If you select the CategoryDetail, AutoMapper will not be able to make the projection correctly from the internal CategoryDetailDto 
             *  from a CategoryDetail, and you cannot use an anonymous type with ProjectTo with AutoMapper.
             *  Using Include allows the selection of the data, and then mapping is completed successfully.
            */
        }

        private static void ListInventoryWithProjection()
        {
            using var db = new InventoryDbContext(_optionsBuilder.Options);
            var items = db.Items
                .OrderBy(x => x.Name)
                .ProjectTo<ItemDTO>(_mapper.ConfigurationProvider)
                .ToList();

            Console.WriteLine("ListInventoryWithProjection");
            items.ForEach(x => Console.WriteLine($"New Item: {x}"));
        }

        private static void GetItemsForListingLinqDTO()
        {
            var minDateValue = new DateTime(2021, 1, 1);
            var maxDateValue = new DateTime(2034, 1, 1);

            using var db = new InventoryDbContext(_optionsBuilder.Options);
            var results = db.Items.Select(x => new ItemDTO
            {
                CreatedDate = x.CreatedDate,
                CategoryName = x.Category.Name,
                Description = x.Description,
                IsActive = x.IsActive,
                IsDeleted = x.IsDeleted,
                Name = x.Name,
                Notes = x.Notes,
                CategoryId = x.Category.Id,
                Id = x.Id
            }).Where(x => x.CreatedDate >= minDateValue && x.CreatedDate <= maxDateValue)
            .OrderBy(y => y.CategoryName).ThenBy(z => z.Name)
            .ToList();

            Console.WriteLine("GetItemsForListingLinqDTO");
            foreach (var itemDto in results)
            {
                Console.WriteLine(itemDto);
            }
        }
        private static void GetItemsForListingLinq()
        {
            var minDateValue = new DateTime(2021, 1, 1);
            var maxDateValue = new DateTime(2034, 1, 1);

            using var db = new InventoryDbContext(_optionsBuilder.Options);
            var results = db.Items.Select(x => new
            {
                x.CreatedDate,
                CategoryName = x.Category.Name,
                x.Description,
                x.IsActive,
                x.IsDeleted,
                x.Name,
                x.Notes
            }).Where(x => x.CreatedDate >= minDateValue && x.CreatedDate <= maxDateValue)
            .OrderBy(y => y.CategoryName).ThenBy(z => z.Name)
            .ToList();

            Console.WriteLine("GetItemsForListingLinq");
            foreach (var item in results)
            {
                Console.WriteLine($"ITEM {item.CategoryName}| {item.Name} - {item.Description}");
            }
        }

        private static void BuildMapper()
        {
            var services = new ServiceCollection();
            services.AddAutoMapper(typeof(InventoryMapper));
            _serviceProvider = services.BuildServiceProvider();

            // set up the mapping configuration
            _mapperConfig = new MapperConfiguration(cfg => {
                cfg.AddProfile<InventoryMapper>();
            });
            _mapperConfig.AssertConfigurationIsValid();
            _mapper = _mapperConfig.CreateMapper();
        }

        private static void GetFullItemDetails()
        {
            using var db = new InventoryDbContext(_optionsBuilder.Options);
            var results = db.FullItemDetailDtos
                            .FromSqlRaw("SELECT * FROM dbo.vwFullItemDetails ORDER BY ItemName, GenreName, Category, PlayerName")
                            .ToList();
            foreach (var item in results)
            {
                Console.WriteLine($"New Item] {item.Id,-10}" +
                                    $"|{item.ItemName ?? "Unknow",-50}" +
                                    $"|{item.ItemDescription ?? "N/A",-4}" +
                                    $"|{item.PlayerName ?? "Unknow",-5}" +
                                    $"|{item.Category ?? "Unknow",-5}" +
                                    $"|{item.GenreName ?? "Unknow",-5}");
            }
        }

        private static void GetItemsTotalValues()
        {
            using var db = new InventoryDbContext(_optionsBuilder.Options);
            var isActiveParm = new Microsoft.Data.SqlClient.SqlParameter("IsActive", true);
            var totalValues = db.ItemsTotalValue.FromSqlRaw("SELECT * FROM dbo.GetItemsTotalValue (@IsActive)", isActiveParm).ToList();
            Console.WriteLine("dbo.GetItemsTotalValue");
            foreach (var item in totalValues)
            {
                Console.WriteLine($"New Item] {item.Id,-10}" +
                                    $"|{item.Name,-50}" +
                                    $"|{item.Quantity,-4}" +
                                    $"|{item.TotalValue,10}|");
            }
        }

        private static void GetAllActiveItemsAsPipeDelimitedString()
        {
            using var db = new InventoryDbContext(_optionsBuilder.Options);
            var isActiveParm = new Microsoft.Data.SqlClient.SqlParameter("IsActive", true);
            var allItems = db.AllItemsOutput.FromSqlRaw("SELECT dbo.ItemNamesPipeDelimitedString (@IsActive) AllItems", isActiveParm).FirstOrDefault();
            Console.WriteLine($"dbo.ItemNamesPipeDelimitedString - All active Items: {allItems.AllItems}");
        }

        private static void GetItemsForListing()
        {
            using var db = new InventoryDbContext(_optionsBuilder.Options);
            var items = db.ItemsForListing.FromSqlRaw("EXECUTE dbo.GetItemsForListingV1").ToList();
            Console.WriteLine("EXECUTE dbo.GetItemsForListingV1");
            items.ForEach(item => 
            {
                var output = $"ITEM {item.Name}] {item.Description}";
                if (!string.IsNullOrWhiteSpace(item.CategoryName))
                {
                    output = $"{output} has category: {item.CategoryName}";
                }
                Console.WriteLine(output);
            });
        }



        static void BuildOptions()
        {
            _configuration = ConfigurationBuilderSingleton.ConfigurationRoot;
            _optionsBuilder = new DbContextOptionsBuilder<InventoryDbContext>();
           _optionsBuilder.UseSqlServer(_configuration.GetConnectionString("InventoryManager"));
        }




        static void EnsureItems()
        {
            EnsureItem("Batman Begins", "You either die the hero or live long enough to see yourself become the villain", "Christian Bale, Katie Holmes");
            EnsureItem("Inception", "You mustn't be afraid to dream a little bigger, darling", "Leonardo DiCaprio, Tom Hardy, Joseph Gordon - Levitt" );
            EnsureItem("Remember the Titans", "Left Side, Strong Side", "Denzell Washington, Will Patton" );
            EnsureItem("Star Wars: The Empire Strikes Back", "He will join us or die, master", "Harrison Ford, Carrie Fisher, Mark Hamill");
            EnsureItem("Top Gun", "I feel the need, the need for speed!", "Tom Cruise, Anthony Edwards, Val Kilmer");
        }


        private static void EnsureItem(string name, string description, string notes)
        {
            Random r = new();
            using var db = new InventoryDbContext(_optionsBuilder.Options);
            //determine if item exists:
            var existingItem = db.Items.FirstOrDefault(x => x.Name.ToLower() == name.ToLower());
            if (existingItem == null)
            {
                //doesn't exist, add it.
                var item = new Item()
                {
                    Name = name,
                    CreatedByUserId = _loggedInUserId,
                    IsActive = true,
                    Quantity = r.Next(1, InventoryModelsConstants.MAXIMUM_QUANTITY),
                    Description = description,
                    Notes = notes
                };
                db.Items.Add(item);
                db.SaveChanges();
            }
        }

        private static void ListInventory()
        {
            using var db = new InventoryDbContext(_optionsBuilder.Options);
            var items = db.Items.OrderBy(x => x.Name).ToList();
            //items.ForEach(x => Console.WriteLine($"New Item: {x.Name}"));

            Console.WriteLine("AutoMapper");
            var result = _mapper.Map<List<Item>, List<ItemDTO>>(items);
            result.ForEach(x => Console.WriteLine($"New Item: {x}"));
        }

        private static void DeleteAllItems()
        {
            using var db = new InventoryDbContext(_optionsBuilder.Options);
            var items = db.Items.ToList();
            db.Items.RemoveRange(items);
            db.SaveChanges();
        }

        private static void UpdateItems()
        {
            using var db = new InventoryDbContext(_optionsBuilder.Options);
            var items = db.Items.ToList();
            foreach (var item in items)
            {
                item.CurrentOrFinalPrice = 9.99M;
            }
            db.Items.UpdateRange(items);
            db.SaveChanges();
        }












    }

}


/*
 * SELECT * INTO ItemBackup FROM dbo.Items
truncate table dbo.Items
select * from Items

 */