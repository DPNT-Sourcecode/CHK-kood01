using BeFaster.Domain.Enums;
using BeFaster.Domain.Objects;

namespace BeFaster.Core.Interfaces;

public interface IPromo
{
    List<char> ProductSkus { get; }
    int RequiredQuantity { get; set; }
    public PromotionType Type { get; }
    void ApplyDiscount(Receipt receipt);
}