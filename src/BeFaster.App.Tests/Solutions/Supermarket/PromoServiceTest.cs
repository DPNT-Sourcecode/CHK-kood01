using BeFaster.Core.Interfaces;
using BeFaster.Core.Services;
using BeFaster.Core.Services.PromotionFactory;
using BeFaster.Domain.Entities;
using BeFaster.Domain.Enums;
using BeFaster.Domain.Objects;
using BeFaster.Domain.Repositories;
using Moq;

namespace BeFaster.App.Tests.Solutions.Supermarket;

public class PromoServiceTest
{
    private Receipt _receipt;
    private Mock<IPromotionRepository<Promotion>> _mockPromotionRepository;
    private Mock<IPromotionFactory> _mockPromotionFactory;
    private PromotionService _promotionService;

    [SetUp]
    public void SetUp()
    {
        _receipt = new Receipt();
        _mockPromotionRepository = new Mock<IPromotionRepository<Promotion>>();
        _mockPromotionFactory = new Mock<IPromotionFactory>();
        _promotionService = new PromotionService(_mockPromotionRepository.Object, _mockPromotionFactory.Object);
    }

    [Test]
    public void ApplyPromotions_ShouldNotThrow_WhenReceiptIsEmpty()
    {
        // Arrange
        _mockPromotionRepository.Setup(repo => repo.GetAll()).Returns(Enumerable.Empty<Promotion>().AsQueryable());

        // Act & Assert
        Assert.DoesNotThrow(() => _promotionService.ApplyPromotions(_receipt));
    }
    
    [Test]
    public void ApplyPromotions_ShouldApplyValidPromotions_WhenApplicable()
    {
        // Arrange
        _receipt.ReceiptItems.Add(new ReceiptItem{ProductSku = 'A', Total = 100m, DiscountedTotal = 100m, AppliedPromo = null});
        _receipt.ReceiptItems.Add(new ReceiptItem{ProductSku = 'B', Total = 200m, DiscountedTotal = 200m, AppliedPromo = null});

        var promotions = new List<Promotion>
        {
            new Promotion { ProductSkus = ['A', 'B'] }
        };

        _mockPromotionRepository.Setup(repo => repo.GetAll()).Returns(promotions.AsQueryable());
        _mockPromotionFactory.Setup(factory => factory.CreatePromotions(It.IsAny<IEnumerable<Promotion>>()))
            .Returns(new List<IPromo> { new BulkBuyPromo(new List<char> { 'A', 'B' }, 2, 150m) });
        
        // Act 
        _promotionService.ApplyPromotions(_receipt);
        
        // Assert
        var discountedItems = _receipt.ReceiptItems.Where(item => item.AppliedPromo != null).ToList();
        Assert.That(discountedItems.Count, Is.EqualTo(2));
        Assert.IsTrue(discountedItems.All(item => Math.Round(item.DiscountedTotal, 2) == 75m));
    }
    
    [Test]
    public void ApplyPromotions_ShouldNotApplyAnyPromotions_WhenNoneApplicable()
    {
        // Arrange
        _receipt.ReceiptItems.Add(new ReceiptItem{ProductSku = 'C', Total = 100m, DiscountedTotal = 100m, AppliedPromo = null});

        _mockPromotionRepository.Setup(repo => repo.GetAll()).Returns(Enumerable.Empty<Promotion>().AsQueryable());
        
        // Act 
        _promotionService.ApplyPromotions(_receipt);
        
        // Assert
        Assert.IsTrue(_receipt.ReceiptItems.Any(item => item.AppliedPromo == null));
    }

    [Test]
    public void ApplyPromotions_ShouldHandleMulltiplePromotions()
    {
        // Arrange
        _receipt.ReceiptItems.Add(new ReceiptItem{ProductSku = 'A', Total = 100m, DiscountedTotal = 100m, AppliedPromo = null});
        _receipt.ReceiptItems.Add(new ReceiptItem{ProductSku = 'B', Total = 200m, DiscountedTotal = 200m, AppliedPromo = null});

        var promotions = new List<Promotion>
        {
            new Promotion { ProductSkus = ['A'] },
            new Promotion { ProductSkus = ['B'] }
        };

        _mockPromotionRepository.Setup(repo => repo.GetAll()).Returns(promotions.AsQueryable());
        _mockPromotionFactory.Setup(factory => factory.CreatePromotions(It.IsAny<IEnumerable<Promotion>>()))
            .Returns(new List<IPromo>
            {
                new BulkBuyPromo(new List<char> { 'A' }, 1, 50m),
                new BulkBuyPromo(new List<char> { 'B' }, 1, 150m),
            });
        
        // Act 
        _promotionService.ApplyPromotions(_receipt);
        
        // Assert
        Assert.That(_receipt.ReceiptItems[0].DiscountedTotal, Is.EqualTo(50));
        Assert.That(_receipt.ReceiptItems[1].DiscountedTotal, Is.EqualTo(150));
    }
}
