using AM.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.Core.Service
{
    public class Service<T> : IService<T>where T:class
    {
        IRepository<T> repository;
        readonly IUnitOfWork _unitOfWork;
        public Service(IUnitOfWork _unitOfWork)
        {
            this._unitOfWork = _unitOfWork;
            repository = _unitOfWork.GetRepository<T>();
        }
        public void Add(T t)
        {
            repository.Add(t);
            _unitOfWork.Save();
        }

        public IList<T> GetAll()
        {
            return repository.GetAll();
            
        }
        public T Get(int id)
        {
            return repository.Get(id);
        }

        public void Remove(int id)
        {
            repository.Delete(id);
            _unitOfWork.Save();
        }
        public void update(T t)
        {
            repository.Update(t);
            _unitOfWork.Save(); 
        }
    }
}
