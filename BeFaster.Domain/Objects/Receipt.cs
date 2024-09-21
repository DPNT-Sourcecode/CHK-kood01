namespace BeFaster.Domain.Objects;

public class Receipt
{
    //private readonly IDictionary<char, ReceiptItem> _receiptItems = new Dictionary<char, ReceiptItem>();
    public readonly List<ReceiptItem> ReceiptItems = new();
    //
    // public IDictionary<char, ReceiptItem> GetAllItems()
    // {
    //     return _receiptItems;
    // }
    //
    // public ReceiptItem? GetItemByKey(char key)
    // {
    //     _receiptItems.TryGetValue(key, out var item);
    //     return item;
    // }
    
    public void AddItem(ReceiptItem item)
    {
        ReceiptItems.Add(item);
    }
    
    public int CalculateTotal()
    {
        return (int)Math.Round(ReceiptItems.Sum(s => s.DiscountedTotal),2,MidpointRounding.AwayFromZero);
    }
    // in the future we want to add additional logic, such as: remove item, change item quantity, apply global promotions etc etc/
    
}
