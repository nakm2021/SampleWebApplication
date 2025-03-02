namespace SampleWebApplication_DataAccess.Repository.IRepository
{
    /// <summary>
    /// 複数リポジトリをまとめるためのインターフェース
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        #region 操作対象リポジトリ
        OrderRepository Order { get; }
        OrderItemRepository OrderItem { get; }
        #endregion

        /// <summary>
        /// 保存処理
        /// </summary>
        /// <returns>Microsoft.EntityFrameworkCore.DbContext.SaveChanges()と同じ</returns>
        int Save();
    }
}
