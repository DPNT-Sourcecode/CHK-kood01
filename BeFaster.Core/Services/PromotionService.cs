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
    public void ApplyPromotions(IEnumerable<ReceiptItem> receiptItems)
    {
        var promotionEntities = _promotionRepository.GetAll();
        var promotions = _promotionFactory.CreatePromotions(promotionEntities);

        foreach (var receiptItem in receiptItems)
        {
            var aplicablePromotions = promotions.Where(promotion =>
                promotion.ProductSku == receiptItem.BasketItem.Product.ProductSku);

            int total = receiptItem.Total;
            int discount = aplicablePromotions.Sum(promotion => promotion.GetDiscount(receiptItem));
            int totalWithDiscount = total - discount;
            
            receiptItem.ApplyPromotions(_=>totalWithDiscount);
        }
    }
}
