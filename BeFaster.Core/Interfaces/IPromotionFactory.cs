using BeFaster.Domain.Entities;

namespace BeFaster.Core.Interfaces;

public interface IPromotionFactory
{
    IEnumerable<IPromo> CreatePromotions(IEnumerable<Promotion> promotionEntities);
}