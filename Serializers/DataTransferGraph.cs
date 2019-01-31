using System.Collections.Generic;
using System.Linq;
using Serializers.Model;
using Core.Model;

namespace Serializers
{
    public static class DataTransferGraph
    {
        public static AssemblyBase AssemblyBase(AssemblyModel assemblyModel)
        {
            dictionaryType = new Dictionary<string, TypeBase>();
            return new AssemblyBase()
            {
                Name = assemblyModel.Name,
                Namespaces = assemblyModel.Namespaces?.Select(NamespaceBase).ToList()
            };
        }

        public static NamespaceBase NamespaceBase(NamespaceModel namespaceModel)
        {
            return new NamespaceBase()
            {
                Name = namespaceModel.Name,
                Types = namespaceModel.Types?.Select(GetOrAdd).ToList()
            };
        }

        public static TypeBase TypeBase(TypeModel typeModel)
        {
            TypeBase typeBase = new TypeBase()
            {
                Name = typeModel.Name
            };

            dictionaryType.Add(typeBase.Name, typeBase);

            typeBase.NamespaceName = typeModel.NamespaceName;
            typeBase.Type = typeModel.Type;
            typeBase.BaseType = GetOrAdd(typeModel.BaseType);
            typeBase.DeclaringType = GetOrAdd(typeModel.DeclaringType);
            typeBase.AccessLevel = typeModel.AccessLevel;
            typeBase.AbstractEnum = typeModel.AbstractEnum;
            typeBase.StaticEnum = typeModel.StaticEnum;
            typeBase.SealedEnum = typeModel.SealedEnum;

            typeBase.Constructors = typeModel.Constructors?.Select(MethodBase).ToList();
            typeBase.Fields = typeModel.Fields?.Select(FieldBase).ToList();
            typeBase.GenericArguments = typeModel.GenericArguments?.Select(GetOrAdd).ToList();
            typeBase.ImplementedInterfaces = typeModel.ImplementedInterfaces?.Select(GetOrAdd).ToList();
            typeBase.Methods = typeModel.Methods?.Select(MethodBase).ToList();
            typeBase.NestedTypes = typeModel.NestedTypes?.Select(GetOrAdd).ToList();         
            typeBase.Properties = typeModel.Properties?.Select(PropertyBase).ToList();


            return typeBase;
        }

        public static MethodBase MethodBase(MethodModel methodModel)
        {
            return new MethodBase()
            {
                Name = methodModel.Name,

                Extension = methodModel.Extension,
                ReturnType = GetOrAdd(methodModel.ReturnType),

                GenericArguments = methodModel.GenericArguments?.Select(GetOrAdd).ToList(),
                
                Parameters = methodModel.Parameters?.Select(ParameterBase).ToList(),

                AccessLevel = methodModel.AccessLevel,
                AbstractEnum = methodModel.AbstractEnum,
                StaticEnum = methodModel.StaticEnum,
                VirtualEnum = methodModel.VirtualEnum
            };
        }

        public static FieldBase FieldBase(FieldModel fieldModel)
        {
            return new FieldBase()
            {
                Name = fieldModel.Name,
                Type = GetOrAdd(fieldModel.Type),
                AccessLevel = fieldModel.AccessLevel,
                StaticEnum = fieldModel.StaticEnum
            };
        }

        public static ParameterBase ParameterBase(ParameterModel parameterModel)
        {
            return new ParameterBase()
            {
                Name = parameterModel.Name,
                Type = GetOrAdd(parameterModel.Type)
            };
        }

        public static PropertyBase PropertyBase(PropertyModel propertyModel)
        {
            return new PropertyBase()
            {
                Name = propertyModel.Name,
                Type = GetOrAdd(propertyModel.Type)
            };
        }

        public static TypeBase GetOrAdd(TypeModel baseType)
        {
            if (baseType != null)
            {
                if (dictionaryType.ContainsKey(baseType.Name))
                {
                    return dictionaryType[baseType.Name];
                }
                else
                {
                    return TypeBase(baseType);
                }
            }
            else
                return null;
        }

        private static Dictionary<string, TypeBase> dictionaryType = new Dictionary<string, TypeBase>();
    }

}

