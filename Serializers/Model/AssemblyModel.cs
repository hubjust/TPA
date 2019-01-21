using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using DBCore.Model;

namespace Serializers.Model
{
    public class AssemblyModel
    {
        private AssemblyModel() { }

        public AssemblyModel(AssemblyBase assemblyMetadata)
        {
            this.Name = assemblyMetadata.Name;
            Namespaces = assemblyMetadata.Namespaces?.Select(ns => new NamespaceModel(ns)).ToList();
        }

        public List<NamespaceModel> Namespaces { get; set; }

        public string Name { get; set; }
    }
}
