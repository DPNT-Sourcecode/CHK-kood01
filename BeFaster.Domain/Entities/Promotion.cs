using BeFaster.Domain.Enums;

namespace BeFaster.Domain.Entities;

// in real life implementation we will separate Entity from Objects, but for test task purpose we just keep it simple
public class Promotion
{
    public List<char> ProductSkus { get; set; }
    public int RequiredQuantity { get; set; }
    public decimal PromoPrice { get; set; }
    public PromotionType Type { get; set; }
    public char? FreeProductSku { get; set; } // nullable, only for buy X get Y promotions
    
    // we also probably want to add some additional fields to extend business logic for promotions
    
    // public enum Type { get; set; } // this field need to define type of promotion 
    // public DateTime PromotionStartFrom { get; set; }
    // public DateTime PromotionEndAt { get; set; }
    // maybe some more fields for regular events like Every friday or something
    // this is just a brief
    
    // In real life implimentation we probably want to create multiple Promotion[type] Tables for each type of Promotion
    // Bulk promotion, Multiple items promotion (like buy A+B+C get discount), Percentage Promotion, etc etc. 
}
