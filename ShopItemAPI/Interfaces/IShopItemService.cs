﻿using ShopItemAPI.Data.Entities;
using ShopItemAPI.Data.Models.Entities;

namespace ShopItemAPI.Interfaces
{
    public interface IShopItemService
    {
        public IEnumerable<ShopItem> GetAllItems();
        public int InsertItem(string name, double price);
        public bool UpdateItem(int id, string name, double price);
        public bool DeleteItem(int id);
        public ShopItem GetItemById(int id);
    }
}
