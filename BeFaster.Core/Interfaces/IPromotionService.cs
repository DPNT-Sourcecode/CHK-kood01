using BeFaster.Domain.Objects;

namespace BeFaster.Core.Interfaces;

public interface IPromotionService
{
    void ApplyPromotions(IList<ReceiptItem> receiptItems);
}