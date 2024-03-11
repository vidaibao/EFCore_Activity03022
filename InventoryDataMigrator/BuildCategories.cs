using EFCore_DBLibrary;
using InventoryModels;

namespace InventoryDataMigrator
{
    internal class BuildCategories
    {
        private readonly InventoryDbContext _context;
        private const string SEED_USER_ID = "31412031-7859-429c-ab21-c2e3e8d98042";

        public BuildCategories(InventoryDbContext context)
        {
            this._context = context;
        }

        public void ExecuteSeed()
        {
            if (_context.Categories.Count() == 0)
            {
                _context.Categories.AddRange(
                    new Category()
                    {
                        CreatedByUserId = SEED_USER_ID,
                        CreatedDate = DateTime.Now,
                        IsActive = true,
                        IsDeleted = false,
                        Name = "Movies",
                        CategoryDetail = new CategoryDetail()
                        {
                            ColorValue = "#0000FF",
                            ColorName = "Blue"
                        }
                    },
                    new Category()
                    {
                        CreatedByUserId = SEED_USER_ID,
                        CreatedDate = DateTime.Now,
                        IsActive = true,
                        IsDeleted = false,
                        Name = "Books",
                        CategoryDetail = new CategoryDetail()
                        {
                            ColorValue = "#FF0000",
                            ColorName = "Red"
                        }
                    },
                    new Category()
                    {
                        CreatedByUserId = SEED_USER_ID,
                        CreatedDate = DateTime.Now,
                        IsActive = true,
                        IsDeleted = false,
                        Name = "Games",
                        CategoryDetail = new CategoryDetail()
                        {
                            ColorValue = "#00FF00",
                            ColorName = "Green"
                        }
                    }
                );
                _context.SaveChanges();
            }
        }
    }
}