using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using ShopItemAPI.Interfaces;
using ShopItemAPI.Data.Entities;

namespace ShopItemAPI.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ShopItemController : ControllerBase
    {

        private readonly IShopItemService _shopItemService;
        private readonly ILogger<ShopItemController> _logger;
        public ShopItemController(IShopItemService shopItemService, ILogger<ShopItemController> logger)
        {
            _shopItemService = shopItemService;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> InsertShopItem([FromBody] string name, double price)
        {
            return Ok(_shopItemService.InsertItem(name, price));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllItems()
        {
            return Ok(_shopItemService.GetAllItems());
        }

        [HttpGet]
        public async Task<IActionResult> GetItemById(int id)
        {
            var item = _shopItemService.GetItemById(id);
            if (item == null)
            {
                return NotFound("Item does not exist");
            } 
                return Ok(item);

        }

        [HttpDelete]
        public async Task<IActionResult> DeleteItem(int id)
        {
            var item = _shopItemService.DeleteItem(id);
            if (!item)
            {
                return NotFound("Item does not exist");
            }
            return Ok(item);
         
        }
        [HttpPut]
        public async Task<IActionResult> UpdateItem(int id , [FromBody] string name, double price)
        {
            var item = _shopItemService.UpdateItem(id, name, price);
            if (!item)
            {
                return NotFound("Item does not exist");
            }
                return Ok(item);
           
         }
    }
}
