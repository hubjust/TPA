using System.Collections.Generic;
using System.Reflection;

namespace Model
{
    public class ParameterMetadata : BaseMetadata
    {
        public TypeMetadata Type { get; set; }
        public ICollection<TypeMetadata> Attributes { get; set; }

        public ParameterMetadata(ParameterInfo parameter)
            : base(parameter.Name)
        {
            Type = TypeMetadata.EmitReference(parameter.ParameterType);
            if (!parameter.GetCustomAttributes().IsNullOrEmpty())
                Attributes = TypeMetadata.EmitAttributes(parameter.GetCustomAttributes());
        }

        public ParameterMetadata() { }
    }
}