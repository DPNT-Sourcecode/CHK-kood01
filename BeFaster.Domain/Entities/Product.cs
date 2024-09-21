namespace BeFaster.Domain.Entities;

// in real life implementation we will separate Entity from Objects, but for test task purpose we just keep it simple 
public class Product
{
    public required char ProductSku { get; set; }
    public decimal Price { get; set; }
}