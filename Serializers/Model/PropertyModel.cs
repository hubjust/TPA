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
    public class PropertyModel
    {
        private PropertyModel() { }

        public PropertyModel(PropertyBase propertyMetadata)
        {
            this.Name = propertyMetadata.Name;
            this.Type = TypeModel.GetOrAdd(propertyMetadata.Type);
        }

        public string Name { get; set; }

        public TypeModel Type { get; set; }
    }
}