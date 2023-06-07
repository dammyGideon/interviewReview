using StartTofinish.Entities;
using MongoDB.Driver;
using MongoDB.Bson;


namespace StartTofinish.Repositories  {

    public class MongoDbItensRepository : IitemsRepository
    {
        private const string databaseName ="catalog";

        private const string CollectionName="items";
        private readonly IMongoCollection<Item> itemsCollection;

        private readonly FilterDefinitionBuilder<Item> filterBuilder=Builders <Item>.Filter;
        public MongoDbItensRepository(IMongoClient mongoClient){
            IMongoDatabase database = mongoClient.GetDatabase(databaseName);
            itemsCollection = database.GetCollection<Item>(CollectionName);
        }   
        public void CreateItem(Item item)
        {
            itemsCollection.InsertOne(item);
        }
        public void DeleteItem(Guid id)
        {
            var filter = filterBuilder.Eq(existing => existing.Id, id);
            itemsCollection.DeleteOne(filter);
        }
        public Item GetItem(Guid id)
        {
            var filter = filterBuilder.Eq(item=> item.Id, id);
            return itemsCollection.Find(filter).SingleOrDefault();
        }

        public IEnumerable<Item> GetItems()
        {
            return itemsCollection.Find(new BsonDocument()).ToList();
        }

        public void UpdateItem(Item item)
        {
            var filter = filterBuilder.Eq(existing =>existing.Id, item.Id);
            itemsCollection.ReplaceOne(filter,item);
        }
    }
}