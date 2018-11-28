using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Model
{
    [DataContract(IsReference = true)]
    public abstract class BaseMetadata
    {
        [DataMember]
        public string Name { get; set; }

        public BaseMetadata(string name)
        {
            Name = name;
        }

        public BaseMetadata() { }
    }
}