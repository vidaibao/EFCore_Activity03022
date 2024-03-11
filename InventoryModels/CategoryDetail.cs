using InventoryModels.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryModels
{
    public class CategoryDetail : IIdentityModel
    {
        [Key, ForeignKey("Category")] // use the same Id as the Category table for the record as a direct one-to-one relationship
        [Required]
        public int Id { get; set; }

        [StringLength(InventoryModelsConstants.MAX_COLORVALUE_LENGTH)]
        public string ColorValue { get; set; }

        [StringLength(InventoryModelsConstants.MAX_COLORNAME_LENGTH)]
        public string ColorName { get; set; }

        public virtual Category Category { get; set; }
    }
}
