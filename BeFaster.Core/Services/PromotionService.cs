using BeFaster.Core.Interfaces;
using BeFaster.Domain.Entities;
using BeFaster.Domain.Objects;
using BeFaster.Domain.Repositories;

namespace BeFaster.Core.Services;

public class PromotionService : IPromotionService
{
    private readonly IPromotionRepository<Promotion> _promotionRepository;
    private readonly IPromotionFactory _promotionFactory;


    public PromotionService(IPromotionRepository<Promotion> promotionRepository, IPromotionFactory promotionFactory)
    {
        _promotionRepository = promotionRepository;
        _promotionFactory = promotionFactory;
    }
    
    // Because we need promotions to be applied in specific order, we applying promotions one buy one to all receipt. 
    // potentially it should not cause performance issues, unless we have millions promos per receipt,
    // I'm assuming that we would normaly have dozens, or hundreds promotions per receipt, not millions, otherwise it will take more advanced logic
    
    public void ApplyPromotions(Receipt receipt)
    {
        var receiptSkus = receipt.ReceiptItems
            .Select(item => item.ProductSku)
            .Distinct()
            .ToList();

        var promotionEntities = _promotionRepository
            .GetAll()
            .Where(w => w.ProductSkus.Any(sku => receiptSkus.Contains(sku)));

        var promotions = _promotionFactory.CreatePromotions(promotionEntities).ToList();
        if (!promotions.Any()) return;

        foreach (IPromo promotion in promotions)
        {
            promotion.ApplyDiscount(receipt);
        }
    }
}