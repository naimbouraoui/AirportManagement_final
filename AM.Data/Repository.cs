using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AM.Core.Interfaces;

namespace AM.Data
{
    public class Repository<T> : IRepository<T> where T : class
    {
        //proble de dependance
        readonly AMContext ctxt;
        public Repository(AMContext ctxt)
        {
            this.ctxt = ctxt;
        }

        public void Add(T t)
        {
            ctxt.Add(t);
        }

        //public void commit()
        //{
        //    ctxt.SaveChanges();
        //}

        public void Delete(int id)
        {
            ctxt.Remove(id);
        }

        public T Get(int id)
        {
            return (T)ctxt.Find(typeof(T),id);
        }

        public T Get(string id)
        {
            return (T)ctxt.Find(typeof(T), id);
        }

        public IList<T> GetAll()
        {
            return ctxt.Set<T>().ToList();
        }

        public void Update(T t)
        {
            ctxt.Update(t);
        }

    }
}
