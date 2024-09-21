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
    private Receipt _receipt;
    
    [SetUp]
    public void Setup()
    {
        _receipt = new Receipt();
        _mockProductRepository = new Mock<IProductRepository<Product>>();
        _mockPromotionService = new Mock<IPromotionService>();
        _checkoutService = new CheckoutService(_mockProductRepository.Object, _mockPromotionService.Object);
    }

    [Test]
    public void GetTotal_ShouldReturnZero_WhenNoProductSkusProvided()
    {
        // Arrange
        var productInputModel = new ProductInputModel(new List<char>());
        
        // Act 
        var result = _checkoutService.GetTotal(productInputModel);

        // Assert
        Assert.That(result, Is.EqualTo(0));
    }
    
    [Test]
    public void GetTotal_ShouldReturnMinusOne_WhenProductNotFound()
    {
        // Arrange
        var productInputModel = new ProductInputModel(new List<char> { 'A' });
        _mockProductRepository.Setup(repo => repo.GetByProductSku('A')).Returns((Product)null);
        
        // Act 
        var result = _checkoutService.GetTotal(productInputModel);

        // Assert
        Assert.That(result, Is.EqualTo(-1));
    }

    [Test]
    public void GetTotal_ShouldCalculateTotal_WhenValidProductProvided()
    {
        // Arrange
        var productInputModel = new ProductInputModel(new List<char> { 'A', 'B' });
        var productA = new Product { ProductSku = 'A', Price = 50 };
        var productB = new Product { ProductSku = 'B', Price = 30 };
        
        _mockProductRepository.Setup(repo => repo.GetByProductSku('A')).Returns(productA);
        _mockProductRepository.Setup(repo => repo.GetByProductSku('B')).Returns(productB);
        
        _receipt.AddItem(new ReceiptItem(){ProductSku = 'A',Total = productA.Price, DiscountedTotal = productA.Price});
        _receipt.AddItem(new ReceiptItem(){ProductSku = 'B',Total = productB.Price, DiscountedTotal = productB.Price});
        
        _mockPromotionService.Setup(service => service.ApplyPromotions(_receipt));
        
        var total = productA.Price + productB.Price;
        
        // Act
        var result = _checkoutService.GetTotal(productInputModel); 
        
        // Assert 
        Assert.That(result, Is.EqualTo(total));
    }
}

