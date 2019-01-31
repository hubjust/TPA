using System.Runtime.Serialization;
using Core.Model;

namespace Serializers.Model
{
    [DataContract(Name = "PropertyModel", IsReference = true)]
    public class PropertyModel
    {
        private PropertyModel() { }

        public PropertyModel(PropertyBase propertyMetadata)
        {
            this.Name = propertyMetadata.Name;
            this.Type = TypeModel.GetOrAdd(propertyMetadata.Type);
        }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public TypeModel Type { get; set; }
    }
}