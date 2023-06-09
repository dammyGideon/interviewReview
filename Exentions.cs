using StartTofinish.Dtos;
using StartTofinish.Entities;

namespace StartTofinish {
    public static class Extensions {

        public static ItemDto  AsDto( this Item item){
            return new ItemDto {
                  Id = item.Id,
                    Name = item.Name,
                    Price = item.Price,
                    CreatedDate = item.CreatedDate  
            };
        }
    }
}