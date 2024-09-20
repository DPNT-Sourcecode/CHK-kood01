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
        { 'F', new Product { ProductSku = 'F', Price = 10 } },
        { 'F', new Product { ProductSku = 'F', Price = 10 } },
        { 'F', new Product { ProductSku = 'F', Price = 10 } },
        { 'F', new Product { ProductSku = 'F', Price = 10 } },
        { 'F', new Product { ProductSku = 'F', Price = 10 } },
        { 'F', new Product { ProductSku = 'F', Price = 10 } },
        { 'F', new Product { ProductSku = 'F', Price = 10 } },
        { 'F', new Product { ProductSku = 'F', Price = 10 } },
        { 'F', new Product { ProductSku = 'F', Price = 10 } },
    };
    
    Our price table and offers: 
    +------+-------+------------------------+
    | Item | Price | Special offers         |
    +------+-------+------------------------+
    | A    | 50    | 3A for 130, 5A for 200 |
    | B    | 30    | 2B for 45              |
    | C    | 20    |                        |
    | D    | 15    |                        |
    | E    | 40    | 2E get one B free      |
    | F    | 10    | 2F get one F free      |
    | G    | 20    |                        |
    | H    | 10    | 5H for 45, 10H for 80  |
    | I    | 35    |                        |
    | J    | 60    |                        |
    | K    | 80    | 2K for 150             |
    | L    | 90    |                        |
    | M    | 15    |                        |
    | N    | 40    | 3N get one M free      |
    | O    | 10    |                        |
    | P    | 50    | 5P for 200             |
    | Q    | 30    | 3Q for 80              |
    | R    | 50    | 3R get one Q free      |
    | S    | 30    |                        |
    | T    | 20    |                        |
    | U    | 40    | 3U get one U free      |
    | V    | 50    | 2V for 90, 3V for 130  |
    | W    | 20    |                        |
    | X    | 90    |                        |
    | Y    | 10    |                        |
    | Z    | 50    |                        |
    +------+-------+------------------------+
    
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
