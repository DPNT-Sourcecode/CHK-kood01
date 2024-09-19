namespace BeFaster.Domain.Entities;

// in real life implementation we will separate Entity from Objects, but for test task purpose we just keep it simple
public class Promotion
{
    public int Id { get; set; }
    public char ProductSku { get; set; }
    
}