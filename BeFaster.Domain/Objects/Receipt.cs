namespace BeFaster.Domain.Objects;

// i introduced this level of abstruction because at some point we would want to store and show some reciept specific info ,like reciept barcode, customer discounts, etc etc
public class Receipt
{
    private IList<ReceiptItem> _receiptItems = new List<ReceiptItem>();
    
    // we want to get all items in receipt
    
    // we want to add new item to receipt
    // we want to calculate total 
    // in the future we want to add additional logic, such as: remove item, change item quantity, apply global promotions etc etc/
    
}