using BeFaster.Core.Interfaces;
using BeFaster.Core.Services;
using BeFaster.Domain.Entities;
using BeFaster.Domain.Models;
using BeFaster.Domain.Objects;
using BeFaster.Domain.Repositories;
using Moq;

namespace BeFaster.App.Tests.Solutions.Supermarket;

public class CheckoutServiceTest
{
    private Mock<IProductRepository<Product>> _mockProductRepository;
    private Mock<IPromotionService> _mockPromotionService;
    private CheckoutService _checkoutService;
    
    [SetUp]
    public void Setup()
    {
        _mockProductRepository = new Mock<IProductRepository<Product>>();
        _mockPromotionService = new Mock<IPromotionService>();
        _checkoutService = new CheckoutService(_mockProductRepository.Object, _mockPromotionService.Object);
    }

    [Test]
    public void GetTotal_ValidProductSku_ReturnCorrectTotal()
    {
        // Arrange
        var productA = new Product { ProductSku = 'A', Price = 50 };
        var productB = new Product { ProductSku = 'B', Price = 30 };
        
        _mockProductRepository.Setup(repo => repo.GetByProductSku('A')).Returns(productA);
        _mockProductRepository.Setup(repo => repo.GetByProductSku('B')).Returns(productB);

        var basket = new Basket();
        basket.AddItem(productA);
        basket.AddItem(productB);
        var receipt = new Receipt();
        receipt.AddItem(new ReceiptItem(new BasketItem(productA)));
        receipt.AddItem(new ReceiptItem(new BasketItem(productB)));

        _mockPromotionService.Setup(service => service.ApplyPromotions(receipt));

        var productInputModel = new ProductInputModel(new List<char> { 'A', 'B' });
        
        // Act
        var total = _checkoutService.GetTotal(productInputModel);
    
        // Assert
        Assert.That(total, Is.EqualTo(80));
    }
    [Test]
    public void GetTotal_WithPromotions_AppliesPromotionsCorrectly()
    {
        // Arrange
        var productA = new Product { ProductSku = 'A', Price = 50 };
        var productB = new Product { ProductSku = 'B', Price = 30 };
        
        _mockProductRepository.Setup(repo => repo.GetByProductSku('A')).Returns(productA);
        _mockProductRepository.Setup(repo => repo.GetByProductSku('B')).Returns(productB);

        var basket = new Basket();
        basket.AddItem(productA);
        basket.AddItem(productB);
        var receipt = new Receipt();
        receipt.AddItem(new ReceiptItem(new BasketItem(productA)));
        receipt.AddItem(new ReceiptItem(new BasketItem(productB)));

        //apply a promotion tat reduce the price by 10
        _mockPromotionService.Setup(service => service.ApplyPromotions(It.IsAny<Receipt>()))
            .Callback((Receipt receipt) =>
            {
                // Assume promotion applies to product A (product SKU 'A')
                var receiptItem = receipt.GetItemByKey('A');
        
                if (receiptItem != null)
                {
                    // Apply a promotion that reduces the price by 10
                    receiptItem.ApplyPromotions(40); // Assuming the new price after promotion is 40
                }
            });

        var productInputModel = new ProductInputModel(new List<char> { 'A', 'B' });
        
        // Act
        var total = _checkoutService.GetTotal(productInputModel);
        
        // Assert
        Assert.That(total, Is.EqualTo(70));
    }
    [Test]
    public void GetTotal_InvalidProductSku_ReturnsMinusOne()
    {
        // Arrange
        _mockProductRepository.Setup(repo => repo.GetByProductSku(It.IsAny<char>())).Returns((Product)null);
        
        var productInputModel = new ProductInputModel(new List<char> { 'X' });
        
        // Act
        var total = _checkoutService.GetTotal(productInputModel);
        
        // Assert
        Assert.That(total, Is.EqualTo(-1));
    }
    
    [Test]
    public void GetTotal_IEmptyProductSku_ReturnsZero()
    {
        // Arrange
        var productInputModel = new ProductInputModel(new List<char> ());
        
        // Act
        var total = _checkoutService.GetTotal(productInputModel);
        
        // Assert
        Assert.That(total, Is.EqualTo(0));
    }
}