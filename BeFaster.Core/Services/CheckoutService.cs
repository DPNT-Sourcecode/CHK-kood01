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

       var receipt = new Receipt();

       foreach (var sku in productInputModel.ProductSkuList)
       {
           var product = _productRepository.GetByProductSku(sku);
           if (product == null) return -1;

           receipt.AddItem(new ReceiptItem
           {
                ProductSku = sku,
                Total = product.Price,
                DiscountedTotal = product.Price
           });
       }

       // Apply promotions
       _promotionService.ApplyPromotions(receipt);

       // Calculate the final price after applying promotions
       return receipt.CalculateTotal();
    }
}
