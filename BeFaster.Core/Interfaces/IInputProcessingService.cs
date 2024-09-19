using BeFaster.Domain.Models;

namespace BeFaster.Core.Interfaces;

public interface IInputService
{
    ProductInputModel ProcessInput(string input);
}