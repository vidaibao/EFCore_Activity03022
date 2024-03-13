using AutoMapper;
using InventoryModels;
using InventoryModels.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore_Activity0302
{
    public class InventoryMapper : Profile
    {
        public InventoryMapper()
        {
            CreateMaps();
        }

        private void CreateMaps()
        {
            CreateMap<Item, ItemDTO>();
            //CreateMap<Category, CategoryDTO>();
            // 09.02
            CreateMap<Category, CategoryDTO>()
                .ForMember(x => x.Category, opt => opt.MapFrom(y => y.Name))
                .ReverseMap() // need to map the relationship in both directions and sometimes just in one direction
                .ForMember(y => y.Name, opt => opt.MapFrom(x => x.Category));

            CreateMap<CategoryDetail, CategoryDetailDTO>()
                .ForMember(x => x.Color, opt => opt.MapFrom(y => y.ColorName))
                .ForMember(x => x.Value, opt => opt.MapFrom(y => y.ColorValue))
                .ReverseMap()
                .ForMember(y => y.ColorValue, opt => opt.MapFrom(x => x.Value))
                .ForMember(y => y.ColorName, opt => opt.MapFrom(x => x.Color));
            // << 09.02
            /*  By reversing the map with the ReverseMap call and going in the other direction, it is
                now possible to map one of the database objects to the corresponding DTO and also to
                go from the DTO back to the corresponding database object.
            */
        }
    }
}
