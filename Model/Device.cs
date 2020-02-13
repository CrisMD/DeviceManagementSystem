using Model.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [DbTable("Devices")]
    [DataContract]
    public class Device
    {
        [DbColumn("id")]
        [DataMember]
        public int Id { get; set; }
        [DbColumn("nume")]
        [DataMember]
        public string Name { get; set; }
        [DbColumn("producator")]
        [DataMember]
        public int Manufacturer { get; set; }
        [DbColumn("tip")]
        [DataMember]
        public int Type { get; set; }
        [DbColumn("OS")]
        [DataMember]
        public int OS { get; set; }
        [DbColumn("procesor")]
        [DataMember]
        public int Processor { get; set; }
        [DbColumn("RAM")]
        [DataMember]
        public int RAM { get; set; }

        public override bool Equals(object obj)
        {
            if (obj.GetType().Equals(typeof(Device)))
            {
                Device other = (Device)obj;
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
