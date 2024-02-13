using firstapiproject.Model;
using Microsoft.Identity.Client;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections;

namespace firstapiproject.Repository
{
    public class ItemRepository
    {
        private readonly IMongoCollection<Item> _collection;
        public ItemRepository(IMongoClient client) 
        
        {
            var database = client.GetDatabase("dummydb");
            _collection = database.GetCollection<Item>("items");
        }

        //get all the items from the db
        public async Task<List<Item>> GetAllItemsAsync()
        {
            return await _collection.AsQueryable<Item>().ToListAsync();
            //return await _collection.Find(_ => true).ToListAsync();

        }

        //get a specific item from the db
        public async Task<Item> GetItemByIdAsync(ObjectId id)
        {
            var filter = Builders<Item>.Filter.Eq("_id",id);
            var item = await _collection.Find(filter).FirstOrDefaultAsync();
            return item;
        }

        //test case

        public async Task<Item> GetByIdAsync(string id)
        {
            var objectId = new ObjectId(id);
            var filter = Builders<Item>.Filter.Eq("_id", objectId);
            return await _collection.Find(filter).FirstOrDefaultAsync();
        }


        //Update item
        public async Task UpdateItemAsync(string id,Item entity)
        {
            //var filter = Builders<Item>.Filter.Eq("_id", id);

            //var Updateitem = Builders<Item>.Update.Set("Name", item.Name)
            //.Set("Price", item.Price);

            //var result = await _collection.FindOneAndUpdateAsync(filter, Updateitem, new FindOneAndUpdateOptions<Item>
            //{
            //    ReturnDocument = ReturnDocument.After // Return the updated document
            //});

            //return result;

            var objectId = new ObjectId(id);
            var filter = Builders<Item>.Filter.Eq("_id", objectId);
            var update = Builders<Item>.Update
                .Set("Name", entity.Name)
                .Set("Price", entity.Price);
            await _collection.UpdateOneAsync(filter, update);
        }

        public async Task DeleteItemAsync(string id)
        {
            var objectId = new ObjectId(id);

            var filter = Builders<Item>.Filter.Eq("_id", objectId);

            var result = await _collection.DeleteOneAsync(filter);
        }

        public async Task CreateItemAsync(Item item)
        {
            await _collection.InsertOneAsync(item);

            
        }
    }
}
