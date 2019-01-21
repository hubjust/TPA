using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using DBCore.Model;

namespace Serializers.Model
{
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

        public string Name { get; set; }

        public List<TypeModel> GenericArguments { get; set; }

        public DBCore.Enum.AccessLevel AccessLevel { get; set; }

        public DBCore.Enum.AbstractEnum AbstractEnum { get; set; }

        public DBCore.Enum.StaticEnum StaticEnum { get; set; }

        public DBCore.Enum.VirtualEnum VirtualEnum { get; set; }

        public TypeModel ReturnType { get; set; }

        public bool Extension { get; set; }

        public List<ParameterModel> Parameters { get; set; }
    }
}
