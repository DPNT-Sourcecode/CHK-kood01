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
            new Promotion {Type = PromotionType.BuyXGetYFree, ProductSkus = ['A'], RequiredQuantity = 2, FreeProductSku = 'B'},
            new Promotion {Type = PromotionType.BuyXGetYFree, ProductSkus = ['C'], RequiredQuantity = 2, PromoPrice = 50m},
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
            new Promotion {Type = PromotionType.BuyXGetYFree, ProductSkus = ['C'], RequiredQuantity = 2, PromoPrice = 50m},
            new Promotion {Type = PromotionType.BuyXGetYFree, ProductSkus = ['A'], RequiredQuantity = 2, FreeProductSku = 'B'},
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


    // [Test]
    // public void CreatePromotions_ValidPromotion_ReturnBulkBuyPromo()
    // {
    //     // Arrange
    //     var promotions = new List<Promotion>
    //     {
    //         new Promotion { ProductSku = 'A', RequiredQuantity = 3, PromoPrice = 125, Type = PromotionType.BulkBuy}
    //     };
    //
    //     // Act
    //     var result = _promotionFactory.CreatePromotions(promotions);
    //
    //     // Assert
    //     Assert.IsNotNull(result);
    //     Assert.IsInstanceOf<BulkBuyPromo>(result.FirstOrDefault());
    //
    //     var bulkBuyPromo = result.FirstOrDefault() as BulkBuyPromo;
    //     Assert.That(bulkBuyPromo.ProductSkus, Is.EqualTo('A'));
    // } 
    //
    // [Test]
    // public void CreatePromotions_EmptyPromotionList_ReturnEmptyList()
    // {
    //     // Arrange
    //     var promotions = new List<Promotion>();
    //
    //     // Act
    //     var result = _promotionFactory.CreatePromotions(promotions);
    //
    //     // Assert
    //     Assert.IsNotNull(result);
    //     Assert.IsEmpty(result);
    // } 
    //
    // [Test]
    // public void CreatePromotions_NullPromotionsList_ReturnNotNull()
    // {
    //     // Act 
    //     var promotioms = _promotionFactory.CreatePromotions(null);
    //     
    //     // Assert
    //     Assert.IsNotNull(promotioms);
    // } 
    //
    // [Test]
    // public void CreatePromotions_MultipleValidPromotions_ReturnCorrectPromos()
    // {
    //     // Arrange
    //     var promotions = new List<Promotion>
    //     {
    //         new Promotion { ProductSku = 'A', RequiredQuantity = 3, PromoPrice = 125, Type = PromotionType.BulkBuy},
    //         new Promotion { ProductSku = 'B', RequiredQuantity = 2, PromoPrice = 70, Type = PromotionType.BulkBuy}
    //     };
    //
    //     // Act
    //     var result = _promotionFactory.CreatePromotions(promotions);
    //     
    //     // Assert
    //     Assert.IsNotNull(result);
    //     Assert.That(result.Count(), Is.EqualTo(2));
    //
    //     var promo1 = result.ElementAt(0) as BulkBuyPromo;
    //     var promo2 = result.ElementAt(1) as BulkBuyPromo;
    //
    //     Assert.AreEqual('A', promo1.ProductSkus);
    //     Assert.AreEqual('B', promo2.ProductSkus);
    // } 
}