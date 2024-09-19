using BeFaster.Domain.Entities;

namespace BeFaster.Domain.Repositories;

public class InMemoryProductRepository : IProductRepository<Product>
{
    private readonly Dictionary<char, Product> _productsRepo = new()
    {
        { 'A', new Product { ProductSku = 'A', Price = 50 } },
        { 'B', new Product { ProductSku = 'B', Price = 30 } },
        { 'C', new Product { ProductSku = 'C', Price = 20 } },
        { 'D', new Product { ProductSku = 'D', Price = 15 } }
    };
    
    public Product? GetByProductSku(char productSku)
    {
        throw new NotImplementedException();
    }

    public void Add(Product entity)
    {
        throw new NotImplementedException();
    }
}