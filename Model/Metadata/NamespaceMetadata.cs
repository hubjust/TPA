
using System;
using System.Collections.Generic;
using System.Linq;

namespace Model
{
    public class NamespaceMetadata : BaseMetadata
    {
        public List<TypeMetadata> Types { get; set; }

        public NamespaceMetadata(string name, List<Type> types)
            : base(name)
        {
            Types = (from type
                     in types
                     orderby type.Name
                     select new TypeMetadata(type)).ToList();
        }

        public NamespaceMetadata() { }
    }
}