namespace BeFaster.Domain.Objects;

// I separated Receipt items from basket, because these are two different logical entities.  
// Reciept is the part of checkout logic, while basket is part of buing process. 
public class ReceiptItem
{
    public BasketItem BasketItem { get; set; }
    public int Total { get; private set; }
    
    public ReceiptItem(BasketItem basketItem)
    {
        BasketItem = basketItem;
        Total = basketItem.Product.Price * basketItem.Quantity;
    }

    public void ApplyPromotions(int promotion)
    {
        Total = promotion;
    }
}
