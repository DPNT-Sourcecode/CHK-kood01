using BeFaster.Domain.Entities;

namespace BeFaster.Domain.Repositories;

public class InMemoryPromotionRepository : IPromotionRepository<Promotion>
{
    private readonly List<Promotion> _promotions = new()
    {
        new Promotion {Id = 1,ProductSku = 'A',RequiredQuantity = 3,PromoPrice = 130},
        new Promotion {Id = 2,ProductSku = 'A',RequiredQuantity = 5,PromoPrice = 200},
        new Promotion {Id = 3,ProductSku = 'B',RequiredQuantity = 2,PromoPrice = 45},
        new Promotion {Id = 4,ProductSku = 'E',RequiredQuantity = 2, FreeProductSku = 'B'} // 2E get one B free
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
