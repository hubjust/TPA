using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using DBCore.Model;
using DBCore;
using DBCore.Enum;

namespace Serializers.Model
{
    [DataContract(Name = "NamespaceModel", IsReference = true)]
    public class NamespaceModel
    {
        private NamespaceModel(object key) { }

        public NamespaceModel(NamespaceBase namespaceMetadata)
        {
            this.Name = namespaceMetadata.Name;
            Types = namespaceMetadata.Types?.Select(t => TypeModel.GetOrAdd(t)).ToList();
        }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public List<TypeModel> Types { get; set; }

    }
}
