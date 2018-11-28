using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;

namespace Model
{
    [DataContract]
    public class ParameterMetadata : BaseMetadata
    {
        [DataMember]
        public TypeMetadata Type { get; set; }
        [DataMember]
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