namespace EM.BOL.Infrastructure
{
    using System.Threading.Tasks;
    public interface IRepository : IReadOnlyRepository
    {
        TEntity Create<TEntity>(TEntity entity, string createdBy = null)
            where TEntity : class;

        void Update<TEntity>(TEntity entity, string modifiedBy = null)
            where TEntity : class;

        TEntity Delete<TEntity>(object id)
            where TEntity : class;

        TEntity Delete<TEntity>(TEntity entity)
            where TEntity : class;

        void Save();

        Task SaveAsync();
    }
}
