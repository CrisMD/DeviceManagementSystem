using Repository.DbHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public abstract class AbstractRepository<T> : IRepository<T>
    {
        public List<T> GetAll()
        {
            return Queries.GetAll<T>();
        }

        public T GetOne(int id)
        {
            return Queries.GetOne<T>(id);
        }

        public bool Insert(T t)
        {
            try
            {
                Queries.Insert<T>(t);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Update(int id, T tNew)
        {
            try
            {
                Queries.Update<T>(id, tNew);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                Queries.Delete<T>(id);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }

}
