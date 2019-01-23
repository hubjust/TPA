using System.Collections.Generic;
using System.Linq;
using DBCore.Model;
using Database.Model;

public static class DataTransferGraph
{
    public static AssemblyBase AssemblyBase(DatabaseAssembly assemblyModel)
    {
        dictionaryType.Clear();
        return new AssemblyBase()
        {
            Name = assemblyModel.Name,
            Namespaces = assemblyModel.Namespaces?.Select(NamespaceBase)
        };
    }

    public static NamespaceBase NamespaceBase(DatabaseNamespace namespaceModel)
    {
        NamespaceBase nb = new NamespaceBase()
        {
            Name = namespaceModel.Name,
            Types = namespaceModel.Types?.Select(TypeBase)
        };
        return nb;
    }

    public static TypeBase TypeBase(DatabaseType typeModel)
    {
        TypeBase typeBase = new TypeBase()
        {
            Name = typeModel.Name
        };

        dictionaryType.Add(typeBase.Name, typeBase);

        typeBase.NamespaceName = typeModel.NamespaceName;
        typeBase.Type = typeModel.Type;
        typeBase.BaseType = TypeBase(typeModel.BaseType);
        typeBase.DeclaringType = TypeBase(typeModel.DeclaringType);
        typeBase.AccessLevel = typeModel.AccessLevel;
        typeBase.AbstractEnum = typeModel.AbstractEnum;
        typeBase.StaticEnum = typeModel.StaticEnum;
        typeBase.SealedEnum = typeModel.SealedEnum;

        typeBase.Constructors = typeModel.Constructors?.Select(MethodBase).ToList();
        typeBase.Fields = typeModel.Fields?.Select(FieldBase).ToList();
        typeBase.GenericArguments = typeModel.GenericArguments?.Select(GetOrAdd).ToList();
        typeBase.ImplementedInterfaces = typeModel.ImplementedInterfaces?.Select(GetOrAdd).ToList();
        typeBase.Methods = typeModel.Methods?.Select(MethodBase).ToList();
        typeBase.NestedTypes = typeModel.NestedTypes?.Select(TypeBase).ToList();
        typeBase.Properties = typeModel.Properties?.Select(PropertyBase).ToList();


        return typeBase;
    }

    public static MethodBase MethodBase(DatabaseMethod methodModel)
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

    public static FieldBase FieldBase(DatabaseField fieldModel)
    {
        return new FieldBase()
        {
            Name = fieldModel.Name,
            //Type = GetOrAdd(fieldModel.),
            AccessLevel = fieldModel.AccessLevel,
            StaticEnum = fieldModel.StaticEnum
        };
    }

    public static ParameterBase ParameterBase(DatabaseParameter parameterModel)
    {
        return new ParameterBase()
        {
            Name = parameterModel.Name,
            TypeMetadata = TypeBase(parameterModel.Type)
        };
    }

    public static PropertyBase PropertyBase(DatabaseProperty propertyModel)
    {
        return new PropertyBase()
        {
            Name = propertyModel.Name,
            Type = TypeBase(propertyModel.Type)
        };
    }

    public static TypeBase GetOrAdd(DatabaseType baseType)
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