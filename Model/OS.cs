using Model.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [DbTable("SistemeOperare")]
    public class OS
    {
        [DbColumn("id")]
        public int Id { get; set; }
        [DbColumn("nume")]
        public string Name { get; set; }
        [DbColumn("versiune")]
        public int Version { get; set; }

        public override bool Equals(object obj)
        {
            if (obj.GetType().Equals(typeof(OS)))
            {
                OS other = (OS)obj;
                return Id.Equals(other.Id);
            }

            return false;
        }

        public override string ToString()
        {
            return ($"{Id} {Name} {Version}");
        }
    }
}
