namespace BeFaster.Domain.Objects;

// i introduced this level of abstruction because at some point we would want to store and show some reciept specific info ,like reciept barcode, customer discounts, etc etc
public class Receipt
{
    private IList<ReceiptItem> _receiptItems = new List<ReceiptItem>();
    
    public IList<ReceiptItem> GetAllItems()
    {
        return _receiptItems;
    }
    
    public void AddItem(ReceiptItem item)
    {
        _receiptItems.Add(item);
    }
    
    public int CalculateTotal()
    {
        return _receiptItems.Sum(item => item.Total);
    }
    // in the future we want to add additional logic, such as: remove item, change item quantity, apply global promotions etc etc/
    
}
