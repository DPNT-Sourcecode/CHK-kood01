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
        { 'F', new Product { ProductSku = 'F', Price = 10 } },
        { 'G', new Product { ProductSku = 'G', Price = 20 } },
        { 'H', new Product { ProductSku = 'H', Price = 10 } },
        { 'I', new Product { ProductSku = 'I', Price = 35 } },
        { 'J', new Product { ProductSku = 'J', Price = 60 } },
        { 'K', new Product { ProductSku = 'K', Price = 70 } },
        { 'L', new Product { ProductSku = 'L', Price = 90 } },
        { 'M', new Product { ProductSku = 'M', Price = 15 } },
        { 'N', new Product { ProductSku = 'N', Price = 40 } },
        { 'O', new Product { ProductSku = 'O', Price = 10 } },
        { 'P', new Product { ProductSku = 'P', Price = 50 } },
        { 'Q', new Product { ProductSku = 'Q', Price = 30 } },
        { 'R', new Product { ProductSku = 'R', Price = 50 } },
        { 'S', new Product { ProductSku = 'S', Price = 20 } },
        { 'T', new Product { ProductSku = 'T', Price = 20 } },
        { 'U', new Product { ProductSku = 'U', Price = 40 } },
        { 'V', new Product { ProductSku = 'V', Price = 50 } },
        { 'W', new Product { ProductSku = 'W', Price = 20 } },
        { 'X', new Product { ProductSku = 'X', Price = 17 } },
        { 'Y', new Product { ProductSku = 'Y', Price = 20 } },
        { 'Z', new Product { ProductSku = 'Z', Price = 21 } },
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