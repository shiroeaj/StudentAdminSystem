using StudentAdminSystem.Data;
using StudentAdminSystem.Models;
using MongoDB.Driver;
using System.Collections.Generic; // Required for List<Order>
using System.Threading.Tasks;     // Required for Task<T>

namespace StudentAdminSystem.Services
{
    public class OrderService : IOrderService
    {
        private readonly MongoDbContext _context;

        public OrderService(MongoDbContext context)
        {
            _context = context;
        }

        // FIXED: Return type changed to Task<Order> to match the 'return order;' statement
        public async Task<Order> CreateOrderAsync(Order order)
        {
            order.OrderDate = DateTime.UtcNow;
            order.Status = "Pending";
            await _context.Orders.InsertOneAsync(order);
            return order;
        }

        // FIXED: Return type changed to Task<Order> to match the returned Order object
        public async Task<Order> GetOrderByIdAsync(string orderId)
        {
            return await _context.Orders.Find(o => o.Id == orderId).FirstOrDefaultAsync();
        }

        // FIXED: Corrected syntax to Task<List<Order>>
        public async Task<List<Order>> GetOrdersByStudentAsync(int studentId)
        {
            return await _context.Orders.Find(o => o.StudentId == studentId).ToListAsync();
        }

        // FIXED: Corrected syntax to Task<List<Order>>
        public async Task<List<Order>> GetAllOrdersAsync()
        {
            return await _context.Orders.Find(_ => true).ToListAsync();
        }

        // FIXED: Return type changed to Task<Order> to match the 'return result;' statement
        public async Task<Order> UpdateOrderStatusAsync(string orderId, string status)
        {
            var update = Builders<Order>.Update.Set(o => o.Status, status); // Added generic constraint for clarity
            var result = await _context.Orders.FindOneAndUpdateAsync(o => o.Id == orderId, update);
            return result;
        }

        // FIXED: Return type changed to Task<bool> to match the 'return result != null;' statement
        public async Task<bool> CancelOrderAsync(string orderId)
        {
            var result = await UpdateOrderStatusAsync(orderId, "Cancelled");
            // The result will be an Order object or null.
            return result != null;
        }
    }
}