using BeFaster.Domain.Entities;

namespace BeFaster.Domain.Objects;

public class Basket
{
    private readonly Dictionary<char, BasketItem> _items = new();
    
    public void AddItem(Product product)
    {
        if (!_items.ContainsKey(product.ProductSku))
        {
            _items[product.ProductSku] = new BasketItem(product);
        }
        else
        {
            _items[product.ProductSku].AddItem();
        }
    }
    public List<BasketItem> GetAllItems()
    {
        return _items.Select(i => i.Value).ToList();
    }
}