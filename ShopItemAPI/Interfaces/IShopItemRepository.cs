using ShopItemAPI.Data.Entities;
using ShopItemAPI.Data.Models.Entities;

namespace ShopItemAPI.Interfaces
{
    public interface IShopItemRepository
    {
        public IEnumerable<ShopItem> GetAllItems();
        public int InsertItem (string name, double price); 
        public int UpdateItem (int id, string name, double price);
        public int DeleteItem (int id);
        public ShopItem GetItemById (int id);
    }
}
