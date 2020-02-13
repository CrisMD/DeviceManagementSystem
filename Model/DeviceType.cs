using Model.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [DbTable("Tipuri")]
    [DataContract]
    public class DeviceType
    {
        [DbColumn("id")]
        [DataMember]
        public int Id { get; set; }
        [DbColumn("nume")]
        [DataMember]
        public string Name { get; set; }

        public override bool Equals(object obj)
        {
            if (obj.GetType().Equals(typeof(DeviceType)))
            {
                DeviceType other = (DeviceType)obj;
                return Id.Equals(other.Id);
            }

            return false;
        }

        public override string ToString()
        {
            return ($"{Id} {Name}");
        }
    }
}
