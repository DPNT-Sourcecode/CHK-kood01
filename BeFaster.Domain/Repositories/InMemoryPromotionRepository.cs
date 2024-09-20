using BeFaster.Domain.Entities;
using BeFaster.Domain.Enums;

namespace BeFaster.Domain.Repositories;

public class InMemoryPromotionRepository : IPromotionRepository<Promotion>
{
    private readonly List<Promotion> _promotions = new()
    {
        new Promotion {ProductSku = 'A',RequiredQuantity = 3,PromoPrice = 130, Type = PromotionType.BulkBuy},
        new Promotion {ProductSku = 'A',RequiredQuantity = 5,PromoPrice = 200, Type = PromotionType.BulkBuy},
        new Promotion {ProductSku = 'B',RequiredQuantity = 2,PromoPrice = 45, Type = PromotionType.BulkBuy},
        new Promotion {ProductSku = 'E',RequiredQuantity = 2, FreeProductSku = 'B', Type = PromotionType.BuyXGetYFree} // 2E get one B free
    };
    public IEnumerable<Promotion> GetAll()
    {
        return _promotions;
    }

    public void Add(Promotion entity)
    {
        _promotions.Add(entity);
    }
}