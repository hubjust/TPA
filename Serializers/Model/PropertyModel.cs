using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using DBCore.Model;
using DBCore;
using DBCore.Enum;

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