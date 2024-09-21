using BeFaster.Core.Interfaces;
using BeFaster.Core.Services;
using BeFaster.Core.Services.PromotionFactory;
using BeFaster.Domain.Entities;
using BeFaster.Domain.Objects;
using BeFaster.Domain.Repositories;
using Moq;

namespace BeFaster.App.Tests.Solutions.Supermarket;

public class PromoServiceTest
{
    private Mock<IPromotionRepository<Promotion>> _mockPromotionRepository;
    private Mock<IPromotionFactory> _mockPromotionFactory;
    private PromotionService _promotionService;

    [SetUp]
    public void SetUp()
    {
        _mockPromotionRepository = new Mock<IPromotionRepository<Promotion>>();
        _mockPromotionFactory = new Mock<IPromotionFactory>();
        _promotionService = new PromotionService(_mockPromotionRepository.Object, _mockPromotionFactory.Object);
    }

    [Test]
    public void ApplyPromotions_ShouldNotThrow_WhenReceiptIsEmpty()
    {
        // Arrange
        var receipt = new Receipt();
        _mockPromotionRepository.Setup(repo => repo.GetAll()).Returns(Enumerable.Empty<Promotion>().AsQueryable());

        // Act & Assert
        Assert.DoesNotThrow(() => _promotionService.ApplyPromotions(receipt));
    }

    // [Test]
    // public void ApplyPromotions_ValidPromotions_ApploesCorrectDiscounts()
    // {
    //     // Arrange
    //     var productA = new Product { ProductSku = 'A', Price = 50 };
    //     var basketItem = new BasketItem(productA, 3);
    //     var receiptItem = new ReceiptItem(basketItem);
    //     var promotionList = new List<Promotion>
    //         { new Promotion { ProductSku = 'A', RequiredQuantity = 3, PromoPrice = 125 } };
    //     var promotions = new List<IPromo>
    //     {
    //         new BulkBuyPromo('A', promotionList)
    //     };
    //     var promotionEntities = new List<Promotion>
    //     {
    //         new Promotion { ProductSku = 'A', RequiredQuantity = 3, PromoPrice = 125 }
    //     };
    //     var receipt = new Receipt();
    //     receipt.AddItem(receiptItem);
    //
    //     _mockPromotionRepository.Setup(repo => repo.GetAll()).Returns(promotionEntities);
    //     _mockPromotionFactory.Setup(factory => factory.CreatePromotions(promotionEntities)).Returns(promotions);
    //     
    //     // Act
    //     _promotionService.ApplyPromotions(receipt);
    //     
    //     // Assert
    //     Assert.That(receiptItem.Total, Is.EqualTo(125));
    // }
}