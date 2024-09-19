using BeFaster.Domain.Objects;

namespace BeFaster.Core.Interfaces;

public interface IPromo
{
    char ProductSku { get; }
    int GetDiscount(ReceiptItem item);
}