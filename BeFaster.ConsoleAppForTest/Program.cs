// See https://aka.ms/new-console-template for more information

using BeFaster.Core.Services;
using BeFaster.Core.Services.PromotionFactory;
using BeFaster.Domain.Repositories;


// setup repositories 
var productRepository = new InMemoryProductRepository();
var promotionRepository = new InMemoryPromotionRepository();

// setup factory and service; 

var promotionFactory = new PromotionFactory();
var promotionService = new PromotionService(promotionRepository, promotionFactory);
var chechoutService = new CheckoutService(productRepository,promotionService);
var inputProcessingService = new InputProcessingService();

// input
var input = "AAAAAAAAAAFFFEEEEBBBB"; 
    
    // | Item | Price | Special offers         |
    // +------+-------+------------------------+
    // | A    | 50    | 3A for 130, 5A for 200 |
    // | B    | 30    | 2B for 45              |
    // | C    | 20    |                        |
    // | D    | 15    |                        |
    // | E    | 40    | 2E get one B free      |
    
// process input with input service and get productInputModel
var productInputModel = inputProcessingService.ProcessInput(input);

// calculate total price with promotions 
var totalPrice = chechoutService.GetTotal(productInputModel);

// display result 
if (totalPrice != -1)
{
    Console.WriteLine($"The total price is: {totalPrice}");
}
else
{
    Console.WriteLine("Invalid product codes or product not found");
}
