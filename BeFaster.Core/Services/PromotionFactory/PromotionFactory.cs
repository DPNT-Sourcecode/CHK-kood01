using BeFaster.Core.Interfaces;
using BeFaster.Domain.Entities;

namespace BeFaster.Core.Services.PromotionFactory;

public class PromotionFactory : IPromotionFactory
{
    public IEnumerable<IPromo> CreatePromotions(IEnumerable<Promotion> promotionEntities)
    {
        if (promotionEntities == null)
            return Enumerable.Empty<IPromo>();

        // Group promotions by SKU

        var promotionsGrouped = promotionEntities
            .GroupBy(p => p.ProductSku)
            .ToDictionary(g => g.Key, g => g.ToList());

        var bulkPromos = promotionsGrouped.Select(g =>
        {
            var promotions = g.Value;

            promotions.Where(p => p.FreeProductSku.HasValue).Select(entity =>
            {
                return new BuyXGetYFreePromo(entity.ProductSku, entity.RequiredQuantity, entity.PromoPrice,
                    entity.FreeProductSku.Value);
            });

            promotions.Where(p => !p.FreeProductSku.HasValue).Select(entity =>
            {
                return new BulkBuyPromo(entity.ProductSku, entity.RequiredQuantity, entity.PromoPrice);
            });
        });
        
        return bulkPromos;
    }
}
