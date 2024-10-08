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

    [Test]
    public void ApplyDiscounts_ShouldRemovePreviousPromotions_WhenBetterPromoExist()
    {
        // Arrange
        var worsePromo = new BulkBuyPromo(new List<char> { 'a' },3,120m);
        _receipt.ReceiptItems.Add(new ReceiptItem{ProductSku = 'A', Total = 50m, DiscountedTotal = 40m, AppliedPromo = worsePromo});
        _receipt.ReceiptItems.Add(new ReceiptItem{ProductSku = 'A', Total = 50m, DiscountedTotal = 40m, AppliedPromo = worsePromo});
        _receipt.ReceiptItems.Add(new ReceiptItem{ProductSku = 'A', Total = 50m, DiscountedTotal = 40m, AppliedPromo = worsePromo});
        
        // Act 
        _promo.ApplyDiscount(_receipt);

        // Assert
        var discountedItems = _receipt.ReceiptItems.Where(item => item.AppliedPromo == _promo).ToList();
        Assert.That(discountedItems.Count, Is.EqualTo(3));
        Assert.IsTrue(discountedItems.All(item => Math.Round(item.DiscountedTotal, 2) == 33.33m));
    }

    [Test]
    public void ApplyDiscounts_ShouldNotApplyPromotions_WhenNoEligibleItemsPresent()
    {
        // Arrange 
        _receipt.ReceiptItems.Add(new ReceiptItem{ProductSku = 'A', Total = 50m, DiscountedTotal = 50m, AppliedPromo = null});
        _receipt.ReceiptItems.Add(new ReceiptItem{ProductSku = 'B', Total = 50m, DiscountedTotal = 50m, AppliedPromo = null});
        
        // Act
        _promo.ApplyDiscount(_receipt);
        
        // Assert
        var discountedItems = _receipt.ReceiptItems.Where(item => item.AppliedPromo == _promo).ToList();
        Assert.That(discountedItems.Count, Is.EqualTo(0));
    }
    
    [Test]
    public void ApplyDiscounts_ShouldApplyCorrectNumberOfPromos_WhenMoreItems()
    {
        // Arrange 
        _receipt.ReceiptItems.Add(new ReceiptItem{ProductSku = 'A', Total = 50m, DiscountedTotal = 50m, AppliedPromo = null});
        _receipt.ReceiptItems.Add(new ReceiptItem{ProductSku = 'A', Total = 50m, DiscountedTotal = 50m, AppliedPromo = null});
        _receipt.ReceiptItems.Add(new ReceiptItem{ProductSku = 'A', Total = 50m, DiscountedTotal = 50m, AppliedPromo = null});
        _receipt.ReceiptItems.Add(new ReceiptItem{ProductSku = 'A', Total = 50m, DiscountedTotal = 50m, AppliedPromo = null});
        _receipt.ReceiptItems.Add(new ReceiptItem{ProductSku = 'B', Total = 50m, DiscountedTotal = 50m, AppliedPromo = null});
        
        // Act
        _promo.ApplyDiscount(_receipt);
        
        // Assert
        var discountedItems = _receipt.ReceiptItems.Where(item => item.AppliedPromo == _promo).ToList();
        Assert.That(discountedItems.Count, Is.EqualTo(3));
    }
    
    [Test]
    public void ApplyDiscounts_ShouldRemovePromos_WhenAplicableCountConditionsMet()
    {
        // Arrange
        var promo = new BulkBuyPromo(new List<char> { 'A' },2,40m);
        _receipt.ReceiptItems.Add(new ReceiptItem{ProductSku = 'A', Total = 50m, DiscountedTotal = 50m, AppliedPromo = _promo});
        _receipt.ReceiptItems.Add(new ReceiptItem{ProductSku = 'A', Total = 50m, DiscountedTotal = 50m, AppliedPromo = _promo});
        _receipt.ReceiptItems.Add(new ReceiptItem{ProductSku = 'A', Total = 50m, DiscountedTotal = 50m, AppliedPromo = _promo});
        _receipt.ReceiptItems.Add(new ReceiptItem{ProductSku = 'B', Total = 50m, DiscountedTotal = 50m, AppliedPromo = _promo});
        
        // Act
        promo.ApplyDiscount(_receipt);
        
        // Assert
        var discountedItems = _receipt.ReceiptItems.Where(item => item.AppliedPromo == promo).ToList();
        Assert.That(discountedItems.Count, Is.EqualTo(2));
        Assert.IsTrue(_receipt.ReceiptItems.All(item=>item.AppliedPromo==null||item.ProductSku!='A'||discountedItems.Contains(item)));
    }
}