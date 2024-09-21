using BeFaster.Core.Services.PromotionFactory;
using BeFaster.Domain.Entities;
using BeFaster.Domain.Objects;

namespace BeFaster.App.Tests.Solutions.Supermarket;

public class BulkBuyPromoTest
{
    private BulkBuyPromo _promo;

    private Receipt _receipt;

    [SetUp]
    public void Setup()
    {
        List<char> productSkus = ['A', 'B'];
        decimal promoPrice = 100m;
        int requiredQuantity = 3;

        _promo = new BulkBuyPromo(productSkus, requiredQuantity, promoPrice);
        _receipt = new Receipt();
    }

    [Test]
    public void ApplyDiscounts_ShouldApplyPromo_WhenEligibleItemsPresent()
    {
        // Arrange 
        _receipt.ReceiptItems.Add(new ReceiptItem{ProductSku = 'A', Total = 50m, DiscountedTotal = 50m, AppliedPromo = null});
        _receipt.ReceiptItems.Add(new ReceiptItem{ProductSku = 'A', Total = 50m, DiscountedTotal = 50m, AppliedPromo = null});
        _receipt.ReceiptItems.Add(new ReceiptItem{ProductSku = 'A', Total = 50m, DiscountedTotal = 50m, AppliedPromo = null});
        
        // Act
        _promo.ApplyDiscount(_receipt);
        
        // Assert 
        var discountedItems = _receipt.ReceiptItems.Where(item => item.AppliedPromo == _promo).ToList();

        Assert.That(discountedItems.Count, Is.EqualTo(3));
        Assert.IsTrue(discountedItems.All(item => Math.Round(item.DiscountedTotal, 2) == 33.33m));
    }
    
    
    // [Test]
    // public void GetDiscount_EnoughQuantity_ReturnCorrectDiscount()
    // {
    //     // Arrange
    //     var productA = new Product { ProductSku = 'A', Price = 50 };
    //     var basketItem = new BasketItem(productA, 5);
    //     var receiptItem = new ReceiptItem(basketItem);
    //     var receipt = new Receipt();
    //     receipt.AddItem(receiptItem);
    //     var promotionList = new List<Promotion>
    //         { new Promotion { ProductSku = 'A', RequiredQuantity = 3, PromoPrice = 125 } };
    //     var promo = new BulkBuyPromo('A', promotionList);
    //     
    //     // Act 
    //     var discount = promo.GetDiscount(receipt, productA.ProductSku);
    //
    //     // Assert
    //     Assert.That(discount, Is.EqualTo(25));
    // }
    //
    // [Test]
    // public void GetDiscount_NotEnoughQuantity_ReturnsNoDiscount()
    // {
    //     // Arrange
    //     var productA = new Product { ProductSku = 'A', Price = 50 };
    //     var basketItem = new BasketItem(productA, 2);
    //     var receiptItem = new ReceiptItem(basketItem);
    //     var receipt = new Receipt();
    //     receipt.AddItem(receiptItem);
    //     var promotionList = new List<Promotion>
    //         { new Promotion { ProductSku = 'A', RequiredQuantity = 3, PromoPrice = 125 } };
    //     var promo = new BulkBuyPromo('A', promotionList);
    //     
    //     // Act 
    //     var discount = promo.GetDiscount(receipt, productA.ProductSku);
    //     
    //     // Assert
    //     Assert.That(discount, Is.EqualTo(0));
    // }
    //
    // [Test]
    // public void GetDiscount_DifferentProductSku_ReturnsNoDiscount()
    // {
    //     // Arrange
    //     var productA = new Product { ProductSku = 'B', Price = 50 };
    //     var basketItem = new BasketItem(productA, 5);
    //     var receiptItem = new ReceiptItem(basketItem);
    //     var receipt = new Receipt();
    //     receipt.AddItem(receiptItem);
    //     var promotionList = new List<Promotion>
    //         { new Promotion { ProductSku = 'A', RequiredQuantity = 3, PromoPrice = 125 } };
    //     var promo = new BulkBuyPromo('A', promotionList);
    //     
    //     // Act 
    //     var discount = promo.GetDiscount(receipt, productA.ProductSku);
    //     
    //     // Assert
    //     Assert.That(discount, Is.EqualTo(0));
    // }
    //
    // [Test]
    // public void GetDiscount_ZeroPricePerItem_ReturnsCorrectDiscount()
    // {
    //     // Arrange
    //     var productA = new Product { ProductSku = 'B', Price = 0 };
    //     var basketItem = new BasketItem(productA, 5);
    //     var receiptItem = new ReceiptItem(basketItem);
    //     var receipt = new Receipt();
    //     receipt.AddItem(receiptItem);
    //     var promotionList = new List<Promotion>
    //         { new Promotion { ProductSku = 'A', RequiredQuantity = 3, PromoPrice = 0 } };
    //     var promo = new BulkBuyPromo('A', promotionList);
    //     
    //     // Act 
    //     var discount = promo.GetDiscount(receipt, productA.ProductSku);
    //     
    //     // Assert
    //     Assert.That(discount, Is.EqualTo(0));
    // }
}