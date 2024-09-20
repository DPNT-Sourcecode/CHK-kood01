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
    //ABCDEFABCDEF
    //AA BB CC DD EE FF
    
    // AA = 100 - 100
    // BB = 45 - 30
    // CC = 40 - 40
    // DD = 30 - 30
    // EE = 80 - 40 
    // FF = 20 - 20 
// +------+-------+------------------------+
//     | Item | Price | Special offers         |
//     +------+-------+------------------------+
//     | A    | 50    | 3A for 130, 5A for 200 |
//     | B    | 30    | 2B for 45              |
//     | C    | 20    |                        |
//     | D    | 15    |                        |
//     | E    | 40    | 2E get one B free      |
//     | F    | 10    | 2F get one F free      |
    //
    // Some requests have failed (2/46). Here are some of them:
    // - {"method":"checkout","params":["ABCDEFABCDEF"],"id":"CHK_R3_044"}, expected: 300, got: 285
    //     - {"method":"checkout","params":["CDFFAECBDEAB"],"id":"CHK_R3_045"}, expected: 300, got: 285

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



