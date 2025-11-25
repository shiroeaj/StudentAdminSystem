using MongoDB.Driver;
using StudentAdminSystem.Models;
using System.Threading.Tasks; // Ensure this is included for Task

namespace StudentAdminSystem.Data
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;

        public MongoDbContext(IMongoClient mongoClient, string databaseName)
        {
            _database = mongoClient.GetDatabase(databaseName);
        }

        // FIXED: Added generic type <Product>
        public IMongoCollection<Product> Products =>
            _database.GetCollection<Product>("Products");

        // FIXED: Added generic type <Order>
        public IMongoCollection<Order> Orders =>
            _database.GetCollection<Order>("Orders");

        // FIXED: Added generic type <Receipt>
        public IMongoCollection<Receipt> Receipts =>
            _database.GetCollection<Receipt>("Receipts");

        public async Task InitializeIndexesAsync()
        {
            // FIXED: Added generic type <Product> to CreateIndexModel array and the model
            var productIndexes = new CreateIndexModel<Product>[] {
                new CreateIndexModel<Product>(
                    Builders<Product>.IndexKeys.Ascending(p => p.Category))
            };
            await Products.Indexes.CreateManyAsync(productIndexes);

            // FIXED: Added generic type <Order> to CreateIndexModel array and the model
            var orderIndexes = new CreateIndexModel<Order>[] {
                new CreateIndexModel<Order>(
                    Builders<Order>.IndexKeys.Ascending(o => o.StudentId)),
                new CreateIndexModel<Order>(
                    Builders<Order>.IndexKeys.Ascending(o => o.OrderDate))
            };
            await Orders.Indexes.CreateManyAsync(orderIndexes);
        }
    }
}