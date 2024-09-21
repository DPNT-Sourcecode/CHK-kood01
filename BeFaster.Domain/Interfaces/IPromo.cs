using BeFaster.Domain.Enums;
using BeFaster.Domain.Objects;

namespace BeFaster.Core.Interfaces;

public interface IPromo
{
    char ProductSku { get; }
    public PromotionType Type { get; }
    int GetDiscount(Receipt receipt, char receiptId);
}