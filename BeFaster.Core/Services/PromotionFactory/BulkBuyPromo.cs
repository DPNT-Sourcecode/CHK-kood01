using BeFaster.Core.Interfaces;
using BeFaster.Domain.Objects;

namespace BeFaster.Core.Services.PromotionFactory;

public class BulkBuyPromo : IPromo
{
    public char ProductSku { get; }

    private readonly int _requiredQuantity;
    private readonly int _promoPrice;

    public BulkBuyPromo( char productSku, int requiredQuantity, int promoPrice)
    {
        ProductSku = productSku;
        _requiredQuantity = requiredQuantity;
        _promoPrice = promoPrice;
    }

    public int GetDiscount(Receipt receipt, char receiptKey)
    {
        var item = receipt.GetItemByKey(receiptKey);

        if (item is null) return 0;
        
        if (item.BasketItem.Product.ProductSku != ProductSku) return 0;

        int quantity = item.BasketItem.Quantity;
        int pricePerItem = item.BasketItem.Product.Price;

        if (quantity < _requiredQuantity) return 0;

        int plainTotal = quantity * pricePerItem;
        int promoTotal = (quantity / _requiredQuantity) * _promoPrice
                         + (quantity % _requiredQuantity) * pricePerItem;

        return plainTotal - promoTotal;
    }
}