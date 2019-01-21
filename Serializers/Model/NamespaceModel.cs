using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using DBCore.Model;
using DBCore;
using DBCore.Enum;

namespace Serializers.Model
{
    public class NamespaceModel
    {
        private NamespaceModel(object key) { }

        public NamespaceModel(NamespaceBase namespaceMetadata)
        {
            this.Name = namespaceMetadata.Name;
            Types = namespaceMetadata.Types?.Select(t => TypeModel.GetOrAdd(t)).ToList();
        }

        public string Name { get; set; }

        public List<TypeModel> Types { get; set; }
    }
}
