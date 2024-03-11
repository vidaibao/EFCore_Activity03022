using EFCore_DBLibrary;
using InventoryHelpers;
using InventoryModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace InventoryDataMigrator
{
    internal class Program
    {
        static IConfigurationRoot _configuration;
        static DbContextOptionsBuilder<InventoryDbContext> _optionsBuilder;
        static void BuildOptions()
        {
            _configuration = ConfigurationBuilderSingleton.ConfigurationRoot;
            _optionsBuilder = new DbContextOptionsBuilder<InventoryDbContext>();
            _optionsBuilder.UseSqlServer(_configuration.GetConnectionString("InventoryManager"));
        }

        /*
         * Don’t run update-database. Instead, run the migrator project you created to both
        execute the migration and also seed the Categories and CategoryDetails.
        Right-click the InventoryDataMigrator project and select Debug ➤ Start New Instance
         */

        static void Main(string[] args)
        {
            BuildOptions();

            ApplyMigrations();

            ExecuteCustomSeedData();

        }

        

        private static void ExecuteCustomSeedData()
        {
            using var context = new InventoryDbContext(_optionsBuilder.Options);
            //var categories = new BuildCategories(context);
            //categories.ExecuteSeed();

            var items = new BuildItems(context);
            items.ExecuteSeed();
        }

        private static void ApplyMigrations()
        {
            using var db = new InventoryDbContext(_optionsBuilder.Options);
            db.Database.Migrate();
        }
    }
}
