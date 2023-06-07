using System;
using System.Collections.Generic;
using StartTofinish.Entities;
namespace StartTofinish.Repositories {

    public class ItemsRepository:IitemsRepository {

        private readonly List<Item> items =new(){
            new Item {Id= Guid.NewGuid(), Name="Potion", Price =9, CreatedDate = DateTimeOffset.UtcNow},
            new Item {Id = Guid.NewGuid(), Name ="Iron Sword", Price=100, CreatedDate=DateTimeOffset.UtcNow,},
            new Item {Id = Guid.NewGuid(), Name ="brazen", Price = 20, CreatedDate = DateTimeOffset.UtcNow,}
        };

        public IEnumerable<Item>GetItems(){
             return items;
        } 

        public Item GetItem(Guid id)
        {
            return items.Where(item => item.Id == id).SingleOrDefault();
        }

         public void CreateItem(Item item){
            items.Add(item);
         }

        public void UpdateItem(Item item){
            var index = items.FindIndex(existingItem => existingItem.Id ==item.Id);
            items[index]=item;
        }

        public void DeleteItem(Guid id){
            var index= items.FindIndex(existingItem => existingItem.Id ==id);
            items.RemoveAt(index);
        }
    }
}