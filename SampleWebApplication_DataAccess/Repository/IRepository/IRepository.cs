using System.Linq.Expressions;

namespace SampleWebApplication_DataAccess.Repository.IRepository
{
    /// <summary>
    /// データの操作を抽象化
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T? Get(Expression<Func<T, bool>> filter);
        void Add(T entity);
        void Update(T entity);
        void Remove(T entity);
    }
}
