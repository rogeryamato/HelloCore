using HelloCore.DomainModel;
using HelloCore.DomainModel.Models;
using HelloCore.Interface;
using HelloCore.Interface.Manually;
using HelloCore.Interface.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HelloCore.Repository
{
    public abstract class BaseRepository<TEntity, Tkey> : IBaseRepository<TEntity, Tkey> where TEntity : class, new()
    {
        public BaseRepository()
        {
        }
        

        public DbSet<TEntity> Entities
        {
            get
            {
                return ContextUnitOfWork.Current.Value.DataSet<TEntity>();
            }
        }

        public void Add(TEntity data)
        {
            Entities.Add(data);
        }

        public void Delete(Tkey key)
        {
            var data = Entities.Find(key);
            if (data == null)
                throw new Exception("No such record!");
            Entities.Remove(data);
        }

        public IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> expression)
        {
            return Entities.AsQueryable().Where(expression).ToList();
        }

        public async Task<PageList<TEntity>> Get(IEnumerable<Expression<Func<TEntity, bool>>> expressions,PageCondition condition)
        {
            var query = Entities.AsQueryable();

            if (condition.Includes != null)
            {
                foreach (var include in condition.Includes)
                {
                    query = query.Include(include);
                }
            }

            foreach (var expression in expressions)
            {
                query = query.Where(expression);
            }
            
            var totalCount = await query.CountAsync();
            int pageCount = 0;
            Task<List<TEntity>> items = null;
            if (totalCount > 0)
            {
                if (condition.PageIndex == 0)
                {
                    condition.PageIndex = 1;
                    pageCount = totalCount / condition.PageSize;
                }
                items = query.Skip((condition.PageIndex - 1) * condition.PageSize).Take(condition.PageSize).ToListAsync();
            }
            
            return new PageList<TEntity>(condition.PageIndex, condition.PageSize, pageCount,await items);
        }

        public TEntity Get(Tkey key)
        {
            return Entities.Find(key);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return Entities.ToList();
        }

        public void Update(TEntity data)
        {
            ContextUnitOfWork.Current.Value.Entry(data).State = EntityState.Modified;
        }
    }

}
