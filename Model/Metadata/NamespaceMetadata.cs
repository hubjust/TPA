
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Model
{
    [DataContract]
    public class NamespaceMetadata : BaseMetadata
    {
        [DataMember]
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