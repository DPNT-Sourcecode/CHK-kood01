namespace BeFaster.Domain.Objects;

// I separated Reciept items 
public class RecieptItem
{
    public BasketItem BasketItem { get; set; }
    public int Total { get; set; }
    
    public RecieptItem(BasketItem basketItem)
    {
        BasketItem = basketItem;
    }

    // i added this part 
    public void ApplyPromotions()
    {
        
    }
}