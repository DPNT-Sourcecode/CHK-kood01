using BeFaster.Core.Services;
using BeFaster.Core.Services.PromotionFactory;
using BeFaster.Domain.Models;
using BeFaster.Domain.Repositories;
using BeFaster.Runner.Exceptions;

namespace BeFaster.App.Solutions.CHK
{
    public static class CheckoutSolution
    {
        public static int ComputePrice(string? skus)
        {
            // setup repositories 
            var productRepository = new InMemoryProductRepository();
            var promotionRepository = new InMemoryPromotionRepository();

            // setup factory and service; 
            var promotionFactory = new PromotionFactory();
            var promotionService = new PromotionService(promotionRepository, promotionFactory);
            var chechoutService = new CheckoutService(productRepository, promotionService);
            var inputProcessingService = new InputProcessingService();

            var productInputModel =  new ProductInputModel(new List<char>());
            // process input with input service and get productInputModel
            try
            {
                productInputModel = inputProcessingService.ProcessInput(skus);
            }
            catch (ArgumentException ex)
            {
                return -1;
            }
            

            // calculate total price with promotions 
            var totalPrice = chechoutService.GetTotal(productInputModel);

            return totalPrice;
        }
    }
}
