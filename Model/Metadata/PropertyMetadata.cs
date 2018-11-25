
using System.Collections.Generic;
using System.Reflection;
using System.Linq;

namespace Model
{
  public class PropertyMetadata : BaseMetadata
  {
        public TypeMetadata Type { get; set; }
        public ICollection<TypeMetadata> Attributes { get; set; }

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