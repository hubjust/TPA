using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public abstract class Metadata
    {
        public string m_Name { get; set; }

        public Metadata() { }

        public Metadata(string m_name)
        {
            m_Name = m_name;
        }
    }
}
