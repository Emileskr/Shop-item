using Npgsql;
using Dapper;
using ShopItemAPI.Interfaces;
using System.Xml.Linq;
using System.Data;
using ShopItemAPI.Data.Entities;
using ShopItemAPI.Data.Models.Entities;

namespace ShopItemAPI.Repositories
{
    public class ShopItemRepository : IShopItemRepository
    {
        private readonly IConfiguration _configuration;
        private readonly IDbConnection _connection;
        public ShopItemRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public int DeleteItem(int id)
        {
            try
            {
                string sql = $"DELETE FROM shop_item WHERE id = @id";
                var queryArguments = new
                {
                    id = id
                };
                return _connection.Execute(sql, queryArguments);

            }
            catch (Exception)
            {

                throw new Exception("The item with this id does not exist");
            }

        }

        public IEnumerable<ShopItem> GetAllItems()
        {

                return _connection.Query<ShopItem>
                    ("Select * from shop_item");

        }

        public ShopItem GetItemById(int id)
        {
            try
            {

                string sql = $"SELECT * FROM shop_item WHERE id = @id";
                var queryArguments = new
                {
                    id = id
                };

                return _connection.QueryFirst<ShopItem>(sql, queryArguments);

            }
            catch (Exception)
            {

                throw new Exception("The item with this id does not exist");
            }
           
        }

        public int InsertItem(string name, double price)
        {


                string sql = $"INSERT INTO shop_item (name, price) " +
                    $"VALUES (@name, @price)";

                var queryArguments = new
                {
                    name = name,
                    price = price
                };
                return _connection.Execute(sql, queryArguments);

        }

        public int UpdateItem(int id, string name, double price)
        {
            try
            {
                using (var connection = new NpgsqlConnection
                   (_configuration.GetConnectionString("PostgreConnection")))
                {
                    string sql = $"UPDATE shop_item SET name = @name, price = @price " +
                        $"WHERE id = @id";

                    var queryArguments = new
                    {
                        id = id,
                        name = name,
                        price = price
                    };
                    return connection.Execute(sql, queryArguments);
                }
            }
            catch (Exception)
            {

                throw new Exception("The item with this id does not exist");
            }

        }

        IEnumerable<ShopItem> IShopItemRepository.GetAllItems()
        {
            throw new NotImplementedException();
        }

        ShopItem IShopItemRepository.GetItemById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
