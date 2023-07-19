using AM.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.Core.Service
{
    public interface IService<T> where T : class
    {
        void Add(T t);
        void Remove(int id);
        T Get(int id);
        IList<T> GetAll();
        void update(T t);
    }
}
