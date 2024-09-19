namespace BeFaster.Domain.Models;

// we use this model to transfer data from customer faced layer to our service with business logic 
// in a future it will pass some additional data, like cashier id, customer discount card, etc etc
public class ProductModel
{
    public List<char> ProductSkuList { get; private set; }

    public ProductModel(List<char> productSkuList)
    {
        ProductSkuList = productSkuList;
    }
}