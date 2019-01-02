using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Model;


namespace Serializers.Model
{
    [DataContract(Name = "FieldModel", IsReference = true)]
    public class FieldModel
    {
        private FieldModel() { }

        public FieldModel(FieldMetadata fieldMetadata)
        {
            this.Name = fieldMetadata.Name;
            this.Type = TypeModel.GetOrAdd(fieldMetadata.Type);
            this.AccessLevel = fieldMetadata.AccessLevel;
            this.StaticEnum = fieldMetadata.StaticEnum;
        }

        public List<NamespaceModel> Namespaces { get; set; }

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
