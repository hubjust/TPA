using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using DBCore.Enum;
using DBCore.Model;

namespace Serializers.Model
{
    [DataContract(Name = "FieldModel", IsReference = true)]
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

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public AccessLevel AccessLevel { get; set; }

        [DataMember]
        public StaticEnum StaticEnum { get; set; }

        [DataMember]
        public TypeModel Type { get; set; }
    }
}
