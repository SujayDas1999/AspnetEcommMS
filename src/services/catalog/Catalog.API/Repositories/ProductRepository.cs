using Catalog.API.Data;
using Catalog.API.Entities;
using MongoDB.Driver;
using System.Xml.Linq;

namespace Catalog.API.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ICatalogContext _context;

        public ProductRepository(ICatalogContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<bool> DeleteById(string id)
        {
            var deletedResult = await _context.Products.DeleteOneAsync(filter: x => x.Id == id);

            return deletedResult.IsAcknowledged && deletedResult.DeletedCount > 0;
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            return await _context.Products.Find(prop => true).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetByCategory(string categoryName)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Category, categoryName);

            return await _context.Products.Find(filter).ToListAsync();
        }

        public async Task<Product> GetById(string id)
        {
            return await _context.Products.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Product>> GetByName(string name)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Name, name);

            return await _context.Products.Find(filter).ToListAsync();
        }

        public async Task Save(Product product)
        {
            await _context.Products.InsertOneAsync(product);
        }

        public async Task<bool> Update(string id, Product product)
        {
            var foundProduct = await _context.Products.Find(x => x.Id == id).FirstOrDefaultAsync();

            if(foundProduct != null)
            {
                product.Id = foundProduct.Id;

                var updatedResult = await _context.Products.ReplaceOneAsync(filter: g => g.Id == product.Id, replacement: product);
                return updatedResult.IsAcknowledged && updatedResult.ModifiedCount > 0;
            }

            return false;
            
        }
    }
}
