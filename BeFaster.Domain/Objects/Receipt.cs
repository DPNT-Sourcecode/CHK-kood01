namespace BeFaster.Domain.Objects;

// i introduced this level of abstruction because at some point we would want to store and show some reciept specific info ,like reciept barcode, customer discounts, etc etc
public class Receipt
{
    private IList<ReceiptItem> _receiptItems = new List<ReceiptItem>();
    
    // we want to get all items in receipt
    public IList<ReceiptItem> GetAllItems()
    {
        return _receiptItems;
    }
    // we want to add new item to receipt
    public void AddItem(ReceiptItem item)
    {
        _receiptItems.Add(item);
    }
    // we want to calculate total
    public int CalculateTotal()
    {
        return _receiptItems.Sum(item => item.Total);
    }
    // in the future we want to add additional logic, such as: remove item, change item quantity, apply global promotions etc etc/
    
}