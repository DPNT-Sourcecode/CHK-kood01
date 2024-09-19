using BeFaster.Core.Interfaces;
using BeFaster.Domain.Entities;

namespace BeFaster.Core.Services.PromotionFactory;

public class PromotionFactory : IPromotionFactory
{
    public IEnumerable<IPromo> CreatePromotions(IEnumerable<Promotion> promotionEntities)
    {
        if (promotionEntities == null)
            return Enumerable.Empty<IPromo>();

        var bulkPromos = promotionEntities.Select<Promotion, IPromo>(entity =>
        {
            // we can do the same with Type field, as i described in Promotion Entity class, i decided to keep it simple for now 
            if (entity.FreeProductSku.HasValue)
            {
                return new BuyXGetYFreePromo(entity.ProductSku, entity.RequiredQuantity, entity.PromoPrice,entity.FreeProductSku.Value);
            }
            else
            {
                return new BulkBuyPromo(entity.ProductSku, entity.RequiredQuantity, entity.PromoPrice);
            }
        });
        
        return bulkPromos;
    }
}
