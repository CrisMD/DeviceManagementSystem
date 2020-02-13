using Model.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [DbTable("Utilizatori")]
    [DataContract]
    public class User
    {
        [DbColumn("id")]
        [DataMember]
        public int Id { get; set; }
        [DbColumn("username")]
        [DataMember]
        public string Username { get; set; }
        [DbColumn("parola")]
        [DataMember]
        public string Password { get; set; }
        [DbColumn("nume")]
        [DataMember]
        public string Name { get; set; }
        [DbColumn("rol")]
        [DataMember]
        public int Role { get; set; }
        [DbColumn("locatie")]
        [DataMember]
        public int Location { get; set; }

        public override bool Equals(object obj)
        {
            if (obj.GetType().Equals(typeof(Manufacturer)))
            {
                User other = (User)obj;
                return Username.Equals(other.Username);
            }

            return false;
        }

        public override string ToString()
        {
            return ($"{Username} {Name}");
        }
    }
}
