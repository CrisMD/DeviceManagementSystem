using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Data;
using Model.Attributes;

namespace Repository.DbHelper
{
    public class Queries
    {
        private static List<PropertyInfo> GetProperties<T>()
        {
            List<PropertyInfo> rez = typeof(T).GetProperties().ToList();
            return rez;
        }

        private static PropertyInfo GetIdProperty<T>()
        {
            List<PropertyInfo> rez = typeof(T).GetProperties().ToList();
            return rez[0];
        }

        private static List<DbColumnAttribute> GetAttributes<T>()
        {
            var properties = GetProperties<T>();
            List<DbColumnAttribute> rez = new List<DbColumnAttribute>();
            foreach (var prop in properties)
            {
                var attributes = prop.GetCustomAttributes();
                rez.Add(attributes.ElementAt(0) as DbColumnAttribute);
            }
            return rez;
        }

        private static DbColumnAttribute GetIdAttribute<T>()
        {
            var prop = GetIdProperty<T>();
            var attributes = prop.GetCustomAttributes();
            return attributes.ElementAt(0) as DbColumnAttribute;
        }

        private static DbTableAttribute GetTableAttribute<T>()
        {
            DbTableAttribute dbTableAttribute = typeof(T).GetCustomAttribute(typeof(DbTableAttribute)) as DbTableAttribute;
            return dbTableAttribute;
        }

        private static string BuildPropertiesString<T>()
        {
            var props = GetProperties<T>();
            props.RemoveAt(0);
            string properties = String.Empty;
            foreach (var property in props)
            {
                properties = properties + ":" + property.Name;
                if (property != props.ElementAt(props.Count - 1))
                {
                    properties = properties + ", ";
                }
            }
            return properties;
        }

        private static string BuildAttributesString<T>()
        {
            var attrs = GetAttributes<T>();
            attrs.RemoveAt(0);
            string attributes = string.Empty;
            foreach (var attribute in attrs)
            {
                attributes = attributes + attribute.Name;
                if (attribute != attrs.ElementAt(attrs.Count - 1))
                {
                    attributes = attributes + ", ";
                }
            }
            return attributes;
        }

        public static List<T> GetAll<T>()
        {
            var list = new List<T>();
            var conn = DbUtils.GetConnection();
            using (var comm = conn.CreateCommand())
            {
                comm.CommandText = string.Format("SELECT * FROM {0}", GetTableAttribute<T>().Name);
                using (var result = comm.ExecuteReader())
                {
                    while (result.Read())
                    {
                        T entity = (T)Activator.CreateInstance(typeof(T));
                        var properties = GetProperties<T>();
                        var attributes = GetAttributes<T>();
                        for (int j = 0; j < properties.Count; j++)
                        {
                            if (result.GetValue(j) == DBNull.Value)
                            {
                                properties[j].SetValue(entity, null, null);
                            }
                            else
                            {
                                if (properties[j].PropertyType == typeof(int))
                                {
                                    properties[j].SetValue(entity, Convert.ToInt32(result.GetValue(j)), null);
                                }
                                else if (properties[j].PropertyType == typeof(double))
                                {
                                    properties[j].SetValue(entity, Convert.ToDouble(result.GetValue(j)), null);
                                }
                                else if (properties[j].PropertyType == typeof(DateTime))
                                {
                                    properties[j].SetValue(entity, Convert.ToDateTime(result.GetValue(j)), null);
                                }
                                else
                                {
                                    properties[j].SetValue(entity, result.GetValue(j), null);
                                }
                            }
                        }

                        list.Add(entity);
                    }
                }
            }
            return list;
        }

        public static T GetOne<T>(int id)
        {
            var conn = DbUtils.GetConnection();
            using (var comm = conn.CreateCommand())
            {
                comm.CommandText = string.Format("SELECT * FROM {0} WHERE {1} = :{2}", GetTableAttribute<T>().Name, GetIdAttribute<T>().Name, GetIdProperty<T>().Name);
                var param = comm.CreateParameter();
                param.ParameterName = string.Format(":{0}", GetIdProperty<T>().Name);
                param.Value = id;
                comm.Parameters.Add(param);
                using (var result = comm.ExecuteReader())
                {
                    if (result.Read())
                    {
                        T entity = (T)Activator.CreateInstance(typeof(T));
                        var properties = GetProperties<T>();
                        var attributes = GetAttributes<T>();
                        for (int j = 0; j < properties.Count; j++)
                        {
                            if (result.GetValue(j) == DBNull.Value)
                            {
                                properties[j].SetValue(entity, null, null);
                            }
                            else
                            {
                                if (properties[j].PropertyType == typeof(int))
                                {
                                    properties[j].SetValue(entity, Convert.ToInt32(result.GetValue(j)), null);
                                }
                                else if (properties[j].PropertyType == typeof(double))
                                {
                                    properties[j].SetValue(entity, Convert.ToDouble(result.GetValue(j)), null);
                                }
                                else
                                {
                                    properties[j].SetValue(entity, result.GetValue(j), null);
                                }
                            }
                        }

                        return entity;
                    }

                    return default(T);
                }
            }
        }

        public static void Insert<T>(T entity)
        {
            var conn = DbUtils.GetConnection();
            using (var comm = conn.CreateCommand())
            {
                if (string.IsNullOrEmpty(BuildPropertiesString<T>()))
                {
                    comm.CommandText = string.Format("INSERT INTO {0} DEFAULT VALUES", GetTableAttribute<T>().Name);
                }
                else
                {
                    comm.CommandText = string.Format("INSERT INTO {0}({1}) VALUES ({2})", GetTableAttribute<T>().Name, BuildAttributesString<T>(), BuildPropertiesString<T>());
                }
                for (int i = 1; i < GetProperties<T>().Count; i++)
                {
                    var param = comm.CreateParameter();
                    param.ParameterName = ":" + GetProperties<T>()[i].Name;
                    param.Value = GetProperties<T>()[i].GetValue(entity);
                    comm.Parameters.Add(param);
                }

                var result = comm.ExecuteNonQuery();
                if (result == 0)
                {
                    throw new Exception("Modificare esuata!");
                }
            }
        }

        public static void Update<T>(int id, T entity)
        {
            var conn = DbUtils.GetConnection();
            string sequence = string.Empty;
            for (int i = 1; i < GetProperties<T>().Count; i++)
            {
                sequence = sequence + GetAttributes<T>()[i].Name + "= :" + GetProperties<T>()[i].Name;
                if (i != GetProperties<T>().Count - 1)
                {
                    sequence = sequence + ", ";
                }
            }
            using (var comm = conn.CreateCommand())
            {
                comm.CommandText = string.Format("UPDATE {0} SET {1} WHERE {2} = :{3}", GetTableAttribute<T>().Name, sequence, GetIdAttribute<T>().Name, GetIdProperty<T>().Name);
                for (int i = 1; i < GetProperties<T>().Count; i++)
                {
                    var param = comm.CreateParameter();
                    param.ParameterName = ":" + GetProperties<T>()[i].Name;
                    param.Value = GetProperties<T>()[i].GetValue(entity);
                    comm.Parameters.Add(param);
                }

                var paramId = comm.CreateParameter();
                paramId.ParameterName = GetIdProperty<T>().Name;
                paramId.Value = id;
                comm.Parameters.Add(paramId);

                var result = comm.ExecuteNonQuery();
                if (result == 0)
                {
                    throw new Exception("Modificare esuata!");
                }
            }
        }

        public static void Delete<T>(int id)
        {
            var conn = DbUtils.GetConnection();
            using (var comm = conn.CreateCommand())
            {
                comm.CommandText = string.Format("DELETE FROM {0} WHERE {1} = :{2}", GetTableAttribute<T>().Name, GetIdAttribute<T>().Name, GetIdProperty<T>().Name);

                var paramId = comm.CreateParameter();
                paramId.ParameterName = GetIdProperty<T>().Name;
                paramId.Value = id;
                comm.Parameters.Add(paramId);
                var result = comm.ExecuteNonQuery();

                if (result == 0)
                {
                    throw new Exception("Stergere esuata!");
                }
            }
        }
    }
}
