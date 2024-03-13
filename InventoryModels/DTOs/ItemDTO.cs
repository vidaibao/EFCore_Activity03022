using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryModels.DTOs
{
    public class ItemDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }


        // Activity 09.02
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public string Notes { get; set; }
        public string CategoryName { get; set; }
        public DateTime CreatedDate { get; set; }
        
        public override string ToString()
        {
            //return $"{Name,-50} | {Description}";
            return $"ITEM {Name,-35}] {Description,-50} has category: {CategoryName}";
        }
        //09.02
    }

    /*
     *  One thing to note is that AutoMapper works automatically when the field names
        line up exactly with each other. Here, both classes, Item and ItemDto, had fields with
        identical names – Id, Name, Description, and CategoryId.
        When the field names don’t line up exactly, then you will need to do a bit more with
        AutoMapper configurations to make things work as expected. You’ll get to see how to do
        that next in the final activity for this chapter.
     */
}
