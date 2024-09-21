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
            RemovePreviousPromotions(totalApplicableWithPromos, totalApplicableWithoutPromos, foundItems);
        }
        
        // Apply Promo 
        ApplyPromotions(foundItems);
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
}

