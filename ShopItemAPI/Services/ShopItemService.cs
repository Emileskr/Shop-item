using ShopItemAPI.Data.Entities;
using ShopItemAPI.Data.Models.Entities;
using ShopItemAPI.Interfaces;

namespace ShopItemAPI.Services
{
    public class ShopItemService : IShopItemService
    {
        public readonly IShopItemRepository _shopItemRepository;
        public ShopItemService(IShopItemRepository shopItemRepository)
        {
            _shopItemRepository = shopItemRepository;
        }
        public bool DeleteItem(int id)
        {
            return _shopItemRepository.DeleteItem(id)>0;
        }

        public IEnumerable<ShopItem> GetAllItems()
        {
            return _shopItemRepository.GetAllItems().ToList();
        }

        public ShopItem GetItemById(int id)
        {
            return _shopItemRepository.GetItemById(id);
        }

        public int InsertItem(string name, double price)
        {
            return _shopItemRepository.InsertItem(name, price);
        }

        public bool UpdateItem(int id, string name, double price)
        {
            return _shopItemRepository.UpdateItem(id, name, price)>0;
        }

        IEnumerable<ShopItem> IShopItemService.GetAllItems()
        {
            throw new NotImplementedException();
        }

        ShopItem IShopItemService.GetItemById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
