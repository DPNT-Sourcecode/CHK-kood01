// using BeFaster.Core.Interfaces;
// using BeFaster.Domain.Entities;
// using BeFaster.Domain.Objects;
// using BeFaster.Domain.Repositories;
//
// namespace BeFaster.Core.Services;
//
// public class PromotionService : IPromotionService
// {
//     private readonly IPromotionRepository<Promotion> _promotionRepository;
//     private readonly IPromotionFactory _promotionFactory;
//
//
//     public PromotionService(IPromotionRepository<Promotion> promotionRepository, IPromotionFactory promotionFactory)
//     {
//         _promotionRepository = promotionRepository;
//         _promotionFactory = promotionFactory;
//     }
//     
//     // this service can handle multiple promotions per item and apply all promos at once. 
//     
//     //BUT in the future we would want more advanced logic for this service 
//     // some promotions are stackable, but some of them not
//     // some promos could potentially cancel another promotion, proobably even from another item
//     // anyway, i decided to keep it simple and not to overengineering here; 
//     public void ApplyPromotions(Receipt receipt)
//     {
//         var receiptSkus = receipt.GetAllItems()
//             .Select(item => item.Value.BasketItem.Product.ProductSku)
//             .Distinct()
//             .ToList();
//         
//         var promotionEntities = _promotionRepository.GetAll().Where(w=>receiptSkus.Contains(w.ProductSku));
//         var promotions = _promotionFactory.CreatePromotions(promotionEntities);
//         if (!promotions.Any()) return;
//
//         foreach (var promotion in promotions)
//         {
//             var applicableItems = receipt.GetAllItems()
//                 .Where(item => item.Value.BasketItem.Product.ProductSku == promotion.ProductSku)
//                 .ToList();
//             
//             if (!applicableItems.Any()) continue;
//             
//             foreach (var receiptItem in applicableItems)
//             {
//                 // Calculate remaining eligible quantity
//                 int remainingEligibleQuantity = receiptItem.Value.BasketItem.Quantity - receiptItem.Value.AppliedPromotionsCount;
//                 
//                 // Check exit condition
//                 if (remainingEligibleQuantity <= 0) continue;
//                 
//                 // Apply the discount from the promotion
//                 var discount = promotion.GetDiscount(receipt, receiptItem.Key);
//                 
//                 int total = receiptItem.Value.Total;
//                 // Ensure that the total discount doesn’t exceed the total value
//                 int totalWithDiscount = total - Math.Min(total, discount);
//
//                 // Update the receipt item with the discounted price
//                 receiptItem.Value.ApplyPromotions(totalWithDiscount, remainingEligibleQuantity);
//             }
//         }
//     }
// }