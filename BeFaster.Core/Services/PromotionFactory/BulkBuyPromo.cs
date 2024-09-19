using BeFaster.Core.Interfaces;
using BeFaster.Domain.Objects;

namespace BeFaster.Core.Services.PromotionFactory;

public class BulkBuyPromo : IPromo
{
    public char ProductSku { get; }
    public int GetDiscount(ReceiptItem item)
    {
        if (item.BasketItem.Product.ProductSku != ProductSku) return 0;

        int quantity = item.BasketItem.Quantity;
        int pricePerItem = item.BasketItem.Product.Price;
        
    }
}