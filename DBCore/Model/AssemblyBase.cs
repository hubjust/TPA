using System.Collections.Generic;

namespace Core.Model
{
    public class  AssemblyBase
    {   
        public IEnumerable<NamespaceBase> Namespaces { get; set; }
        public string Name { get; set; }
    }
}
