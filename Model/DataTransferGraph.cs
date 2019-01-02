using System.Collections.Generic;
using System.Linq;
using DBCore.Model;

namespace Model
{
    public static class DataTransferGraph
    {
        public static AssemblyBase AssemblyBase(AssemblyMetadata assemblyModel)
        {
            dictionaryType = new Dictionary<string, TypeBase>();
            return new AssemblyBase()
            {
                Name = assemblyModel.Name,
                Namespaces = assemblyModel.Namespaces?.Select(assemblyModel)
            };
        }

        public static NamespaceBase NamespaceBase(NamespaceMetadata namespaceModel)
        {
            return new NamespaceBase()
            {
                Name = namespaceModel.Name,
                Types = namespaceModel.Types?.Select(GetOrAdd).ToList()
            };
        }

        public static TypeBase TypeBase(TypeMetadata typeModel)
        {
            TypeBase typeBase = new TypeBase()
            {
                Name = typeModel.Name
            };

            dictionaryType.Add(typeBase.Name, typeBase);

            typeBase.NamespaceName = typeModel.NamespaceName;
            typeBase.Type = typeModel.Type.ToBaseEnum();
            typeBase.BaseType = GetOrAdd(typeModel.BaseType);
            typeBase.DeclaringType = GetOrAdd(typeModel.DeclaringType);
            typeBase.AccessLevel = typeModel.AccessLevel.ToBaseEnum();
            typeBase.AbstractEnum = typeModel.AbstractEnum.ToBaseEnum();
            typeBase.StaticEnum = typeModel.StaticEnum.ToBaseEnum();
            typeBase.SealedEnum = typeModel.SealedEnum.ToBaseEnum();

            typeBase.Constructors = typeModel.Constructors?.Select(MethodBase).ToList();
            typeBase.Fields = typeModel.Fields?.Select(FieldBase).ToList();
            typeBase.GenericArguments = typeModel.GenericArguments?.Select(GetOrAdd).ToList();
            typeBase.ImplementedInterfaces = typeModel.ImplementedInterfaces?.Select(GetOrAdd).ToList();
            typeBase.Methods = typeModel.Methods?.Select(MethodBase).ToList();
            typeBase.NestedTypes = typeModel.NestedTypes?.Select(GetOrAdd).ToList();
            typeBase.Properties = typeModel.Properties?.Select(PropertyBase).ToList();


            return typeBase;
        }

        public static MethodBase MethodBase(MethodMetadata methodModel)
        {
            return new MethodBase()
            {
                Name = methodModel.Name,

                Extension = methodModel.Extension,
                ReturnType = GetOrAdd(methodModel.ReturnType),

                GenericArguments = methodModel.GenericArguments?.Select(GetOrAdd).ToList(),

                Parameters = methodModel.Parameters?.Select(ParameterBase).ToList(),

                AccessLevel = methodModel.AccessLevel.ToBaseEnum(),
                AbstractEnum = methodModel.AbstractEnum.ToBaseEnum(),
                StaticEnum = methodModel.StaticEnum.ToBaseEnum(),
                VirtualEnum = methodModel.VirtualEnum.ToBaseEnum()
            };
        }

        public static FieldBase FieldBase(FieldMetadata fieldModel)
        {
            return new FieldBase()
            {
                Name = fieldModel.Name,
                Type = GetOrAdd(fieldModel.Type),
                AccessLevel = fieldModel.AccessLevel.ToBaseEnum(),
                StaticEnum = fieldModel.StaticEnum.ToBaseEnum()
            };
        }

        public static ParameterBase ParameterBase(ParameterMetadata parameterModel)
        {
            return new ParameterBase()
            {
                Name = parameterModel.Name,
                TypeMetadata = GetOrAdd(parameterModel.Type)
            };
        }

        public static PropertyBase PropertyBase(PropertyMetadata propertyModel)
        {
            return new PropertyBase()
            {
                Name = propertyModel.Name,
                Type = GetOrAdd(propertyModel.Type)
            };
        }

        public static TypeBase GetOrAdd(TypeMetadata baseType)
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

        private static Dictionary<string, TypeBase> dictionaryType;
    }
}
