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

        var promos = new List<IPromo>();
        
        foreach (var promo in promotionEntities)
        {
            switch (promo.Type)
            {
                case PromotionType.BuyXGetYFree:
                    promos.Add(new BuyXGetYFreePromo(promo.ProductSkus, promo.RequiredQuantity, promo.FreeProductSku));
                    break;
                case PromotionType.BulkBuy:
                    promos.Add(new BulkBuyPromo(promo.ProductSkus, promo.RequiredQuantity,promo.PromoPrice));
                    break;
            }
        }

        // order is critical here, we want to return promos and apply them in order
        return promos.OrderBy(p => p.Type);
    }
}
