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

    public static Dictionary<string, TypeMetadata> m_dictionarType = new Dictionary<string, TypeMetadata>();
    public TypeMetadata m_BaseType { get; set; }
    public List<TypeMetadata> m_GenericArguments { get; set; }
    public string m_NamespaceName { get; set; }
    public TupleThree<AccessLevel, SealedEnum, AbstractENum> m_Modifiers { get; set; }
    public TypeKind m_Type { get; set; }

    public List<TypeMetadata> m_ImplementedInterfaces { get; set; }
    public List<TypeMetadata> m_NestedTypes { get; set; }
    public List<PropertyMetadata> m_Properties { get; set; }
    public TypeMetadata m_DeclaringType { get; set; }
    public List<MethodMetadata> m_Methods { get; set; }
    public List<MethodMetadata> m_Constructors { get; set; }
    public List<ParameterMetadata> m_Fields { get; set; }

    public TypeMetadata() { }

    public TypeMetadata(Type type) : base(type.Name)
    {
        if (!m_dictionarType.ContainsKey(m_Name))
        {
            m_dictionarType.Add(m_Name, this);
        }

        m_DeclaringType = EmitDeclaringType(type.DeclaringType);
        m_Constructors = MethodMetadata.EmitMethods(type.GetConstructors()).ToList();
        m_Methods = MethodMetadata.EmitMethods(type.GetMethods()).ToList();
        m_NestedTypes = EmitNestedTypes(type.GetNestedTypes()).ToList();
        m_ImplementedInterfaces = EmitImplements(type.GetInterfaces()).ToList();
        m_GenericArguments = !type.IsGenericTypeDefinition ? null : TypeMetadata.EmitGenericArguments(type.GetGenericArguments()).ToList();
        m_Modifiers = EmitModifiers(type);
        m_BaseType = EmitExtends(type.BaseType);
        m_Properties = PropertyMetadata.EmitProperties(type.GetProperties()).ToList();
        m_Type = GetTypeKind(type);
        //m_Attributes = type.GetCustomAttributes(false).Cast<Attribute>().ToList();
        m_Fields = EmitFields(type.GetFields()).ToList();
    }


        static TupleThree<AccessLevel, SealedEnum, AbstractENum> EmitModifiers(Type type)
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
        AbstractENum _abstract = AbstractENum.NotAbstract;
        if (type.IsAbstract)
            _abstract = AbstractENum.Abstract;
        return new Tuple<AccessLevel, SealedEnum, AbstractENum>(_access, _sealed, _abstract);
    }


        private TypeMetadata(string typeName, string namespaceName) : base(typeName)
        {
            this.m_NamespaceName = namespaceName;
        }

        private TypeMetadata(string typeName, string namespaceName, IEnumerable<TypeMetadata> genericArguments) : this(typeName, namespaceName)
        {
            this.m_GenericArguments = genericArguments.ToList();
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
            if (!m_dictionarType.ContainsKey(type.Name))
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