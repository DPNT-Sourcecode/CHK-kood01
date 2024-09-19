namespace BeFaster.Domain.Objects;

// I introduced this level of abstruction because at some point we would want to store and show some reciept specific info ,like reciept barcode, customer discounts, etc etc
public class Receipt
{
    private readonly IDictionary<char, ReceiptItem> _receiptItems = new Dictionary<char, ReceiptItem>();
    
    public IDictionary<char, ReceiptItem> GetAllItems()
    {
        return _receiptItems;
    }

    public ReceiptItem? GetItemByKey(char key)
    {
        _receiptItems.TryGetValue(key, out var item);
        return item;
    }
    
    public void AddItem(ReceiptItem item)
    {
        _receiptItems.TryAdd(item.BasketItem.Product.ProductSku,item);
    }
    
    public int CalculateTotal()
    {
        return _receiptItems.Sum(item => item.Value.Total);
    }
    // in the future we want to add additional logic, such as: remove item, change item quantity, apply global promotions etc etc/
    
}