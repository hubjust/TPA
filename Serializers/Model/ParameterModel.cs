using System.Runtime.Serialization;
using Core.Model;

namespace Serializers.Model
{
    [DataContract(Name = "ParameterModel", IsReference = true)]
    public class ParameterModel
    {
        public ParameterModel(FieldBase t) {   }

        public ParameterModel(ParameterBase parameterMetadata)
        {
            this.Name = parameterMetadata.Name;
            this.Type = TypeModel.GetOrAdd(parameterMetadata.Type);
        }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public TypeModel Type { get; set; }
    }
}
