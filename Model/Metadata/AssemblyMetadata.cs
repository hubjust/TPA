
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Model
{
  public class AssemblyMetadata : Metadata
  {
    public List<NamespaceMetadata> Namespaces { get; set; }

    public AssemblyMetadata() { }

    public AssemblyMetadata(Assembly assembly) : base(assembly.ManifestModule.Name)
    {
      Namespaces = (from Type _type in assembly.GetTypes()
                     where _type.GetVisible()
                     group _type by _type.GetNamespace() into _group
                     orderby _group.Key
                     select new NamespaceMetadata(_group.Key, _group.ToList())).ToList();
    }
  }
}