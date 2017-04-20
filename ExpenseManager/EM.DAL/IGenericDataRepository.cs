using EM.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EM.DAL
{
    public interface IGenericDataRepository<T> where T : class
    {
        IList<T> GetAll(params Expression<Func<T, object>>[] navigationProperties);
        PagedResult<T> GetPaggedResult(
            Expression<Func<T, bool>> filter = null,
            string orderBy = null,
            bool isAscending = true,
            int? skip = null,
            int? take = null, params Expression<Func<T, object>>[] navigationProperties);
        IList<T> GetList(Func<T, bool> where, params Expression<Func<T, object>>[] navigationProperties);
        T GetSingle(Func<T, bool> where, params Expression<Func<T, object>>[] navigationProperties);
        IList<T> Get(
            Expression<Func<T, bool>> filter = null,
            Expression<Func<T, object>> orderBy = null,
            int? skip = null,
            int? take = null, params Expression<Func<T, object>>[] navigationProperties);
        void Add(params T[] items);
        void Update(params T[] items);
        void Remove(params T[] items);
    }
}
