using BeFaster.Domain.Models;

namespace BeFaster.Core.Interfaces;

public interface IInputProcessingService
{
    ProductInputModel ProcessInput(string input);
}