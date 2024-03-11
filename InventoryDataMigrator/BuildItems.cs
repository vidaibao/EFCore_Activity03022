using EFCore_DBLibrary;
using InventoryModels;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace InventoryDataMigrator
{
    public class BuildItems
    {
        private readonly InventoryDbContext _context;
        private const string SEED_USER_ID = "31412031-7859-429c-ab21-c2e3e8d98042";

        public BuildItems(InventoryDbContext context)
        {
            this._context = context;
        }

        public void ExecuteSeed()
        {
            if (_context.Items.Count() == 0)
            {
                _context.Items.AddRange(
                    new Item()
                    {
                        Name = "Batman Begins",
                        CurrentOrFinalPrice = 9.99m,
                        Description = "You either die the hero or live long enough to see yourself become the villain",
                        IsOnSale = false,
                        Notes = "",
                        PurchasePrice = 23.99m,
                        PurchasedDate = null,
                        Quantity = 1000,
                        SoldDate = null,
                        CreatedByUserId = SEED_USER_ID,
                        CreatedDate = DateTime.Now,
                        IsDeleted = false,
                        IsActive = true,
                        Players = new List<Player>()
                        {
                            new Player() 
                            { 
                                CreatedDate = DateTime.Now, 
                                IsActive = true,
                                IsDeleted = false,
                                CreatedByUserId = SEED_USER_ID,
                                Description = "https://www.imdb.com/name/nm0000288/",
                                Name = "Christian Bale"
                            }
                        }
                    },
                    new Item()
                    {
                        Name = "The Dark Knight",
                        CurrentOrFinalPrice = 9.99m,
                        Description = "Why so serious?",
                        IsOnSale = false,
                        Notes = "",
                        PurchasePrice = 23.99m,
                        PurchasedDate = null,
                        Quantity = 1000,
                        SoldDate = null,
                        CreatedByUserId = SEED_USER_ID,
                        CreatedDate = DateTime.Now,
                        IsDeleted = false,
                        IsActive = true,
                        Players = new List<Player>()
                        {
                            new Player()
                            {
                                CreatedDate = DateTime.Now,
                                IsActive = true,
                                IsDeleted = false,
                                CreatedByUserId = SEED_USER_ID,
                                Description = "https://www.imdb.com/name/nm0000288/",
                                Name = "Christian Bale"
                            },
                            new Player()
                            {
                                CreatedDate = DateTime.Now,
                                IsActive = true,
                                IsDeleted = false,
                                CreatedByUserId = SEED_USER_ID,
                                Description = "https://www.imdb.com/name/nm0000198/",
                                Name = "Heath Ledger"
                            },
                            new Player()
                            {
                                CreatedDate = DateTime.Now,
                                IsActive = true,
                                IsDeleted = false,
                                CreatedByUserId = SEED_USER_ID,
                                Description = "https://www.imdb.com/name/nm0000323/",
                                Name = "Aaron Eckhart"
                            }
                        }
                    },
                    new Item()
                    {
                        Name = "Inception",
                        CurrentOrFinalPrice = 7.99m,
                        Description = "You mustn't be afraid to dream a little bigger,darling",
                        IsOnSale = false,
                        Notes = "",
                        PurchasePrice = 4.99m,
                        PurchasedDate = null,
                        Quantity = 1000,
                        SoldDate = null,
                        CreatedByUserId = SEED_USER_ID,
                        CreatedDate = DateTime.Now,
                        IsDeleted = false,
                        IsActive = true,
                        Players = new List<Player>()
                        {
                            new Player() 
                            { 
                                CreatedDate = DateTime.Now,
                                IsActive = true,
                                IsDeleted = false,
                                CreatedByUserId = SEED_USER_ID,
                                Description = "https://www.imdb.com/name/nm0000138/",
                                Name = "Leonardo DiCaprio"
                            },
                            new Player()
                            {
                                CreatedDate = DateTime.Now,
                                IsActive = true,
                                IsDeleted = false,
                                CreatedByUserId = SEED_USER_ID,
                                Description = "https://www.imdb.com/name/nm0000323/",
                                Name = "Tom Hardy"
                            },
                            new Player()
                            {
                                CreatedDate = DateTime.Now,
                                IsActive = true,
                                IsDeleted = false,
                                CreatedByUserId = SEED_USER_ID,
                                Description = "https://www.imdb.com/name/nm0000198/",
                                Name = "Joseph Gordon-Levitt"
                            },
                            new Player()
                            {
                                CreatedDate = DateTime.Now,
                                IsActive = true,
                                IsDeleted = false,
                                CreatedByUserId = SEED_USER_ID,
                                Description = "https://www.imdb.com/name/nm0000138/",
                                Name = "Ellen Page"
                            }
                            
                        }
                    },
                    new Item()
                    {
                        Name = "Star Wars: The Empire Strikes Back",
                        CurrentOrFinalPrice = 19.99m,
                        Description = "He will join us or die, master",
                        IsOnSale = false,
                        Notes = "",
                        PurchasePrice = 35.99m,
                        PurchasedDate = null,
                        Quantity = 1000,
                        SoldDate = null,
                        CreatedByUserId = SEED_USER_ID,
                        CreatedDate = DateTime.Now,
                        IsDeleted = false,
                        IsActive = true,
                        Players = new List<Player>() 
                        {
                            new Player() 
                            { 
                                CreatedDate = DateTime.Now,
                                IsActive = true,
                                IsDeleted = false,
                                CreatedByUserId = SEED_USER_ID,
                                Description = "https://www.imdb.com/name/nm0000434/",
                                Name = "Mark Hamill"
                            }
                        }
                    },
                    new Item()
                    {
                        Name = "Remember the Titans",
                        CurrentOrFinalPrice = 3.99m,
                        Description = "Left Side, Strong Side",
                        IsOnSale = false,
                        Notes = "",
                        PurchasePrice = 7.99m,
                        PurchasedDate = null,
                        Quantity = 1000,
                        SoldDate = null,
                        CreatedByUserId = SEED_USER_ID,
                        CreatedDate = DateTime.Now,
                        IsDeleted = false,
                        IsActive = true,
                        Players = new List<Player>() 
                        {
                            new Player() 
                            {
                                CreatedDate = DateTime.Now,
                                IsActive = true,
                                IsDeleted = false,
                                CreatedByUserId = SEED_USER_ID,
                                Description = "https://www.imdb.com/name/nm0000243/",
                                Name = "Denzel Washington"
                            },
                            new Player()
                            {
                                CreatedDate = DateTime.Now,
                                IsActive = true,
                                IsDeleted = false,
                                CreatedByUserId = SEED_USER_ID,
                                Description = "https://www.imdb.com/name/nm0000434/",
                                Name = "Will Patton"
                            }
                        }
                    }

                );
            }
            _context.SaveChanges();
        }
    }
}
