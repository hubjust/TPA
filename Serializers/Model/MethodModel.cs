using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using DBCore.Model;

namespace Serializers.Model
{
    [DataContract(Name = "MethodModel", IsReference = true)]
    public class MethodModel
    {
        private MethodModel() { }

        public MethodModel(MethodBase baseMethod)
        {
            this.Name = baseMethod.Name;
            this.AbstractEnum = baseMethod.AbstractEnum;
            this.AccessLevel = baseMethod.AccessLevel;
            this.Extension = baseMethod.Extension;
            this.ReturnType = TypeModel.GetOrAdd(baseMethod.ReturnType);
            this.StaticEnum = baseMethod.StaticEnum;
            this.VirtualEnum = baseMethod.VirtualEnum;

            GenericArguments = baseMethod.GenericArguments?.Select(TypeModel.GetOrAdd).ToList();

            Parameters = baseMethod.Parameters?.Select(t => new ParameterModel(t)).ToList();
        }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public List<TypeModel> GenericArguments { get; set; }

        [DataMember]
        public DBCore.Enum.AccessLevel AccessLevel { get; set; }

        [DataMember]
        public DBCore.Enum.AbstractEnum AbstractEnum { get; set; }

        [DataMember]
        public DBCore.Enum.StaticEnum StaticEnum { get; set; }

        [DataMember]
        public DBCore.Enum.VirtualEnum VirtualEnum { get; set; }

        [DataMember]
        public TypeModel ReturnType { get; set; }

        [DataMember]
        public bool Extension { get; set; }

        [DataMember]
        public List<ParameterModel> Parameters { get; set; }

    }
}
