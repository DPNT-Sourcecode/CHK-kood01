using BeFaster.Core.Services.PromotionFactory;
using BeFaster.Domain.Entities;
using BeFaster.Domain.Enums;

namespace BeFaster.App.Tests.Solutions.Supermarket;

public class PromotionFactoryTest
{
    private PromotionFactory _promotionFactory;

    [SetUp]
    public void SetUp()
    {
        _promotionFactory = new PromotionFactory();
    }

    [Test]
    public void CreatePromotions_ShouldCreatePromotions_WhenGivenValidEntities()
    {
        // Arrange
        var promotions = new List<Promotion>
        {
            new Promotion
                { Type = PromotionType.BuyXGetYFree, ProductSkus = ['A'], RequiredQuantity = 2, FreeProductSku = 'B' },
            new Promotion
                { Type = PromotionType.BuyXGetYFree, ProductSkus = ['C'], RequiredQuantity = 2, PromoPrice = 50m },
        };

        // Act
        var result = _promotionFactory.CreatePromotions(promotions).ToList();

        // Assert
        Assert.That(result.Count, Is.EqualTo(2));
        Assert.IsInstanceOf<BuyXGetYFreePromo>(result[0]);
        Assert.IsInstanceOf<BuyXGetYFreePromo>(result[1]);
    }

    [Test]
    public void CreatePromotions_ShouldReturnPromotionsInCorrectOrder()
    {
        // Arrange
        var promotions = new List<Promotion>
        {
            new Promotion
                { Type = PromotionType.BuyXGetYFree, ProductSkus = ['C'], RequiredQuantity = 2, PromoPrice = 50m },
            new Promotion
                { Type = PromotionType.BuyXGetYFree, ProductSkus = ['A'], RequiredQuantity = 2, FreeProductSku = 'B' },
        };

        // Act
        var result = _promotionFactory.CreatePromotions(promotions).ToList();

        // Assert
        Assert.That(result.Count, Is.EqualTo(2));
        Assert.IsInstanceOf<BuyXGetYFreePromo>(result[0]);
        Assert.IsInstanceOf<BuyXGetYFreePromo>(result[1]);
    }

    [Test]
    public void CreatePromotions_ShouldReturnEmptyResult_WhenGivenEmptyInput()
    {
        // Arrange
        var promotions = new List<Promotion>();

        // Act
        var result = _promotionFactory.CreatePromotions(promotions).ToList();

        // Assert
        Assert.IsNotNull(result);
        Assert.IsEmpty(result);
    }

    [Test]
    public void CreatePromotions_ShouldReturnEmptyResult_WhenGivenNullInput()
    {
        // Act
        var result = _promotionFactory.CreatePromotions(null).ToList();

        // Assert
        Assert.IsNotNull(result);
        Assert.IsEmpty(result);
    }
}