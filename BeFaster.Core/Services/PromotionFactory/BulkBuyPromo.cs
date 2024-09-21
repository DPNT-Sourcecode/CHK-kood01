using BeFaster.Core.Interfaces;
using BeFaster.Domain.Entities;
using BeFaster.Domain.Enums;
using BeFaster.Domain.Objects;

namespace BeFaster.Core.Services.PromotionFactory;

public class BulkBuyPromo : IPromo
{
    private readonly decimal _promoPrice;
    public int RequiredQuantity { get; set; }
    public List<char> ProductSkus { get; }
    public PromotionType Type => PromotionType.BulkBuy;

    public BulkBuyPromo(List<char> productSkus, int requiredQuantity, decimal promoPrice)
    {
        ProductSkus = productSkus;
        RequiredQuantity = requiredQuantity;
        _promoPrice = promoPrice;
    }

    public void ApplyDiscount(Receipt receipt)
    {
        var foundItems = receipt.ReceiptItems
                                    .Where(w => ProductSkus.Contains(w.ProductSku) 
                                                &&(w.AppliedPromo == null 
                                                   ||(w.AppliedPromo.Type==Type && IsThisPromoBetter(w.AppliedPromo))))
                                    .OrderByDescending(o=>o.Total)
                                    .ToList();

        if (foundItems.Count < RequiredQuantity) return;

        var totalApplicableWithPromos = foundItems.Count;
        var totalApplicableWithoutPromos = foundItems.Where(w => w.AppliedPromo == null)?.Count() ?? 0;

        if (totalApplicableWithPromos != totalApplicableWithoutPromos
            && totalApplicableWithoutPromos / RequiredQuantity >= totalApplicableWithPromos / RequiredQuantity)
        {
            foundItems.RemoveAll(item => item.AppliedPromo != null);
        }
        else
        {
            // Apply removal of previous promos for applicable ReceiptItems
            
        }
        
        // Apply Promo 
        ApplyPromotions(foundItems);
        
        // var item = receipt.GetItemByKey(receiptKey);
        // if (item is null || item.BasketItem.Product.ProductSku != ProductSku)
        //     return 0;
        //
        // int quantity = item.BasketItem.Quantity;
        // int remainingEligibleQuantity = quantity - item.AppliedPromotionsCount; 
        //
        // var discount = CalculateMaxDiscount(item, remainingEligibleQuantity, _promotions.First);
        // return discount;
    }

    private void ApplyPromotions(List<ReceiptItem> foundItems)
    {
        for (int i = 0; i < foundItems.Count; i += RequiredQuantity)
        {
            var promoBatch = foundItems.Skip(i).Take(RequiredQuantity).ToList();
            if (promoBatch.Count == RequiredQuantity)
            {
                promoBatch.ForEach(item =>
                {
                    item.AppliedPromo = this;
                    item.DiscountedTotal = Math.Min(item.DiscountedTotal, _promoPrice / RequiredQuantity);
                });
            }
        }
    }

    private void RemovePreviousPromotions(int totalApplicableWithPromos, int totalApplicableWithoutPromos, List<ReceiptItem> foundItems)
    {
        var withPromosNeedToBeRemoved = (totalApplicableWithPromos / RequiredQuantity) * RequiredQuantity - totalApplicableWithoutPromos;
        var linkedList = new LinkedList<ReceiptItem>(foundItems.Where(w => w.AppliedPromo != null));
        var cur = linkedList.Last;

        var curBatch = cur?.Value?.AppliedPromo?.RequiredQuantity ?? 0;

        while (withPromosNeedToBeRemoved > 0 || curBatch > 0)
        {
            if (cur?.Value.AppliedPromo == null) continue;
            
            if (withPromosNeedToBeRemoved > 0 && curBatch <= 0)
            {
                if (cur.Value.AppliedPromo != null) curBatch = cur.Value.AppliedPromo.RequiredQuantity;
            }

            cur.Value.AppliedPromo = null;
            cur.Value.DiscountedTotal = cur.Value.Total;

            cur = cur.Previous;
            withPromosNeedToBeRemoved--;
            curBatch--;
        }

        foundItems.RemoveAll(item => item.AppliedPromo != null);
    }
    
    private bool IsThisPromoBetter(IPromo currentPromo)
    {
        if (currentPromo is BulkBuyPromo otherPromo)
        {
            return _promoPrice / RequiredQuantity < otherPromo._promoPrice / otherPromo.RequiredQuantity;
        }

        return false;
    }

    private int CalculateMaxDiscount(ReceiptItem item, int remainingQuantity, LinkedListNode<Promotion>? promotionNode)
    {
    //     // Base case: no more promotions or quantity left
    //     if (promotionNode is null)
    //         return 0;
    //
    //     int maxDiscount = 0;
    //
    //     // Skip the current promotion
    //     maxDiscount = CalculateMaxDiscount(item, remainingQuantity, promotionNode.Next);
    //
    //     // Apply the current promotion if applicable
    //     var promo = promotionNode.Value;
    //     
    //     int i = 0;
    //     while (remainingQuantity >= i * promo.RequiredQuantity)
    //     {
    //         // Calculate the quantity after applying the promo 'i' times
    //         int quantityAfterPromo = remainingQuantity - (i * promo.RequiredQuantity);
    //         
    //         // Calculate the discount for applying the promo 'i' times
    //         int plainTotal = remainingQuantity * item.BasketItem.Product.Price;
    //         int promoTotal = (i * promo.PromoPrice) + (quantityAfterPromo * item.BasketItem.Product.Price);
    //
    //         int discount = plainTotal - promoTotal;
    //
    //         // Recur with the remaining quantity after applying the current promo
    //         int remainingDiscount = CalculateMaxDiscount(item, quantityAfterPromo, promotionNode.Next);
    //
    //         maxDiscount = Math.Max(maxDiscount, discount + remainingDiscount);
    //
    //         i++;
    //     }
    //
    //     return maxDiscount;
    // }
}
