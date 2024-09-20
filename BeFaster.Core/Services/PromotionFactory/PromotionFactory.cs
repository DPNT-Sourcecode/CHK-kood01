using BeFaster.Core.Interfaces;
using BeFaster.Domain.Entities;
using BeFaster.Domain.Enums;

namespace BeFaster.Core.Services.PromotionFactory;

public class PromotionFactory : IPromotionFactory
{
    public IEnumerable<IPromo> CreatePromotions(IEnumerable<Promotion> promotionEntities)
    {
        if (promotionEntities == null)
            return Enumerable.Empty<IPromo>();

        // Group promotions by SKU
        Dictionary<char, List<Promotion>> promotionsGrouped = promotionEntities
            .GroupBy(p => p.ProductSku)
            .ToDictionary(g => g.Key, g => g.ToList());
        
        var promos = new List<IPromo>();
        
        foreach (var promoGroup in promotionsGrouped)
        {
            promos.AddRange(promoGroup.Value
                .Where(p => p.FreeProductSku.HasValue)
                .Select(p => new BuyXGetYFreePromo(promoGroup.Key, new List<Promotion> { p })));

        
            promos.Add(new BulkBuyPromo(promoGroup.Key, promoGroup.Value
                .Where(p => !p.FreeProductSku.HasValue)
                .ToList()));
        }
        
        return promos.OrderBy(p => p.Type);
    }
}