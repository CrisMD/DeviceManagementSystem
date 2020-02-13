using Model.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [DbTable("Imprumuturi")]
    [DataContract]
    public class UserDevice
    {
        [DbColumn("id")]
        [DataMember]
        public int Id { get; set; }
        [DbColumn("utilizator")]
        [DataMember]
        public int UserId { get; set; }
        [DbColumn("device")]
        [DataMember]
        public int DeviceId { get; set; }
        [DbColumn("start_time")]
        [DataMember]
        public DateTime StartTime { get; set; }
        [DbColumn("end_time")]
        [DataMember]
        public DateTime? EndTime { get; set; }
    }
}
