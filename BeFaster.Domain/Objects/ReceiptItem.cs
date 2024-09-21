using BeFaster.Core.Interfaces;

namespace BeFaster.Domain.Objects;

public class ReceiptItem
{
    public required char ProductSku { get; set; }
    public decimal Total { get; set; }
    public decimal DiscountedTotal { get; set; }
    public IPromo? AppliedPromo { get; set; }
    
    // public BasketItem BasketItem { get; set; }
    // public int Total { get; private set; }
    //
    // // This is dum implementation, In real life task i would do somthing much clever
    // // i would probably pass promotions in the receipt item, and then applied them, but for now i preffer to keep it simple 
    // // added this property to track how many items have had promotions applied
    // public int AppliedPromotionsCount { get; private set; }
    //
    // public ReceiptItem(BasketItem basketItem)
    // {
    //     BasketItem = basketItem;
    //     Total = basketItem.Product.Price * basketItem.Quantity;
    //     AppliedPromotionsCount = 0;
    // }
    //
    // public void ApplyPromotions(int totalWithDiscount, int quantityApplied)
    // {
    //     Total = totalWithDiscount;
    //
    //     // Track how many items have been affected by the promotion
    //     AppliedPromotionsCount += quantityApplied;
    // }
}