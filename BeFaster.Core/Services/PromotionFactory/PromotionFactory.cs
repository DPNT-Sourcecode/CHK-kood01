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
            var promoByType = promoGroup.Value.GroupBy(g => g.Type);

            foreach (var typeGroup in promoByType)
            {
                switch (typeGroup.Key)
                {
                    case PromotionType.BuyXGetYFree:
                        promos.Add(new BuyXGetYFreePromo(promoGroup.Key, typeGroup));
                        break;
                    case PromotionType.BulkBuy:
                        promos.Add(new BulkBuyPromo(promoGroup.Key, typeGroup));
                        break;
                }
            }
        }

        // order is critical here, we want to return promos and apply them in order
        return promos.OrderBy(p => p.Type);
    }
}
