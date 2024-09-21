// using BeFaster.Domain.Entities;
//
// namespace BeFaster.Domain.Objects;
//
// public class BasketItem
// {
//     public Product Product { get; private set; }
//     public int Quantity { get; private set; }
//     public int PlainAmount => Product.Price * Quantity;
//
//     public BasketItem(Product product, int qty = 1)
//     {
//         Product = product;
//         Quantity = qty;
//     }
//
//     public void AddItem()
//     {
//         Quantity += 1;
//     }
// }