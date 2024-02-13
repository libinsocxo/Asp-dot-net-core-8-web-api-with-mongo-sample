using MongoDB.Bson;

namespace firstapiproject.Model
{
    public class Item
    {
        public ObjectId Id { get; set; }
        public string? Name { get; set; }
        public decimal Price { get; set; }
    }
}
