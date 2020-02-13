using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface IRepository<T>
    {
        List<T> GetAll();
        T GetOne(int id);
        bool Insert(T t);
        bool Update(int id, T tNew);
        bool Delete(int id);
    }
}
