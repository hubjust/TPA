using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

using DBCore.Model;

namespace Model
{
    [DataContract]
    public class NamespaceMetadata : BaseMetadata
    {
        [DataMember]
        public IEnumerable<TypeMetadata> Types { get; set; }

        public NamespaceMetadata(string name, List<Type> types)
            : base(name)
        {
            Types = (from type
                     in types
                     orderby type.Name
                     select new TypeMetadata(type)).ToList();
        }

        public NamespaceMetadata(NamespaceBase namespaceBase)
        {
            Name = namespaceBase.Name;
            Types = namespaceBase.Types?.Select(t => TypeMetadata.GetOrAdd(t));
        }

        public NamespaceMetadata() { }
    }
}