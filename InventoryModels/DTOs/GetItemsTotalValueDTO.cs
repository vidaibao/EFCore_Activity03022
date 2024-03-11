using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryModels.DTOs
{
    public class GetItemsTotalValueDTO
    {
        /*  SELECT Id, [Name], [Description], Quantity, PurchasePrice, Quantity * PurchasePrice as TotalValue
            From Items
            Where IsActive = @IsActive
        */
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        public int Quantity { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal PurchasePrice { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal TotalValue { get; set; }
    }
}
