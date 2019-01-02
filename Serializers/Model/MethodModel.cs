using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Model;

namespace Serializers.Model
{
    [DataContract(Name = "MethodModel", IsReference = true)]
    public class MethodModel
    {
        private MethodModel() { }

        public MethodModel(MethodMetadata baseMethod)
        {
            Name = baseMethod.Name;
            AbstractEnum = baseMethod.AbstractEnum;
            AccessLevel = baseMethod.AccessLevel;
            Extension = baseMethod.Extension;
            ReturnType = TypeModel.GetOrAdd(baseMethod.ReturnType);
            StaticEnum = baseMethod.StaticEnum;
            VirtualEnum = baseMethod.VirtualEnum;

            GenericArguments = baseMethod.GenericArguments?.Select(TypeModel.GetOrAdd).ToList();

            Parameters = baseMethod.Parameters?.Select(t => new ParameterModel(t)).ToList();
        }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public List<TypeModel> GenericArguments { get; set; }

        [DataMember]
        public AccessLevel AccessLevel { get; set; }

        [DataMember]
        public AbstractEnum AbstractEnum { get; set; }

        [DataMember]
        public StaticEnum StaticEnum { get; set; }

        [DataMember]
        public VirtualEnum VirtualEnum { get; set; }

        [DataMember]
        public TypeModel ReturnType { get; set; }

        [DataMember]
        public bool Extension { get; set; }

        [DataMember]
        public List<ParameterModel> Parameters { get; set; }

    }
}
