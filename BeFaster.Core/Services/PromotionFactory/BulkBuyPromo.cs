using BeFaster.Core.Interfaces;
using BeFaster.Domain.Entities;
using BeFaster.Domain.Enums;
using BeFaster.Domain.Objects;

namespace BeFaster.Core.Services.PromotionFactory;

public class BulkBuyPromo : IPromo
{
    public char ProductSku { get; }
    public PromotionType Type => PromotionType.BulkBuy;
    private readonly LinkedList<Promotion> _promotions;

    public BulkBuyPromo( char productSku, IEnumerable<Promotion> promotions)
    {
        ProductSku = productSku;
        _promotions = new LinkedList<Promotion>(promotions);
    }

    public int GetDiscount(Receipt receipt, char receiptKey)
    {
        var item = receipt.GetItemByKey(receiptKey);
        if (item is null || item.BasketItem.Product.ProductSku != ProductSku)
            return 0;

        int quantity = item.BasketItem.Quantity;
        int remainingEligibleQuantity = quantity - item.AppliedPromotionsCount; 
        
        var discount = CalculateMaxDiscount(item, remainingEligibleQuantity, _promotions.First);
        return discount;
    }

    private int CalculateMaxDiscount(ReceiptItem item, int remainingQuantity, LinkedListNode<Promotion>? promotionNode)
    {
        // Base case: no more promotions or quantity left
        if (promotionNode is null)
            return 0;

        int maxDiscount = 0;

        // Skip the current promotion
        maxDiscount = CalculateMaxDiscount(item, remainingQuantity, promotionNode.Next);

        // Apply the current promotion if applicable
        var promo = promotionNode.Value;
        
        int i = 0;
        while (remainingQuantity >= i * promo.RequiredQuantity)
        {
            // Calculate the quantity after applying the promo 'i' times
            int quantityAfterPromo = remainingQuantity - (i * promo.RequiredQuantity);
            
            // Calculate the discount for applying the promo 'i' times
            int plainTotal = remainingQuantity * item.BasketItem.Product.Price;
            int promoTotal = (i * promo.PromoPrice) + (quantityAfterPromo * item.BasketItem.Product.Price);

            int discount = plainTotal - promoTotal;

            // Recur with the remaining quantity after applying the current promo
            int remainingDiscount = CalculateMaxDiscount(item, quantityAfterPromo, promotionNode.Next);

            maxDiscount = Math.Max(maxDiscount, discount + remainingDiscount);

            i++;
        }

        return maxDiscount;
    }
}