using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using Model;
using DBCore;
using DBCore.Model;

namespace Serializers.Model
{
    [DataContract(Name = "AssemblyModel", IsReference = true)]
    public class AssemblyModel
    {
        private AssemblyModel() { }

        public AssemblyModel(AssemblyBase assemblyMetadata)
        {
            this.Name = assemblyMetadata.Name;
            Namespaces = assemblyMetadata.Namespaces?.Select(ns => new NamespaceModel(ns)).ToList();
        }

        [DataMember]
        public List<NamespaceModel> Namespaces { get; set; }

        [DataMember]
        public string Name { get; set; }
    }
}
