using Catalog.API.Entities;
using MongoDB.Driver;

namespace Catalog.API.Data
{
    public interface ICatalogContext
    {
        void ConnectAndSeedData(IConfiguration config);
        IMongoCollection<Product> Products { get; }
    }
}
