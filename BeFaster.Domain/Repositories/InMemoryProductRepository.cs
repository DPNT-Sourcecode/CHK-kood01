using BeFaster.Domain.Entities;

namespace BeFaster.Domain.Repositories;

public class InMemoryProductRepository : IProductRepository<Product>
{
    private readonly Dictionary<char, Product> _productsRepo = new()
    {
        {'A', new Product {ProductSku = , Price = }},
        {'B', new Product {ProductSku = , Price = }},
        {'C', new Product {ProductSku = , Price = }},
        {'D', new Product {ProductSku = , Price = }}
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
