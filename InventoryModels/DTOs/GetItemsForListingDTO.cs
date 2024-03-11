using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryModels.DTOs
{
    /*** In order to use the Fluent API to modify this solution and use the procedure as is, you will need to do three things.
     * 
     * First, you need a Data Transfer Object (DTO) to map the result to an object.
     * Second, you will need to add code in the override for the OnModelCreating method in the InventoryDbContext. 
     * Finally, you’ll need to modify the call in the original code to leverage the new DTO object.
     * 
     * This DTO will be used to map the results of the stored procedure so that you will no longer get the error for the missing required fields.
    ***/

    public class GetItemsForListingDTO
    {
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        public string Notes { get; set; } = "";
        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; } = true;
        public string CategoryName { get; set; } = "";
    }
}
