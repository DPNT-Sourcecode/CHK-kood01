using BeFaster.Core.Interfaces;
using BeFaster.Domain.Entities;
using BeFaster.Domain.Enums;
using BeFaster.Domain.Objects;

namespace BeFaster.Core.Services.PromotionFactory;

public class BuyXGetYFreePromo : IPromo
{
    public char ProductSku { get; }
    public PromotionType Type => PromotionType.BuyXGetYFree;
    private readonly IEnumerable<Promotion> _promotions;

    public BuyXGetYFreePromo( char productSku, IEnumerable<Promotion> promotions)
    {
        ProductSku = productSku;
        _promotions = promotions;
    }
    public int GetDiscount(Receipt receipt, char receiptKey)
    {
        var item = receipt.GetItemByKey(receiptKey);
        if (item ==  null || item.BasketItem.Product.ProductSku != ProductSku) return 0;
        
        int quantity = item.BasketItem.Quantity;
        int appliedPromos = item.AppliedPromotionsCount; // Track how many promotions were already applied

        // Calculate how many items are still eligible for promotions
        int remainingEligibleQuantity = quantity - appliedPromos;
        
        // get applicable promotions
        var applicablePromotions = _promotions
            .Where(p => p.ProductSku == ProductSku && remainingEligibleQuantity >= p.RequiredQuantity)
            .ToList();

        if (!applicablePromotions.Any()) return 0;

        foreach (var promo in applicablePromotions)
        {
            if (remainingEligibleQuantity <= 0) break;
            
            int freeItemCount = quantity / promo.RequiredQuantity;
            if (freeItemCount < 1) continue;
            
            var freeItem = receipt.GetItemByKey(promo.FreeProductSku.Value);
            if (freeItem == null) continue;
            
            var totalDiscount = freeItemCount * freeItem.BasketItem.Product.Price;
            var finalPrice = freeItem.Total - Math.Min(freeItem.Total,totalDiscount);
            
            freeItem.ApplyPromotions(finalPrice, freeItemCount);

            remainingEligibleQuantity -= freeItemCount;
        }
        
        return 0; 
    }
}