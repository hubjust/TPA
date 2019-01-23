using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Model
{
    public abstract class BaseMetadata
    {
        public string Name { get; set; }

        public BaseMetadata(string name)
        {
            Name = name;
        }

        public BaseMetadata() { }
    }
}