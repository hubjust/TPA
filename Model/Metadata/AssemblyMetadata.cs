using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Model
{
    public class AssemblyMetadata : BaseMetadata
    {
        public ICollection<NamespaceMetadata> Namespaces { get; set; }

        public AssemblyMetadata(Assembly assembly)
            :base(assembly.ManifestModule.Name)
        {       
            Namespaces = (from Type type in assembly.GetTypes()
                          where type.GetVisible()
                          group type by type.GetNamespace() into _group
                          orderby _group.Key
                          select new NamespaceMetadata(_group.Key, _group.ToList())).ToList();
        }

        public AssemblyMetadata() { }
    }
}