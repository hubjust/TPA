using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Serializers.Model;

namespace Serializers
{
    public static class DataTransferGraph
    {
        public static AssemblyMetadata AssemblyMetadata(AssemblyModel assemblyModel)
        {
            dictionaryType = new Dictionary<string, TypeMetadata>();
            return new AssemblyMetadata()
            {
                Name = assemblyModel.Name,
                Namespaces = assemblyModel.Namespaces?.Select(NamespaceMetadata).ToList()
            };
        }

        public static NamespaceMetadata NamespaceMetadata(NamespaceMetadata namespaceModel)
        {
            return new NamespaceMetadata()
            {
                Types = namespaceModel.Types?.Select(GetOrAdd).ToList();
        }
    
        public static NamespaceMetadata NamespaceMetadata(NamespaceMetadata namespaceModel)
        {
            return new NamespaceMetadata()
            {
                Name = namespaceModel.Name,
                Types = namespaceModel.Types?.Select(GetOrAdd).ToList()
            };
        }

        public static TypeMetadata TypeMetadata(TypeModel typeModel)
        {
            TypeMetadata typeBase = new TypeMetadata()
            {
                Name = typeModel.Name
            };

            dictionaryType.Add(typeBase.Name, typeBase);

            typeBase.NamespaceName = typeModel.NamespaceName;
            typeBase.TypeKindProperty = typeModel.Type;
            typeBase.BaseType = GetOrAdd(typeModel.BaseType);
            typeBase.DeclaringType = GetOrAdd(typeModel.DeclaringType);
            typeBase.AbstractEnum = typeModel.AbstractEnum;
            typeBase.AccessLevel = typeModel.AccessLevel;
            typeBase.Modifiers.Item2 = typeModel.SealedEnum;

            typeBase.Constructors = typeModel.Constructors?.Select(MethodMetadata).ToList();
            typeBase.Fields = typeModel.Fields?.Select(ParameterMetadata).ToList();
            typeBase.GenericArguments = typeModel.GenericArguments?.Select(GetOrAdd).ToList();
            typeBase.ImplementedInterfaces = typeModel.ImplementedInterfaces?.Select(GetOrAdd).ToList();
            typeBase.Methods = typeModel.Methods?.Select(MethodMetadata).ToList();
            typeBase.NestedTypes = typeModel.NestedTypes?.Select(GetOrAdd).ToList();
            typeBase.Properties = typeModel.Properties?.Select(PropertyMetadata).ToList();

            return typeBase;
        }

        public static MethodMetadata MethodMetadata(MethodModel methodModel)
        {
            return new MethodMetadata()
            {
                Name = methodModel.Name,
                Modifiers. = methodModel.AbstractEnum,
                AccessLevel = methodModel.AccessLevel,
                Extension = methodModel.Extension,
                ReturnType = GetOrAdd(methodModel.ReturnType),
                StaticEnum = methodModel.StaticEnum,
                VirtualEnum = methodModel.VirtualEnum,
                GenericArguments = methodModel.GenericArguments?.Select(GetOrAdd).ToList(),
                Parameters = methodModel.Parameters?.Select(ParameterBase).ToList()
            };
        }

        public static ParameterMetadata ParameterMetadata(ParameterModel parameterModel)
        {
            return new ParameterMetadata()
            {
                Name = parameterModel.Name,
                Type = GetOrAdd(parameterModel.Type)
            };
        }

        public static PropertyMetadata PropertyMetadata(PropertyModel propertyModel)
        {
            return new PropertyMetadata()
            {
                Name = propertyModel.Name,
                Type = GetOrAdd(propertyModel.Type)
            };
        }

        public static TypeMetadata GetOrAdd(TypeModel baseType)
        {
            if (baseType != null)
            {
                if (dictionaryType.ContainsKey(baseType.Name))
                {
                    return dictionaryType[baseType.Name];
                }
                else
                {
                    return TypeMetadata(baseType);
                }
            }
            else
                return null;
        }

        private static Dictionary<string, TypeMetadata> dictionaryType;



    }
}

