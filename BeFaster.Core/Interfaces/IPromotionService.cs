using BeFaster.Domain.Objects;

namespace BeFaster.Core.Interfaces;

public interface IPromotionService
{
    void ApplyPromotions(Receipt receipt);
}
