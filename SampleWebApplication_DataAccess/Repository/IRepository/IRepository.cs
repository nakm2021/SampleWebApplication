using System.Linq.Expressions;

namespace SampleWebApplication_DataAccess.Repository.IRepository
{
    /// <summary>
    /// データの操作を抽象化
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepository<T> where T : class
    {
        /// <summary>
        /// 全てのデータを取得
        /// </summary>
        /// <returns></returns>
        IEnumerable<T> GetAll();

        /// <summary>
        /// 条件に合致するデータを取得
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        T? Get(Expression<Func<T, bool>> filter);

        /// <summary>
        /// データを追加
        /// </summary>
        /// <param name="entity">エンティティクラス</param>
        void Add(T entity);

        /// <summary>
        /// データを更新
        /// </summary>
        /// <param name="entity">エンティティクラス</param>
        void Update(T entity);

        /// <summary>
        /// データを削除
        /// </summary>
        /// <param name="entity">エンティティクラス</param>
        void Remove(T entity);
    }
}
