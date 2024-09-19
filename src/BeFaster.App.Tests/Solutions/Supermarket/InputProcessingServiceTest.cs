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
        var input = "abc";
        
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
        
        // Act
        
        // Assert
    }
}