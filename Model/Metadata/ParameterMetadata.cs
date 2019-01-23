using DBCore.Model;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;

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

        public ParameterMetadata(FieldBase t) { }

        public ParameterMetadata(ParameterBase baseElement)
        {
            Name = baseElement.Name;
            Type = TypeMetadata.GetOrAdd(baseElement.Type);

        }
    }
}