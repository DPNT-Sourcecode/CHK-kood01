using BeFaster.Core.Services.PromotionFactory;
using BeFaster.Domain.Entities;
using BeFaster.Domain.Objects;

[TestFixture]
public class BuyXGetYFreePromoTests
{
    private BuyXGetYFreePromo _promo;
    private Receipt _receipt;

    [SetUp]
    public void Setup()
    {
        List<char> productSkus = ['A', 'B'];
        char? freeProductSku = 'C';
        int requiredQuantity = 3;

        _promo = new BuyXGetYFreePromo(productSkus, requiredQuantity, freeProductSku);
        _receipt = new Receipt();
    }

    [Test]
    public void ApplyDiscounts_ShouldApplyPromo_WhenEligableForFreeItem()
    {
        // Arrange 
        _receipt.ReceiptItems.Add(new ReceiptItem{ProductSku = 'A', Total = 50m, DiscountedTotal = 100m, AppliedPromo = null});
        _receipt.ReceiptItems.Add(new ReceiptItem{ProductSku = 'A', Total = 50m, DiscountedTotal = 100m, AppliedPromo = null});
        _receipt.ReceiptItems.Add(new ReceiptItem{ProductSku = 'A', Total = 50m, DiscountedTotal = 100m, AppliedPromo = null});
        _receipt.ReceiptItems.Add(new ReceiptItem{ProductSku = 'C', Total = 50m, DiscountedTotal = 50m, AppliedPromo = null});
        _receipt.ReceiptItems.Add(new ReceiptItem{ProductSku = 'C', Total = 50m, DiscountedTotal = 50m, AppliedPromo = null});
        
        // Act
        _promo.ApplyDiscount(_receipt);
        
        // Assert 
        var freeItems = _receipt.ReceiptItems.Where(item => item.ProductSku == 'C' && item.AppliedPromo == _promo).ToList();

        Assert.That(freeItems.Count, Is.EqualTo(1));
        Assert.IsTrue(freeItems.All(item => item.DiscountedTotal == 0m));
    }
    
    [Test]
    public void ApplyDiscounts_ShouldApplyPromoForMultipleTimeProducts_WhenFreeItemsTwiceMoreFromRequiredQuantity()
    {
        // Arrange 
        _receipt.ReceiptItems.Add(new ReceiptItem{ProductSku = 'A', Total = 50m, DiscountedTotal = 100m, AppliedPromo = null});
        _receipt.ReceiptItems.Add(new ReceiptItem{ProductSku = 'A', Total = 50m, DiscountedTotal = 100m, AppliedPromo = null});
        _receipt.ReceiptItems.Add(new ReceiptItem{ProductSku = 'A', Total = 50m, DiscountedTotal = 100m, AppliedPromo = null});
        _receipt.ReceiptItems.Add(new ReceiptItem{ProductSku = 'A', Total = 50m, DiscountedTotal = 100m, AppliedPromo = null});
        _receipt.ReceiptItems.Add(new ReceiptItem{ProductSku = 'A', Total = 50m, DiscountedTotal = 100m, AppliedPromo = null});
        _receipt.ReceiptItems.Add(new ReceiptItem{ProductSku = 'A', Total = 50m, DiscountedTotal = 100m, AppliedPromo = null});
        _receipt.ReceiptItems.Add(new ReceiptItem{ProductSku = 'C', Total = 50m, DiscountedTotal = 50m, AppliedPromo = null});
        _receipt.ReceiptItems.Add(new ReceiptItem{ProductSku = 'C', Total = 50m, DiscountedTotal = 50m, AppliedPromo = null});
        
        // Act
        _promo.ApplyDiscount(_receipt);
        
        // Assert 
        var freeItems = _receipt.ReceiptItems.Where(item => item.ProductSku == 'C' && item.AppliedPromo == _promo).ToList();

        Assert.That(freeItems.Count, Is.EqualTo(2));
        Assert.IsTrue(freeItems.All(item => item.DiscountedTotal == 0m));
    }
    
    [Test]
    public void ApplyDiscounts_ShouldNotApplyPromo_WhenNotEnoughItemsInBasket()
    {
        // Arrange 
        _receipt.ReceiptItems.Add(new ReceiptItem{ProductSku = 'A', Total = 50m, DiscountedTotal = 100m, AppliedPromo = null});
        _receipt.ReceiptItems.Add(new ReceiptItem{ProductSku = 'C', Total = 50m, DiscountedTotal = 50m, AppliedPromo = null});
        
        // Act
        _promo.ApplyDiscount(_receipt);
        
        // Assert 
        var freeItems = _receipt.ReceiptItems.Where(item => item.ProductSku == 'C' && item.AppliedPromo == _promo).ToList();

        Assert.That(freeItems.Count, Is.EqualTo(0));
    }
}