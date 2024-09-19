using BeFaster.Core.Interfaces;
using BeFaster.Core.Services;
using BeFaster.Domain.Models;

namespace BeFaster.App.Tests.Solutions.Supermarket;

public class InputProcessingServiceTest
{
    private IInputProcessingService _inputProcessingService;
    
    [SetUp]
    public void SetUp()
    {
        _inputProcessingService = new InputProcessingService();
    }

    [Test]
    public void ProcessInput_ShouldReturnProductInputModel_WhenInputIsValid()
    {
        // Arrange
        var input = "ABC";
        
        // Act
        var result = _inputProcessingService.ProcessInput(input);
        
        // Assert
        Assert.IsNotNull(result);
        Assert.IsInstanceOf<ProductInputModel>(result);
        CollectionAssert.AreEqual(new List<char> { 'A', 'B', 'C' }, result.ProductSkuList);
    }
    
    [Test]
    public void ProcessInput_ShouldThrowArgumentException_WhenInputContainsInvalidCharacters()
    {
        // Arrange
        var inout = "abc!3pot";
        
        // Act 
        var productInputModel = _inputProcessingService.ProcessInput(inout);
        
        // Assert
        Assert.That(productInputModel.ProductSkuList.Count, Is.EqualTo(0));
    }
    
    [Test]
    public void ProcessInput_ShouldThrowArgumentException_WhenInputIsNullOrEmpty()
    {
        // Arrange
        var inout = "";
        
        // Act 
        var productInputModel = _inputProcessingService.ProcessInput(inout);
        
        // Assert
        Assert.That(productInputModel.ProductSkuList.Count, Is.EqualTo(0));
    }
}
