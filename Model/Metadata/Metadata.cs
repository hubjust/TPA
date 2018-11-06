using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public abstract class Metadata
    {
        public string Name { get; set; }

        public Metadata() { }

        public Metadata(string name)
        {
            Name = name;
        }
    }
}
