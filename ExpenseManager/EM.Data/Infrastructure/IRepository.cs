namespace EM.Data.Infrastructure
{
    using System.Threading.Tasks;
    public interface IRepository : IReadOnlyRepository
    {
        TEntity Create<TEntity>(TEntity entity, string createdBy = null)
            where TEntity : class, IBaseEntity;

        void Update<TEntity>(TEntity entity, string modifiedBy = null)
            where TEntity : class, IBaseEntity;

        TEntity Delete<TEntity>(object id)
            where TEntity : class, IBaseEntity;

        TEntity Delete<TEntity>(TEntity entity)
            where TEntity : class, IBaseEntity;

        void Save();

        Task SaveAsync();
    }
}
