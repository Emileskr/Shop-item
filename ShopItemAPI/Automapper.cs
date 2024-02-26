using AutoMapper;
using ShopItemAPI.Data.Dtos;
using ShopItemAPI.Data.Models.Entities;

namespace ShopItemAPI
{
    public class Automapper : Profile
    {
        public Automapper()
        {
            CreateMap<ShopItem, ShopItemDto>();
        }
    }
}
