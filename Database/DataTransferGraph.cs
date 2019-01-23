using System.Collections.Generic;
using System.Linq;
using DBCore.Model;
using Database.Model;

public static class DataTransferGraph
{
    private static Dictionary<string, TypeBase> dictionaryType = new Dictionary<string, TypeBase>();

    public static AssemblyBase AssemblyBase(DatabaseAssembly assemblyMetadata)
    {
        dictionaryType.Clear();
        return new AssemblyBase()
        {
            Name = assemblyMetadata.Name,
            Namespaces = assemblyMetadata.Namespaces?.Select(NamespaceBase)
        };
    }

    public static NamespaceBase NamespaceBase(DatabaseNamespace namespaceMetadata)
    {
        return new NamespaceBase()
        {
            Name = namespaceMetadata.Name,
            Types = namespaceMetadata.Types?.Select(TypeBase)
        };
    }

    public static TypeBase TypeBase(DatabaseType typeMetadata)
    {
        if (typeMetadata == null)
        {
            return null;
        }

        if(dictionaryType.ContainsKey(typeMetadata.Name))
        {
            return dictionaryType[typeMetadata.Name];
        }

        TypeBase typeBase = new TypeBase()
        {
            Name = typeMetadata.Name,
            NamespaceName = typeMetadata.NamespaceName,
            Type = typeMetadata.Type,
            BaseType = TypeBase(typeMetadata.BaseType),
            DeclaringType = TypeBase(typeMetadata.DeclaringType),
            AccessLevel = typeMetadata.AccessLevel,
            AbstractEnum = typeMetadata.AbstractEnum,
            StaticEnum = typeMetadata.StaticEnum,
            SealedEnum = typeMetadata.SealedEnum,

            Constructors = typeMetadata.Constructors?.Select(MethodBase).ToList(),
            Fields = typeMetadata.Fields?.Select(FieldBase).ToList(),
            GenericArguments = typeMetadata.GenericArguments?.Select(TypeBase).ToList(),
            ImplementedInterfaces = typeMetadata.ImplementedInterfaces?.Select(TypeBase).ToList(),
            Methods = typeMetadata.Methods?.Select(MethodBase).ToList(),
            NestedTypes = typeMetadata.NestedTypes?.Select(TypeBase).ToList(),
            Properties = typeMetadata.Properties?.Select(PropertyBase).ToList()
        };

        dictionaryType.Add(typeBase.Name, typeBase);

        return typeBase;
    }

    public static MethodBase MethodBase(DatabaseMethod methodMetadata)
    {
        return new MethodBase()
        {
            Name = methodMetadata.Name,

            Extension = methodMetadata.Extension,
            ReturnType = TypeBase(methodMetadata.ReturnType),
            GenericArguments = methodMetadata.GenericArguments?.Select(TypeBase).ToList(),
            Parameters = methodMetadata.Parameters?.Select(ParameterBase).ToList(),
            AccessLevel = methodMetadata.AccessLevel,
            AbstractEnum = methodMetadata.AbstractEnum,
            StaticEnum = methodMetadata.StaticEnum,
            VirtualEnum = methodMetadata.VirtualEnum
        };
    }

    public static FieldBase FieldBase(DatabaseField fieldMetadata)
    {
        return new FieldBase()
        {
            Name = fieldMetadata.Name,
            Type = TypeBase(fieldMetadata.Type),
            AccessLevel = fieldMetadata.AccessLevel,
            StaticEnum = fieldMetadata.StaticEnum
        };
    }

    public static ParameterBase ParameterBase(DatabaseParameter parameterMetadata)
    {
        return new ParameterBase()
        {
            Name = parameterMetadata.Name,
            Type = TypeBase(parameterMetadata.Type)
        };
    }

    public static PropertyBase PropertyBase(DatabaseProperty propertyMetadata)
    {
        return new PropertyBase()
        {
            Name = propertyMetadata.Name,
            Type = TypeBase(propertyMetadata.Type)
        };
    }
}