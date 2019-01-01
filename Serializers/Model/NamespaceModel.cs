using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using Model;

namespace Serializers.Model
{
    [DataContract(Name = "NamespaceModel", IsReference = true)]
    public class NamespaceModel
    {
        private NamespaceModel() { }

        public NamespaceModel(NamespaceMetadata namespaceMetadata)
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
