using Catalog.API.Data;
using Catalog.API.Entities;
using FluentResults;
using MongoDB.Driver;

namespace Catalog.API.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ICatalogContext _context;

        public ProductRepository(ICatalogContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Result<IEnumerable<Product>>> GetProducts()
        {
            try
            {
                var products = await _context.Products.Find(p => true).ToListAsync();
                return Result.Ok(products);
            }
            catch (MongoException ex)
            {
                return Result.Fail(ex.Message);
            }
            catch (Exception ex)
            {
                return Result.Fail(ex.Message);
            }
        }

        public async Task<Result<Product>> GetProduct(string id)
        {
            try
            {
                var product = await _context.Products.Find(p => p.Id == id).FirstOrDefaultAsync();
                if (product == null)
                {
                    return Result.Fail($"Product with id: {id}, not found.");
                }
                return Result.Ok(product);
            }
            catch (MongoException ex)
            {
                return Result.Fail(ex.Message);
            }
            catch (Exception ex)
            {
                return Result.Fail(ex.Message);
            }
        }

        public async Task<Result<IEnumerable<Product>>> GetProductByName(string name)
        {
            try
            {
                FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Name, name);
                var products = await _context.Products.Find(filter).ToListAsync();
                return Result.Ok(products);
            }
            catch (MongoException ex)
            {
                return Result.Fail(ex.Message);
            }
            catch (Exception ex)
            {
                return Result.Fail(ex.Message);
            }
        }

        public async Task<Result<IEnumerable<Product>>> GetProductByCategory(string categoryName)
        {
            try
            {
                FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Category, categoryName);
                var products = await _context.Products.Find(filter).ToListAsync();
                return Result.Ok(products);
            }
            catch (MongoException ex)
            {
                return Result.Fail(ex.Message);
            }
            catch (Exception ex)
            {
                return Result.Fail(ex.Message);
            }
        }

        public async Task<Result<Product>> CreateProduct(Product product)
        {
            try
            {
                await _context.Products.InsertOneAsync(product);
                return Result.Ok(product);
            }
            catch (MongoException ex)
            {
                return Result.Fail(ex.Message);
            }
            catch (Exception ex)
            {
                return Result.Fail(ex.Message);
            }
        }

        public async Task<Result<bool>> UpdateProduct(Product product)
        {
            try
            {
                var updateResult = await _context.Products.ReplaceOneAsync(g => g.Id == product.Id, product);
                if (updateResult.IsAcknowledged && updateResult.ModifiedCount > 0)
                {
                    return Result.Ok(true);
                }
                return Result.Fail("Product update failed");
            }
            catch (MongoException ex)
            {
                return Result.Fail(ex.Message);
            }
            catch (Exception ex)
            {
                return Result.Fail(ex.Message);
            }
        }

        public async Task<Result<bool>> DeleteProduct(string id)
        {
            try
            {
                FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Id, id);
                DeleteResult deleteResult = await _context.Products.DeleteOneAsync(filter);
                if (deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0)
                {
                    return Result.Ok(true);
                }
                return Result.Fail("Product delete failed");
            }
            catch (MongoException ex)
            {
                return Result.Fail(ex.Message);
            }
            catch (Exception ex)
            {
                return Result.Fail(ex.Message);
            }
        }
    }
}

