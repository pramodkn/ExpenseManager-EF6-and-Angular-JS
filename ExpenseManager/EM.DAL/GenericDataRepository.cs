using EM.DomainModel;
using PagedList;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Dynamic;
namespace EM.DAL
{
    public class GenericDataRepository<T> : IGenericDataRepository<T> where T : class
    {

        public virtual void Add(params T[] items)
        {
            using (var context = new EMEntities())
            {
                foreach (T item in items)
                {
                    context.Entry(item).State = EntityState.Added;
                }
                context.SaveChanges();
            }
        }

        public virtual IList<T> GetAll(params Expression<Func<T, object>>[] navigationProperties)
        {
            List<T> list;
            using (var context = new EMEntities())
            {
                IQueryable<T> dbQuery = context.Set<T>();

                //Apply eager loading
                foreach (Expression<Func<T, object>> navigationProperty in navigationProperties)
                    dbQuery = dbQuery.Include<T, object>(navigationProperty);

                list = dbQuery
                    .AsNoTracking()
                    .ToList<T>();
            }
            return list;
        }

        public virtual IList<T> GetList(Func<T, bool> where,
             params Expression<Func<T, object>>[] navigationProperties)
        {
            List<T> list;
            using (var context = new EMEntities())
            {
                IQueryable<T> dbQuery = context.Set<T>();

                //Apply eager loading
           
                foreach (Expression<Func<T, object>> navigationProperty in navigationProperties)
                    dbQuery = dbQuery.Include<T, object>(navigationProperty);

                list = dbQuery
                    .AsNoTracking()
                    .Where(where)
                    .ToList<T>();
            }
            return list;
        }

        public virtual T GetSingle(Func<T, bool> where,
             params Expression<Func<T, object>>[] navigationProperties)
        {
            T item = null;
            using (var context = new EMEntities())
            {
                IQueryable<T> dbQuery = context.Set<T>();

                //Apply eager loading
                foreach (Expression<Func<T, object>> navigationProperty in navigationProperties)
                    dbQuery = dbQuery.Include<T, object>(navigationProperty);

                item = dbQuery
                    .AsNoTracking() //Don't track any changes for the selected item
                    .FirstOrDefault(where); //Apply where clause
            }
            return item;
        }
       
        public virtual void Update(params T[] items)
        {
            using (var context = new EMEntities())
            {
                foreach (T item in items)
                {
                    context.Entry(item).State = EntityState.Modified;
                }
                context.SaveChanges();
            }
        }

        public virtual void Remove(params T[] items)
        {
            using (var context = new EMEntities())
            {
                foreach (T item in items)
                {
                    context.Entry(item).State = EntityState.Deleted;
                }
                context.SaveChanges();
            }
        }
        

        public IList<T> Get(Expression<Func<T, bool>> filter = null,
            Expression<Func<T, object>> orderBy = null,
            int? skip = default(int?),
            int? take = default(int?),
            params Expression<Func<T, object>>[] navigationProperties)
        {
            using (var context = new EMEntities())
            {

                IQueryable<T> query = context.Set<T>();
                if (navigationProperties != null)
                    foreach (Expression<Func<T, object>> navigationProperty in navigationProperties)
                    query = query.Include<T, object>(navigationProperty);

                if (filter != null)
                {
                    query = query.Where(filter);
                }
                if (orderBy != null)
                {
                    if (true)
                        query = query.OrderByDescending(orderBy);
                 
                }

                if (skip.HasValue)
                {
                    query = query.Skip(skip.Value);
                }

                if (take.HasValue)
                {
                    query = query.Take(take.Value);
                }

                return query.ToList();
            }
        }
        public PagedResult<T> GetPaggedResult(Expression<Func<T, bool>> filter = null,
           string orderBy = null,
           bool isAscending=true,
           int? skip = default(int?),
           int? take = default(int?),
           params Expression<Func<T, object>>[] navigationProperties)
        {
            using (var context = new EMEntities())
            {
                var result = new PagedResult<T>();
                IQueryable<T> query = context.Set<T>();
                if (navigationProperties != null)
                    foreach (Expression<Func<T, object>> navigationProperty in navigationProperties)
                        query = query.Include<T, object>(navigationProperty);

                if (filter != null)
                {
                    query = query.Where(filter);
                }
                result.RowCount = query.Count();
                if (orderBy != null)
                {
                    query = query.OrderBy(orderBy + (isAscending ? "" : " descending"));
                }

                if (skip.HasValue)
                {
                    query = query.Skip(skip.Value);
                }

                if (take.HasValue)
                {
                    query = query.Take(take.Value);
                }
               
                result.CurrentPage = skip;
                result.PageSize = take;
                var pageCount = (double)result.RowCount / result.PageSize;
                result.PageCount = (int)Math.Ceiling((decimal)pageCount);
                result.Results = query.ToList();
              

                return result;
            }
        }
    }


}
