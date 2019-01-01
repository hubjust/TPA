using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using Model;

namespace Serializers.Model
{
    [DataContract(Name = "TypeModel", IsReference = true)]
    public class TypeModel
    {
        private TypeModel() { }

        private TypeModel(TypeMetadata baseType)
        {
            this.Name = baseType.Name;
            TypeDictionary.Add(Name, this);
            this.NamespaceName = baseType.NamespaceName;
            this.Type = baseType.TypeKindProperty;

            this.BaseType = GetOrAdd(baseType.BaseType);
            this.DeclaringType = GetOrAdd(baseType.DeclaringType);

            this.AbstractEnum = baseType.Modifiers.Item3;
            this.AccessLevel = baseType.Modifiers.Item1;
            this.SealedEnum = baseType.Modifiers.Item2;
            //this.StaticEnum = baseType.Modifiers.

            Constructors = baseType.Constructors?.Select(t => new MethodModel(t)).ToList();

            Fields = baseType.Fields?.Select(t => new ParameterModel(t)).ToList();

            GenericArguments = baseType.GenericArguments?.Select(GetOrAdd).ToList();

            ImplementedInterfaces = baseType.ImplementedInterfaces?.Select(GetOrAdd).ToList();

            Methods = baseType.Methods?.Select(t => new MethodModel(t)).ToList();

            NestedTypes = baseType.NestedTypes?.Select(GetOrAdd).ToList();

            Properties = baseType.Properties?.Select(t => new PropertyModel(t)).ToList();

        }

        public static TypeModel GetOrAdd(TypeMetadata baseType)
        {
            if (baseType != null)
            {
                if (TypeDictionary.ContainsKey(baseType.Name))
                {
                    return TypeDictionary[baseType.Name];
                }
                else
                {
                    return new TypeModel(baseType);
                }
            }
            else
                return null;
        }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string NamespaceName { get; set; }

        [DataMember]
        public TypeModel BaseType { get; set; }

        [DataMember]
        public List<TypeModel> GenericArguments { get; set; }

        [DataMember]
        public AccessLevel AccessLevel { get; set; }

        [DataMember]
        public AbstractEnum AbstractEnum { get; set; }

        [DataMember]
        public SealedEnum SealedEnum { get; set; }

        [DataMember]
        public TypeKind Type { get; set; }

        [DataMember]
        public List<TypeModel> ImplementedInterfaces { get; set; }

        [DataMember]
        public List<TypeModel> NestedTypes { get; set; }

        [DataMember]
        public List<PropertyModel> Properties { get; set; }

        [DataMember]
        public TypeModel DeclaringType { get; set; }

        [DataMember]
        public List<MethodModel> Methods { get; set; }

        [DataMember]
        public List<MethodModel> Constructors { get; set; }

        [DataMember]
        public List<ParameterModel> Fields { get; set; }

        public static Dictionary<string, TypeModel> TypeDictionary = new Dictionary<string, TypeModel>();
    }
}
