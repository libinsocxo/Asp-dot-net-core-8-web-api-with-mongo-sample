using firstapiproject.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using firstapiproject.Model;

namespace firstapiproject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly ItemRepository _itemRepository;

        public ItemController(ItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }

        [HttpGet("getallitems")]
        public async Task<ActionResult<IEnumerable<ItemController>>> GetAllItems()
        {
            var items =  await _itemRepository.GetAllItemsAsync();

            return Ok(items);
        }

        [HttpGet("getitem/{id}")]
        public async Task<ActionResult<IEnumerable<ItemController>>> GetItem(string id)
        {

            if (!ObjectId.TryParse(id, out var objectId))
            {
                return BadRequest("Invalid ObjectId format");
            }

            var item = await _itemRepository.GetItemByIdAsync(objectId);

            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        [HttpPut("updateitem/{id}")]
        public async Task<IActionResult>UpdateItem(string id,[FromBody] firstapiproject.Model.Item item)
        {


            var existingItem = await _itemRepository.GetByIdAsync(id);

            if (existingItem == null)
            {
                return NotFound();
            }

            await _itemRepository.UpdateItemAsync(id, item);

            return NoContent();


        }

        [HttpDelete("itemdelete/{id}")]
        public async Task<IActionResult> deleteItem(string id)
        {
            var itemexists = await _itemRepository.GetByIdAsync(id);

            if(itemexists == null)
            {
                return NotFound();
            }

            await _itemRepository.DeleteItemAsync(id);

            return Content("Item has been deleted");


        }

        [HttpPost("createiteam")]
        public async Task<IActionResult> CreateItem([FromBody] firstapiproject.Model.Item item)
        { 
            await _itemRepository.CreateItemAsync(item);

            return Ok(await _itemRepository.GetAllItemsAsync());

        }
    }
}
