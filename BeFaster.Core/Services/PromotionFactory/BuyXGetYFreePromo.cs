using BeFaster.Core.Interfaces;
using BeFaster.Domain.Objects;

namespace BeFaster.Core.Services.PromotionFactory;

public class BuyXGetYFreePromo : IPromo
{
    public char ProductSku { get; }

    private readonly int _requiredQuantity;
    private readonly int _promoPrice;
    private readonly char _freeProductSku;

    public BuyXGetYFreePromo( char productSku, int requiredQuantity, int promoPrice, char freeProductSku)
    {
        ProductSku = productSku;
        _requiredQuantity = requiredQuantity;
        _promoPrice = promoPrice;
        _freeProductSku = freeProductSku;
    }
    public int GetDiscount(Receipt receipt, char receiptKey)
    {
        var item = receipt.GetItemByKey(receiptKey);
        if (item is null) return 0;
        
        if (item.BasketItem.Product.ProductSku != ProductSku) return 0;
        
        int quantity = item.BasketItem.Quantity;

        if (quantity < _requiredQuantity) return 0;

        int freeItemCount = quantity / _requiredQuantity;

        if (freeItemCount < 1) return 0;
        
        var freeItem = receipt.GetItemByKey(_freeProductSku);
        
        if (freeItem is null) return 0;

        var totalDiscount = freeItemCount * freeItem.BasketItem.Product.Price;
        
        freeItem.ApplyPromotions(_ => totalDiscount );
        
        return 0; 
    }
}
