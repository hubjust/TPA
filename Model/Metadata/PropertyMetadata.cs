
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Runtime.Serialization;

namespace Model
{
    [DataContract(IsReference = true)]
    public class PropertyMetadata : BaseMetadata
  {
        [DataMember]
        public TypeMetadata Type { get; set; }
        [DataMember]
        public ICollection<TypeMetadata> Attributes { get; set; }

        public PropertyMetadata() { }

        public PropertyMetadata(string propertyName, TypeMetadata propertyType, ICollection<TypeMetadata> attributesMetadata)
            : base(propertyName)
        {
            Type = propertyType;
            Attributes = attributesMetadata;
        }

        public static IEnumerable<PropertyMetadata> EmitProperties(IEnumerable<PropertyInfo> props)
        {
            return from prop
                   in props
                   where prop.GetGetMethod().GetVisible() || prop.GetSetMethod().GetVisible()
                   select new PropertyMetadata(prop.Name, TypeMetadata.EmitReference(prop.PropertyType),
                   TypeMetadata.EmitAttributes(prop.GetCustomAttributes()));
        }
  }
}