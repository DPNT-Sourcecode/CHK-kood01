using BeFaster.Domain.Entities;

namespace BeFaster.Domain.Repositories;

public class InMemoryPromotionRepository : IPromotionRepository<Promotion>
{
    public IEnumerable<Promotion> GetAll()
    {
        throw new NotImplementedException();
    }

    public void Add(Promotion entity)
    {
        throw new NotImplementedException();
    }
}