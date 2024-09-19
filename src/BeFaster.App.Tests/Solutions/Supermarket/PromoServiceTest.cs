using BeFaster.Core.Interfaces;
using BeFaster.Core.Services;
using BeFaster.Domain.Entities;
using BeFaster.Domain.Repositories;
using Moq;

namespace BeFaster.App.Tests.Solutions.Supermarket;

public class PromoServiceTest
{
    private Mock<IPromotionRepository<Promotion>> _mockPromotionRepository;
    private Mock<IPromotionFactory> _mockPromotionFactory;
    private PromotionService _promotionService;

    [SetUp]
    public void SetUp()
    {
        _mockPromotionRepository = new Mock<IPromotionRepository<Promotion>>();
        _mockPromotionFactory = new Mock<IPromotionFactory>();
        _promotionService = new PromotionService(_mockPromotionRepository.Object, _mockPromotionFactory.Object);
    }
    
    []
}