using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Castle.DynamicProxy.Generators.Emitters.SimpleAST;

namespace ChapmanUniversity1._0.Repositories
{
    public interface IRepository<T> where T : class
    {
        T GetById(int id);
        Task<IEnumerable<T>> GetAll();
    }
}
