using BeFaster.Domain.Entities;

namespace BeFaster.Domain.Repositories;

public class InMemoryPromotionRepository : IPromotionRepository<Promotion>
{
    private readonly List<Promotion> _promotions = new()
    {
        new Promotion {Id = 1,ProductSku = 'A',RequiredQuantity = 3,PromoPrice = 125},
        new Promotion {Id = 2,ProductSku = 'B',RequiredQuantity = 2,PromoPrice = 45},
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
