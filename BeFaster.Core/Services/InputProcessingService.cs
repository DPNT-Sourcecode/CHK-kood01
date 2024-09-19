using BeFaster.Core.Interfaces;
using BeFaster.Domain.Models;

namespace BeFaster.Core.Services;

public class InputProcessingService : IInputProcessingService
{
    public ProductInputModel ProcessInput(string? input)
    {
        if (string.IsNullOrEmpty(input))
        {
            return new ProductInputModel(new List<char>());
            //throw new ArgumentException("Input cannot be null or empty");
        }

        if (input.Any(c => !char.IsLetter(c)))
        {
            return new ProductInputModel(new List<char>());
            //throw new ArgumentException("Input contains invalid characters. Only letters are allowed");
        }

        var skus = new List<char>();
        foreach (var sku in input)
        {
            skus.Add(sku);
        }

        return new ProductInputModel(skus);
    }
}
