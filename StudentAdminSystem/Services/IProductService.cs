using StudentAdminSystem.Models;
using System.Collections.Generic; // Required for List<Product>
using System.Threading.Tasks;     // Required for Task<T>

namespace StudentAdminSystem.Services
{
    public interface IProductService
    {
        // Must return a Task containing a List of Products
        Task<List<Product>> GetAllProductsAsync();

        // Must return a Task containing a single Product (or null)
        Task<Product> GetProductByIdAsync(string productId);

        // Must return a Task containing a List of Products
        Task<List<Product>> GetProductsByCategoryAsync(string category);

        // Must return a Task containing the created Product
        Task<Product> CreateProductAsync(Product product);

        // Must return a Task containing the updated Product
        Task<Product> UpdateProductAsync(string productId, Product product);

        // Must return a Task containing a boolean (true/false)
        Task<bool> DeleteProductAsync(string productId);
    }
}