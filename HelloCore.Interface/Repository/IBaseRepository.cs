using HelloCore.DomainModel;
using HelloCore.DomainModel.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HelloCore.Interface.Repository
{
    public interface IBaseRepository<T, TKey>
    {
        void Add(T data);
        void Delete(TKey key);
        void Update(T data);
        IEnumerable<T> Get(Expression<Func<T, bool>> expression);
        Task<PageList<T>> Get(IEnumerable<Expression<Func<T, bool>>> expressions,PageCondition condition);
        T Get(TKey key);
        IEnumerable<T> GetAll();
    }
}
