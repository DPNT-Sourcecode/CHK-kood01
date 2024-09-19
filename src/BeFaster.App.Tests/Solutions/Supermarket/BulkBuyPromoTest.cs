using BeFaster.Core.Services.PromotionFactory;
using BeFaster.Domain.Entities;
using BeFaster.Domain.Objects;

namespace BeFaster.App.Tests.Solutions.Supermarket;

public class BulkBuyPromoTest
{
    [Test]
    public void GetDiscount_EnoughQuantity_ReturnCorrectDiscount()
    {
        // Arrange
        var productA = new Product { ProductSku = 'A', Price = 50 };
        var basketItem = new BasketItem(productA, 5);
        var receiptItem = new ReceiptItem(basketItem);
        var receipt = new Receipt();
        receipt.AddItem(receiptItem);
        var promo = new BulkBuyPromo('A', 3, 125);
        
        // Act 
        var discount = promo.GetDiscount(receipt, productA.ProductSku);

        // Assert
        Assert.That(discount, Is.EqualTo(125));
    }
    
    [Test]
    public void GetDiscount_NotEnoughQuantity_ReturnsNoDiscount()
    {
        // Arrange
        var productA = new Product { ProductSku = 'A', Price = 50 };
        var basketItem = new BasketItem(productA, 2);
        var receiptItem = new ReceiptItem(basketItem);
        var receipt = new Receipt();
        receipt.AddItem(receiptItem);
        var promo = new BulkBuyPromo('A', 3, 125);
        
        // Act 
        var discount = promo.GetDiscount(receipt, productA.ProductSku);
        
        // Assert
        Assert.That(discount, Is.EqualTo(0));
    }
    
    [Test]
    public void GetDiscount_DifferentProductSku_ReturnsNoDiscount()
    {
        // Arrange
        var productA = new Product { ProductSku = 'B', Price = 50 };
        var basketItem = new BasketItem(productA, 5);
        var receiptItem = new ReceiptItem(basketItem);
        var receipt = new Receipt();
        receipt.AddItem(receiptItem);
        var promo = new BulkBuyPromo('A', 3, 125);
        
        // Act 
        var discount = promo.GetDiscount(receipt, productA.ProductSku);
        
        // Assert
        Assert.That(discount, Is.EqualTo(0));
    }
    
    [Test]
    public void GetDiscount_ZeroPricePerItem_ReturnsCorrectDiscount()
    {
        // Arrange
        var productA = new Product { ProductSku = 'B', Price = 0 };
        var basketItem = new BasketItem(productA, 5);
        var receiptItem = new ReceiptItem(basketItem);
        var receipt = new Receipt();
        receipt.AddItem(receiptItem);
        var promo = new BulkBuyPromo('A', 3, 0);
        
        // Act 
        var discount = promo.GetDiscount(receipt, productA.ProductSku);
        
        // Assert
        Assert.That(discount, Is.EqualTo(0));
    }
}
