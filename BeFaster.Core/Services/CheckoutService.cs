using BeFaster.Core.Interfaces;
using BeFaster.Domain.Entities;
using BeFaster.Domain.Models;
using BeFaster.Domain.Objects;
using BeFaster.Domain.Repositories;

namespace BeFaster.Core.Services;

public class CheckoutService: ICheckoutService
{
    private readonly IProductRepository<Product> _productRepository;
    private readonly IPromotionService _promotionService;

    public CheckoutService(IProductRepository<Product> productRepository, IPromotionService promotionService)
    {
        _productRepository = productRepository;
        _promotionService = promotionService;
    }

    public int GetTotal(ProductInputModel productInputModel)
    {
        if (productInputModel?.ProductSkuList?.Count < 1)
            return 0;
        
       // create basket and populate it with items based on product codes
       var basket = new Basket();
       foreach (var sku in productInputModel.ProductSkuList)
       {
           var product = _productRepository.GetByProductSku(sku);
           if (product == null) return -1;

           basket.AddItem(product);
       }
       
       // create receipt from basket items
       var receipt = new Receipt();
       foreach (var basketItem in basket.GetAllItems())
       {
           receipt.AddItem(new ReceiptItem(basketItem));
       }

       // apply promotions 
       _promotionService.ApplyPromotions(receipt.GetAllItems());
       
       // Calculate the final price after applying promos
       return receipt.CalculateTotal();
    }
}