using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace arun.Model
{
    public class Resutdata
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Status { get; set; }
        [DataMember]
        public string SlotId { get; set; }
        [DataMember]
        public int IsSystem { get; set; }
    }
}