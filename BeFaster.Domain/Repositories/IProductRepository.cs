namespace BeFaster.Domain.Repositories;

public interface IProductRepository<T> where T: class
{
    T? GetByProductSku(char productSku);
    void Add(T entity);
}