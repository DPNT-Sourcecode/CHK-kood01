using BeFaster.Core.Services.PromotionFactory;
using BeFaster.Domain.Entities;
using BeFaster.Domain.Objects;

[TestFixture]
public class BuyXGetYFreePromoTests
{
    private BuyXGetYFreePromo _promo;

    [SetUp]
    public void SetUp()
    {
        _promo = new BuyXGetYFreePromo('A', new List<Promotion>{new Promotion {ProductSku = 'A', RequiredQuantity = 3, FreeProductSku = 'B'}});
    }

    [Test]
    public void GetDiscount_ShouldReturnZero_WhenReceiptDoesNotContainProduct()
    {
        // Arrange 
        var receipt = new Receipt();

        // Act
        var discount = _promo.GetDiscount(receipt, 'A');

        // Assert
        Assert.AreEqual(0, discount);
    }

    [Test]
    public void GetDiscount_ShouldApplyPromotion_WhenEligibleForFreeItem()
    {
        // Arrange
        var receipt = new Receipt();
        var basketItemA = new BasketItem( new Product { ProductSku = 'A', Price = 50 }, 6); 
        var basketItemB = new BasketItem( new Product { ProductSku = 'B', Price = 10 }, 2); 

        var receiptItemA = new ReceiptItem(basketItemA);
        var receiptItemB = new ReceiptItem(basketItemB);

        receipt.AddItem(receiptItemA);
        receipt.AddItem(receiptItemB);

        // Act
        var discount = _promo.GetDiscount(receipt, 'A');

        // Assert
        Assert.AreEqual(0, discount); 
        Assert.AreEqual(0, receiptItemB.Total); 
        Assert.AreEqual(300, receiptItemA.Total);
    }
}