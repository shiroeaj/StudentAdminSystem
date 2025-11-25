using StudentAdminSystem.Models;
using System.Collections.Generic; // Required for List<Order>
using System.Threading.Tasks;     // Required for Task<T>

namespace StudentAdminSystem.Services
{
    public interface IOrderService
    {
        // Must return the created Order object
        Task<Order> CreateOrderAsync(Order order);

        // Must return the single Order object
        Task<Order> GetOrderByIdAsync(string orderId);

        // FIXED: Corrected syntax to Task<List<Order>>
        Task<List<Order>> GetOrdersByStudentAsync(int studentId);

        // FIXED: Corrected syntax to Task<List<Order>>
        Task<List<Order>> GetAllOrdersAsync();

        // Must return the updated Order object (result of FindOneAndUpdateAsync)
        Task<Order> UpdateOrderStatusAsync(string orderId, string status);

        // Must return a boolean result from the Cancelled operation
        Task<bool> CancelOrderAsync(string orderId);
    }
}