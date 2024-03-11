using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryModels
{
    public class Item : FullAuditModel  //4-1
    {
        [StringLength(InventoryModelsConstants.MAX_NAME_LENGTH)]
        [Required]
        public string Name { get; set; }
        // 4-1
        [Range(InventoryModelsConstants.MINIMUM_QUANTITY, InventoryModelsConstants.MAXIMUM_QUANTITY)]
        public int Quantity { get; set; }
        [StringLength(InventoryModelsConstants.MAX_DESCRIPTION_LENGTH)]
        public string? Description { get; set; }
        [StringLength(InventoryModelsConstants.MAX_NOTES_LENGTH, MinimumLength =10)]
        public string? Notes { get; set; }
        public bool IsOnSale { get; set; }
        public DateTime? PurchasedDate { get; set; }
        public DateTime? SoldDate { get; set; }

        /*No store type was specified for the decimal property 'PurchasePrice' on entity type 'Item'. This will cause values to be silently truncated if they do not fit in the default precision and scale. Explicitly specify the SQL server column type that can accommodate all the values in 'OnModelCreating' using 'HasColumnType', specify precision and scale using 'HasPrecision', or configure a value converter using 'HasConversion'.*/
        [Range(InventoryModelsConstants.MINIMUM_PRICE, InventoryModelsConstants.MAXIMUM_PRICE)]
        [Column(TypeName = "decimal(18,2)")]    // Using Data Annotations 
        public decimal? PurchasePrice { get; set; }

        [Range(InventoryModelsConstants.MINIMUM_PRICE, InventoryModelsConstants.MAXIMUM_PRICE)]
        [Column(TypeName = "decimal(18,2)")]
        public decimal? CurrentOrFinalPrice { get; set; }


        public int? CategoryId { get; set; }
        public virtual Category Category { get; set; }

        public virtual List<Player> Players { get; set; } = new List<Player>();

        public virtual List<ItemGenre> ItemGenres { get; set; } = new List<ItemGenre>();

    }
}