﻿using InventoryModels.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryModels
{
    [Table("ItemGenres")]
    [Index(nameof(ItemId), nameof(GenreId), IsUnique = true)]
    public class ItemGenre : IIdentityModel
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public int GenreId { get; set; }
        public virtual Item Item { get; set; }
        public virtual Genre Genre { get; set; }
    }
}
