using BeFaster.Core.Interfaces;
using BeFaster.Domain.Entities;
using BeFaster.Domain.Enums;
using BeFaster.Domain.Objects;

namespace BeFaster.Core.Services.PromotionFactory;

public class BuyXGetYFreePromo : IPromo
{
    private readonly char? _freeProductSku;
    public List<char> ProductSkus { get; }
    public int RequiredQuantity { get; set; }
    public PromotionType Type => PromotionType.BuyXGetYFree;

    public BuyXGetYFreePromo( List<char> productSkus,  int requiredQuantity, char? freeProductSku)
    {
        ProductSkus = productSkus;
        RequiredQuantity = requiredQuantity;
        _freeProductSku = freeProductSku;
    }

    public void ApplyDiscount(Receipt receipt)
    {
        var foundItems = receipt.ReceiptItems
            .Where(w => ProductSkus.Contains(w.ProductSku) && (w.AppliedPromo == null))
            .OrderByDescending(o => o.Total)
            .ToList();


        int freeItemCont = foundItems.Count / RequiredQuantity;
        var promoBatch = receipt.ReceiptItems.Where(w => w.ProductSku == _freeProductSku).Take(freeItemCont).ToList();

        // we assuming that this type of Promo is more important than BulkBuy, so we applying it right away, if anything changes - need to extend logic similar to buy bulk promo
        promoBatch.ForEach(item =>
        {
            item.AppliedPromo = this;
            item.DiscountedTotal = 0m;
        });
    }
}

