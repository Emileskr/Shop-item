using ShopItemAPI.Data.Entities;

namespace ShopItemAPI.Data.Models.Entities;
    public class ShopItem : BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public DateTime Date { get; set; }
       
    }

