using MySqlConnector;
using System.Data;

namespace DL
{
    public class DLBase : IDLBase
    {
        protected string connectionString = "SERVER=localhost;DATABASE=world;UID=root;PASSWORD=12345678@Abc";
        protected MySqlConnection con = new MySqlConnection();


        public DLBase()
        {
            con.ConnectionString = connectionString;
        }

        protected List<T> GetList<T>(string query)
        {
            con.Open();
            MySqlCommand cmd = new MySqlCommand(query, con);
            MySqlDataReader reader = cmd.ExecuteReader();
            List<T> list = new List<T>();
            while(reader.Read())
            {
                var type = typeof(T);
                T obj = (T)Activator.CreateInstance(type);
                foreach(var prop in type.GetProperties())
                {
                    var propType = prop.PropertyType;
                    if (propType.IsGenericType && propType.GetGenericTypeDefinition() == typeof(Nullable<>))
                    {
                        propType = propType.GetGenericArguments()[0];
                    }
                    prop.SetValue(obj, Convert.ChangeType(reader[prop.Name].ToString(), propType));
                }
                list.Add(obj);
            }
            con.Close();
            return list;
        }
    }
}
