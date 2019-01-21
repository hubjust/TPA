using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using DBCore.Enum;
using DBCore.Model;

namespace Serializers.Model
{
    public class FieldModel
    {
        public FieldModel(ParameterBase t) { }

        public FieldModel(FieldBase fieldMetadata)
        {
            this.Name = fieldMetadata.Name;
            this.Type = TypeModel.GetOrAdd(fieldMetadata.Type);
            this.AccessLevel = fieldMetadata.AccessLevel;
            this.StaticEnum = fieldMetadata.StaticEnum;
        }

        public List<NamespaceModel> Namespaces { get; set; }

        public string Name { get; set; }

        public AccessLevel AccessLevel { get; set; }

        public StaticEnum StaticEnum { get; set; }

        public TypeModel Type { get; set; }
    }
}
