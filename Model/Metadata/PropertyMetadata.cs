
using System.Collections.Generic;
using System.Reflection;
using System.Linq;

namespace Model
{
  public class PropertyMetadata : Metadata
  {
        public TypeMetadata Type { get; set; }

        public PropertyMetadata()   { }

        public PropertyMetadata(string propertyName, TypeMetadata propertyType) : base(propertyName)
        {
            Type = propertyType;
        }

        public static IEnumerable<PropertyMetadata> EmitProperties(IEnumerable<PropertyInfo> props)
        {
            return from prop
                   in props
                   where prop.GetGetMethod().GetVisible() || prop.GetSetMethod().GetVisible()
                   select new PropertyMetadata(prop.Name, TypeMetadata.EmitReference(prop.PropertyType));
        }
  }
}