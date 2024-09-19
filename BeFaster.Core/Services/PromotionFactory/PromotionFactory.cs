using BeFaster.Core.Interfaces;
using BeFaster.Domain.Entities;

namespace BeFaster.Core.Services.PromotionFactory;

public class PromotionFactory : IPromotionFactory
{
    public IEnumerable<IPromo> CreatePromotions(IEnumerable<Promotion> promotionEntities)
    {
        if (promotionEntities == null)
            throw new ArgumentNullException(nameof(promotionEntities));
        
       return 
    }
}