using BeFaster.Core.Interfaces;
using BeFaster.Domain.Entities;

namespace BeFaster.Core.Services.PromotionFactory;

public class PromotionFactory : IPromotionFactory
{
    public IEnumerable<IPromo> CreatePromotions(IEnumerable<Promotion> promotionEntities)
    {
        if (promotionEntities == null)
            return new List<IPromo>();

        var bulkPromos = promotionEntities.Select(entity =>
            new BulkBuyPromo(entity.ProductSku, entity.RequiredQuantity, entity.PromoPrice));
        
        return bulkPromos;
    }
}
