using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Model
{
  public class TypeMetadata : Metadata
  {

    public static Dictionary<string, TypeMetadata> DictionaryType = new Dictionary<string, TypeMetadata>();
    public TypeMetadata BaseType { get; set; }
    public List<TypeMetadata> GenericArguments { get; set; }
    public string NamespaceName { get; set; }
    public TupleThree<AccessLevel, SealedEnum, AbstractEnum> Modifiers { get; set; }
    public TypeKind Type { get; set; }

    public TypeMetadata DeclaringType { get; set; }

    public List<TypeMetadata> ImplementedInterfaces { get; set; }
    public List<TypeMetadata> NestedTypes { get; set; }
    public List<MethodMetadata> Methods { get; set; }
    public List<MethodMetadata> Constructors { get; set; }
    public List<ParameterMetadata> Fields { get; set; }
    public List<PropertyMetadata> Properties { get; set; }

    public TypeMetadata() { }

    public TypeMetadata(Type type) : base(type.Name)
    {
        if (!DictionaryType.ContainsKey(Name))
        {
            DictionaryType.Add(Name, this);
        }

        if (!type.IsGenericTypeDefinition)
        {
            GenericArguments = null;
        }
         
        else
        {
            GenericArguments = TypeMetadata.EmitGenericArguments(type.GetGenericArguments()).ToList();
        }

        Constructors = MethodMetadata.EmitMethods(type.GetConstructors()).ToList();
        Methods = MethodMetadata.EmitMethods(type.GetMethods()).ToList();
        DeclaringType = EmitDeclaringType(type.DeclaringType);
        NestedTypes = EmitNestedTypes(type.GetNestedTypes()).ToList();
        ImplementedInterfaces = EmitImplements(type.GetInterfaces()).ToList();
        Modifiers = EmitModifiers(type);
        BaseType = EmitExtends(type.BaseType);
        Properties = PropertyMetadata.EmitProperties(type.GetProperties()).ToList();
        Fields = EmitFields(type.GetFields()).ToList();
        Type = GetTypeKind(type);
    }

    static TupleThree<AccessLevel, SealedEnum, AbstractEnum> EmitModifiers(Type type)
    {
        AccessLevel _access = AccessLevel.IsPrivate;
        if (type.IsPublic)
            _access = AccessLevel.IsPublic;
        else if (type.IsNestedPublic)
            _access = AccessLevel.IsPublic;
        else if (type.IsNestedFamily)
            _access = AccessLevel.IsProtected;
        else if (type.IsNestedFamANDAssem)
            _access = AccessLevel.IsProtectedInternal;
        SealedEnum _sealed = SealedEnum.NotSealed;
        if (type.IsSealed) _sealed = SealedEnum.Sealed;
        AbstractEnum _abstract = AbstractEnum.NotAbstract;
        if (type.IsAbstract)
            _abstract = AbstractEnum.Abstract;
        return new Tuple<AccessLevel, SealedEnum, AbstractEnum>(_access, _sealed, _abstract);
    }

    private TypeMetadata(string typeName, string namespaceName) : base(typeName)
    {
        this.NamespaceName = namespaceName;
    }

    private TypeMetadata(string typeName, string namespaceName, IEnumerable<TypeMetadata> genericArguments) : this(typeName, namespaceName)
    {
        this.GenericArguments = genericArguments.ToList();
    }

    public enum TypeKind
    {
        Enum, Struct, Interface, Class
    }

    public static TypeMetadata EmitReference(Type type)
    {
        if (!type.IsGenericType)
            return new TypeMetadata(type.Name, type.GetNamespace());
        else
            return new TypeMetadata(type.Name, type.GetNamespace(), EmitGenericArguments(type.GetGenericArguments()));
    }

    public static IEnumerable<TypeMetadata> EmitGenericArguments(IEnumerable<Type> arguments)
    {
        foreach (Type typ in arguments)
        {
            StoreType(typ);
        }
        return from Type _argument in arguments select EmitReference(_argument);
    }

    public static void StoreType(Type type)
    {
        if (!DictionaryType.ContainsKey(type.Name))
        {
            new TypeMetadata(type);
        }
    }

    private static IEnumerable<ParameterMetadata> EmitFields(IEnumerable<FieldInfo> fieldInfo)
    {
        List<ParameterMetadata> parameters = new List<ParameterMetadata>();
        foreach (FieldInfo field in fieldInfo)
        {
            StoreType(field.FieldType);
            parameters.Add(new ParameterMetadata(field.Name, TypeMetadata.EmitReference(field.FieldType)));
        }
        return parameters;
    }

    private TypeMetadata EmitDeclaringType(Type declaringType)
    {
        if (declaringType == null)
            return null;
        StoreType(declaringType);
        return EmitReference(declaringType);
    }
    private IEnumerable<TypeMetadata> EmitNestedTypes(IEnumerable<Type> nestedTypes)
    {
        foreach (Type typ in nestedTypes)
        {
            StoreType(typ);
        }

        return from _type in nestedTypes
                where _type.GetVisible()
                select new TypeMetadata(_type);
    }
    private IEnumerable<TypeMetadata> EmitImplements(IEnumerable<Type> interfaces)
    {
        foreach (Type @interface in interfaces)
        {
            StoreType(@interface);
        }

        return from currentInterface in interfaces
                select EmitReference(currentInterface);
    }
    private static TypeKind GetTypeKind(Type type)
    {
        return type.IsEnum ? TypeKind.Enum :
                type.IsValueType ? TypeKind.Struct :
                type.IsInterface ? TypeKind.Interface :
                TypeKind.Class;
    }

    private static TypeMetadata EmitExtends(Type baseType)
    {
        if (baseType == null || baseType == typeof(Object) || baseType == typeof(ValueType) || baseType == typeof(Enum))
            return null;
        StoreType(baseType);
        return EmitReference(baseType);
    }
    }
}