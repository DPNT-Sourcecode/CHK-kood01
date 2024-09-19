using BeFaster.Domain.Entities;

namespace BeFaster.Domain.Objects;

public class Basket
{
    private Dictionary<char, BasketItem> _items = new();
    
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
    // i added this method just in case if we need to show the plain basket price to the customer before applying any discounts and promotions 
    // depending on the frequincy of calls we may want to calculate price every time during add\remove process, or just do it like that
    public int CalculateTotalPrice()
    {
        return _items.Sum(item => item.Value.PlainAmount);
    }
}