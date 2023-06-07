using Microsoft.AspNetCore.Mvc;
using StartTofinish.Repositories;
using StartTofinish.Entities;
using StartTofinish.Dtos;

namespace StartTofinish.Controllers{

    [ApiController]
    [Route("items")]
    public class ItemsController : ControllerBase {

        private readonly IitemsRepository _ItemRepository;

        public ItemsController(IitemsRepository itemRepository){
            this._ItemRepository = itemRepository;
        }

        [HttpGet]
        public IEnumerable<ItemDto> GetWeapon(){
            var items = _ItemRepository.GetItems().Select(item => item.AsDto());
            return items;
        }

        [HttpGet("{id}")]
        public ActionResult<ItemDto> GetItem(Guid id){

            var item = _ItemRepository.GetItem(id);
            if(item is null){
                return NotFound();
            }
            return item.AsDto();
        }

        [HttpPost]
        public ActionResult<CreateItemDto> CreateItem(CreateItemDto createItemDto){
            Item item = new(){
                Id = Guid.NewGuid(),
                Name = createItemDto.Name,
                Price = createItemDto.Price,
                CreatedDate = DateTimeOffset.UtcNow
            };
            _ItemRepository.CreateItem(item);
            return CreatedAtAction(nameof(GetItem), new{id = item.Id},item.AsDto());
            
        }
        [HttpPut("{id}")]
        public ActionResult updateItem(Guid id, UpdateItemDto itemDto){

            var existingItem = _ItemRepository.GetItem(id);
            if(existingItem is null){
                return NotFound();
            }

            Item updateItem = existingItem with {
                Name = itemDto.Name,
                Price = itemDto.Price
            };
            _ItemRepository.UpdateItem(updateItem);
            return NoContent();

        }

        [HttpDelete("{id}")]
        public ActionResult DeleteItem (Guid id){
            var exisiting = _ItemRepository.GetItem(id);
            if(exisiting is null ){
                return NotFound();
            }

            _ItemRepository.DeleteItem(id);
            return NoContent();
        }
    }
}