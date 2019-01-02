using System.Collections.Generic;

namespace DBCore.Model
{
    public class  AssemblyBase
    {   
        public List<NamespaceBase> Namespaces { get; set; }
        public string Name { get; set; }
    }
}
