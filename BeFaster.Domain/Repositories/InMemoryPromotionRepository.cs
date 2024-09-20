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
        new Promotion {ProductSku = 'E',RequiredQuantity = 2, FreeProductSku = 'B', Type = PromotionType.BuyXGetYFree}, // 2E get one B free
        new Promotion {ProductSku = 'F',RequiredQuantity = 3, FreeProductSku = 'F', Type = PromotionType.BuyXGetYFree},  // 2F get one F free   |YOU have to have at least 3 items in a basket
        new Promotion {ProductSku = 'H',RequiredQuantity = 5, PromoPrice = 45, Type = PromotionType.BulkBuy},
        new Promotion {ProductSku = 'H',RequiredQuantity = 10, PromoPrice = 80, Type = PromotionType.BulkBuy},
        new Promotion {ProductSku = 'K',RequiredQuantity = 2, PromoPrice = 150, Type = PromotionType.BulkBuy},
        new Promotion {ProductSku = 'N',RequiredQuantity = 3, FreeProductSku = 'M', Type = PromotionType.BuyXGetYFree},
        new Promotion {ProductSku = 'P',RequiredQuantity = 5, PromoPrice = 200, Type = PromotionType.BulkBuy},
        new Promotion {ProductSku = 'Q',RequiredQuantity = 3, PromoPrice = 80, Type = PromotionType.BulkBuy},
        new Promotion {ProductSku = 'R',RequiredQuantity = 3, FreeProductSku = 'Q', Type = PromotionType.BuyXGetYFree},
        new Promotion {ProductSku = 'U',RequiredQuantity = 4, FreeProductSku = 'U', Type = PromotionType.BuyXGetYFree},
        new Promotion {ProductSku = 'V',RequiredQuantity = 2, PromoPrice = 90, Type = PromotionType.BulkBuy},
        new Promotion {ProductSku = 'V',RequiredQuantity = 3, PromoPrice = 130, Type = PromotionType.BulkBuy},
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