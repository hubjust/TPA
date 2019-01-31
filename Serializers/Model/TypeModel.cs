using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Core.Model;

namespace Serializers.Model
{
    [DataContract(Name = "TypeModel", IsReference = true)]
    public class TypeModel
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string NamespaceName { get; set; }

        [DataMember]
        public TypeModel BaseType { get; set; }

        [DataMember]
        public List<TypeModel> GenericArguments { get; set; }

        [DataMember]
        public Core.Enum.AccessLevel AccessLevel { get; set; }

        [DataMember]
        public Core.Enum.AbstractEnum AbstractEnum { get; set; }

        [DataMember]
        public Core.Enum.SealedEnum SealedEnum { get; set; }

        [DataMember]
        public Core.Enum.StaticEnum StaticEnum { get; set; }

        [DataMember]
        public Core.Enum.TypeKind Type { get; set; }

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
        public List<FieldModel> Fields { get; set; }

        public static Dictionary<string, TypeModel> TypeDictionary = new Dictionary<string, TypeModel>();

        private TypeModel() { }

        public TypeModel(TypeBase baseType)
        {
            this.Name = baseType.Name;
            TypeDictionary.Add(Name, this);
            this.NamespaceName = baseType.NamespaceName;
            this.Type = baseType.Type;

            this.BaseType = GetOrAdd(baseType.BaseType);
            this.DeclaringType = GetOrAdd(baseType.DeclaringType);

            this.AbstractEnum = baseType.AbstractEnum;
            this.AccessLevel = baseType.AccessLevel;
            this.SealedEnum = baseType.SealedEnum;
            this.StaticEnum = baseType.StaticEnum;

            Constructors = baseType.Constructors?.Select(t => new MethodModel(t)).ToList();

            Fields = baseType.Fields?.Select(t => new FieldModel(t)).ToList();

            GenericArguments = baseType.GenericArguments?.Select(GetOrAdd).ToList();

            ImplementedInterfaces = baseType.ImplementedInterfaces?.Select(GetOrAdd).ToList();

            Methods = baseType.Methods?.Select(t => new MethodModel(t)).ToList();

            NestedTypes = baseType.NestedTypes?.Select(GetOrAdd).ToList();

            Properties = baseType.Properties?.Select(t => new PropertyModel(t)).ToList();

        }

        public static TypeModel GetOrAdd(TypeBase baseType)
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
    }
}
