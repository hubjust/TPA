
using System;
using System.Collections.Generic;
using System.Linq;

namespace Model
{
  public class NamespaceMetadata : Metadata
  {
    public List<TypeMetadata> Types { get; set; }

    public NamespaceMetadata() { }
    
    // base - odwolanie do klasy bazowej
    public NamespaceMetadata(string name, List<Type> types) : base(name) 
    {
            this.Types = (from type
                          in types
                          orderby type.Name
                          select new TypeMetadata(type)).ToList();
    }
  }
}