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
    
    // this service can handle multiple promotions per item and apply all promos at once. 
    
    //BUT in the future we would want more advanced logic for this service 
    // some promotions are stackable, but some of them not
    // some promos could potentially cancel another promotion, proobably even from another item
    // anyway, i decided to keep it simple and not to overengineering here; 
    public void ApplyPromotions(Receipt receipt)
    {
        var promotionEntities = _promotionRepository.GetAll();
        var promotions = _promotionFactory.CreatePromotions(promotionEntities);
        if (!promotions.Any()) return;

        var receiptItems = receipt.GetAllItems();
        
        foreach (var receiptItem in receiptItems)
        {
            var aplicablePromotions = promotions.Where(promotion =>
                promotion.ProductSku == receiptItem.Value.BasketItem.Product.ProductSku);
            
            int total = receiptItem.Value.Total;
            int discount = aplicablePromotions.Max(promotion => promotion.GetDiscount(receipt, receiptItem.Key));
            int totalWithDiscount = total - discount;
            
            receiptItem.Value.ApplyPromotions(totalWithDiscount);
        }
    }
}
