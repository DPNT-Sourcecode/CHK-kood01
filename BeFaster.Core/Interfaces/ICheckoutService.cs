using BeFaster.Domain.Models;

namespace BeFaster.Core.Interfaces;

public interface ICheckoutService
{
    int GetTotal(ProductInputModel productInputModel);
}