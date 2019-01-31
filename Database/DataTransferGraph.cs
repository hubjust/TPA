using System.Collections.Generic;
using System.Linq;
using Core.Model;
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
        };

        dictionaryType.Add(typeBase.Name, typeBase);

        typeBase.NamespaceName = typeMetadata.NamespaceName;
        typeBase.Type = typeMetadata.Type;
        typeBase.BaseType = TypeBase(typeMetadata.BaseType);
        typeBase.DeclaringType = TypeBase(typeMetadata.DeclaringType);
        typeBase.AccessLevel = typeMetadata.AccessLevel;
        typeBase.AbstractEnum = typeMetadata.AbstractEnum;
        typeBase.StaticEnum = typeMetadata.StaticEnum;
        typeBase.SealedEnum = typeMetadata.SealedEnum;

        typeBase.Constructors = typeMetadata.Constructors?.Select(MethodBase).ToList();
        typeBase.Fields = typeMetadata.Fields?.Select(FieldBase).ToList();
        typeBase.GenericArguments = typeMetadata.GenericArguments?.Select(TypeBase).ToList();
        typeBase.ImplementedInterfaces = typeMetadata.ImplementedInterfaces?.Select(TypeBase).ToList();
        typeBase.Methods = typeMetadata.Methods?.Select(MethodBase).ToList();
        typeBase.NestedTypes = typeMetadata.NestedTypes?.Select(TypeBase).ToList();
        typeBase.Properties = typeMetadata.Properties?.Select(PropertyBase).ToList();

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