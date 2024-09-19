using BeFaster.Core.Services.PromotionFactory;
using BeFaster.Domain.Entities;

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
    public void CreatePromotions_ValidPromotion_ReturnBulkBuyPromo()
    {
        // Arrange
        var promotions = new List<Promotion>
        {
            
        }
        
        // Act
        
        
        // Assert
    } 
    
    [Test]
    public void CreatePromotions_EmptyPromotionList_ReturnEmptyList()
    {
        // Arrange
        
        
        // Act
        
        
        // Assert
    } 
    
    [Test]
    public void CreatePromotions_NullPromotionsList_ThrowsArgumentNullException()
    {
        // Arrange
        
        
        // Act
        
        
        // Assert
    } 
    
    [Test]
    public void CreatePromotions_MultipleValidPromotions_ReturnCorrectPromos()
    {
        // Arrange
        
        
        // Act
        
        
        // Assert
    } 
}