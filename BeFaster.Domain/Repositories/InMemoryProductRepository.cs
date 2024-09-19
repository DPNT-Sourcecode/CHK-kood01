using BeFaster.Domain.Entities;

namespace BeFaster.Domain.Repositories;

public class InMemoryProductRepository : IProductRepository<Product>
{
    private readonly Dictionary<char, Product> _productsRepo = new()
    {
        { 'A', new Product { ProductSku = 'A', Price = 50 } },
        { 'B', new Product { ProductSku = 'B', Price = 30 } },
        { 'C', new Product { ProductSku = 'C', Price = 20 } },
        { 'D', new Product { ProductSku = 'D', Price = 15 } },
        { 'E', new Product { ProductSku = 'E', Price = 40 } },
    };
    
    public Product? GetByProductSku(char productSku)
    {
        return _productsRepo.GetValueOrDefault(productSku);
    }

    public void Add(Product product)
    {
        if (!_productsRepo.ContainsKey(product.ProductSku))
        {
            _productsRepo.Add(product.ProductSku, product);
        }
    }
}