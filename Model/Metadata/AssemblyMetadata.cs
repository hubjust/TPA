using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;

using Core.Model;

namespace Model
{
    public class AssemblyMetadata : BaseMetadata
    {
        public IEnumerable<NamespaceMetadata> Namespaces { get; set; }

        public AssemblyMetadata(Assembly assembly)
            :base(assembly.ManifestModule.Name)
        {       
            Namespaces = (from Type type in assembly.GetTypes()
                          where type.IsNested == false
                          group type by type.GetNamespace() into _group
                          orderby _group.Key
                          select new NamespaceMetadata(_group.Key, _group.ToList())).ToList();
        }

        public AssemblyMetadata(AssemblyBase baseAssembly)
        {
            Name = baseAssembly.Name;
            Namespaces = baseAssembly.Namespaces?.Select(ns => new NamespaceMetadata(ns));
        }

        public AssemblyMetadata() { }
    }
}