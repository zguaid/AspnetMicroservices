using Catalog.API.Entities;
using FluentResults;

namespace Catalog.API.Repositories
{
    public interface IProductRepository
    {
        Task<Result<IEnumerable<Product>>> GetProducts();
        Task<Result<Product>> GetProduct(string id);
        Task<Result<IEnumerable<Product>>> GetProductByName(string name);
        Task<Result<IEnumerable<Product>>> GetProductByCategory(string categoryName);

        Task<Result<Product>> CreateProduct(Product product);
        Task<Result<bool>> UpdateProduct(Product product);
        Task<Result<bool>> DeleteProduct(string id);
    }
}

