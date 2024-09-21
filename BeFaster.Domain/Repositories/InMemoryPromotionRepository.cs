using BeFaster.Domain.Entities;
using BeFaster.Domain.Enums;

namespace BeFaster.Domain.Repositories;

public class InMemoryPromotionRepository : IPromotionRepository<Promotion>
{
    private readonly List<Promotion> _promotions = new()
    {
        new Promotion {ProductSkus = ['A'],RequiredQuantity = 3,PromoPrice = 130, Type = PromotionType.BulkBuy},
        new Promotion {ProductSkus = ['A'],RequiredQuantity = 5,PromoPrice = 200, Type = PromotionType.BulkBuy},
        new Promotion {ProductSkus = ['B'],RequiredQuantity = 2,PromoPrice = 45, Type = PromotionType.BulkBuy},
        new Promotion {ProductSkus = ['E'],RequiredQuantity = 2, FreeProductSku = 'B', Type = PromotionType.BuyXGetYFree}, // 2E get one B free
        new Promotion {ProductSkus = ['F'],RequiredQuantity = 3, FreeProductSku = 'F', Type = PromotionType.BuyXGetYFree},  // 2F get one F free   |YOU have to have at least 3 items in a basket
        new Promotion {ProductSkus = ['H'],RequiredQuantity = 5, PromoPrice = 45, Type = PromotionType.BulkBuy},
        new Promotion {ProductSkus = ['H'],RequiredQuantity = 10, PromoPrice = 80, Type = PromotionType.BulkBuy},
        new Promotion {ProductSkus = ['k'],RequiredQuantity = 2, PromoPrice = 150, Type = PromotionType.BulkBuy},
        new Promotion {ProductSkus = ['N'],RequiredQuantity = 3, FreeProductSku = 'M', Type = PromotionType.BuyXGetYFree},
        new Promotion {ProductSkus = ['P'],RequiredQuantity = 5, PromoPrice = 200, Type = PromotionType.BulkBuy},
        new Promotion {ProductSkus = ['Q'],RequiredQuantity = 3, PromoPrice = 80, Type = PromotionType.BulkBuy},
        new Promotion {ProductSkus = ['R'],RequiredQuantity = 3, FreeProductSku = 'Q', Type = PromotionType.BuyXGetYFree},
        new Promotion {ProductSkus = ['U'],RequiredQuantity = 4, FreeProductSku = 'U', Type = PromotionType.BuyXGetYFree},
        new Promotion {ProductSkus = ['V'],RequiredQuantity = 2, PromoPrice = 90, Type = PromotionType.BulkBuy},
        new Promotion {ProductSkus = ['V'],RequiredQuantity = 3, PromoPrice = 130, Type = PromotionType.BulkBuy},
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
