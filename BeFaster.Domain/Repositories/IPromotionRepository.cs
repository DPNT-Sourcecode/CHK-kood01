namespace BeFaster.Domain.Repositories;

public interface IPromotionRepository<T> where T: class
{
    IEnumerable<T> GetAll();
    void Add(T entity);
}