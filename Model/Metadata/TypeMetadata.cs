﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;

namespace Model
{
    [DataContract]
    public class TypeMetadata : BaseMetadata
    {
        [DataMember]
        public string NamespaceName { get; set; }

        [DataMember]
        public static Dictionary<string, TypeMetadata> DictionaryType = new Dictionary<string, TypeMetadata>();

        [DataMember]
        public TypeMetadata BaseType { get; set; }
        [DataMember]
        public TypeMetadata DeclaringType { get; set; }
        [DataMember]
        public TypeKind TypeKindProperty { get; set; }

        public AccessLevel AccessLevel { get; set; }

        public AbstractEnum AbstractEnum { get; set; }

        public StaticEnum StaticEnum { get; set; }

        public SealedEnum SealedEnum { get; set; }

        [DataMember]
        public ICollection<TypeMetadata> GenericArguments { get; set; }
        [DataMember]
        public ICollection<TypeMetadata> Attributes { get; set; }
        [DataMember]
        public ICollection<TypeMetadata> ImplementedInterfaces { get; set; }
        [DataMember]
        public ICollection<TypeMetadata> NestedTypes { get; set; }
        [DataMember]
        public ICollection<FieldMetadata> Fields { get; set; }
        [DataMember]
        public ICollection<PropertyMetadata> Properties { get; set; }
        [DataMember]
        public ICollection<MethodMetadata> Methods { get; set; }
        [DataMember]
        public ICollection<MethodMetadata> Constructors { get; set; }

        internal TypeMetadata(Type type)
            : base(type.Name)
        {
            NamespaceName = type.Namespace;

            if (!DictionaryType.ContainsKey(Name))
            {
                DictionaryType.Add(Name, this);
            }

            BaseType = EmitExtends(type.BaseType);
            DeclaringType = EmitDeclaringType(type.DeclaringType);
            TypeKindProperty = GetTypeKind(type);

            EmitModifiers(type);

            if (!type.IsGenericTypeDefinition)
                GenericArguments = null;
            else
                GenericArguments = TypeMetadata.EmitGenericArguments(type.GetGenericArguments()).ToList();

            Attributes = EmitAttributes(type.GetCustomAttributes(false).Cast<Attribute>());
            ImplementedInterfaces = EmitImplements(type.GetInterfaces()).ToList();
            NestedTypes = EmitNestedTypes(type.GetNestedTypes()).ToList();
            Fields = FieldMetadata.EmitFields(type.GetFields());
            Properties = PropertyMetadata.EmitProperties(type.GetProperties()).ToList();
            Methods = MethodMetadata.EmitMethods(type.GetMethods()).ToList();
            Constructors = MethodMetadata.EmitMethods(type.GetConstructors()).ToList();
        }

        private TypeMetadata(string typeName, string namespaceName)
            : base(typeName)
        {
            NamespaceName = namespaceName;
        }

        private TypeMetadata(string typeName, string namespaceName, IEnumerable<TypeMetadata> genericArguments)
            : this(typeName, namespaceName)
        {
            GenericArguments = genericArguments.ToList();
        }

        public TypeMetadata() { }


        private void EmitModifiers(Type type)
        {
            AccessLevel = type.IsPublic || type.IsNestedPublic ? AccessLevel.IsPublic :
                    type.IsNestedFamily ? AccessLevel.IsProtected :
                    type.IsNestedFamANDAssem ? AccessLevel.Internal : AccessLevel.IsPrivate;

            StaticEnum = type.IsSealed && type.IsAbstract ? StaticEnum.Static : StaticEnum.NotStatic;
            SealedEnum = SealedEnum.NotSealed;
            AbstractEnum = AbstractEnum.NotAbstract;

            if (StaticEnum == StaticEnum.NotStatic)
            {
                SealedEnum = type.IsSealed ? SealedEnum.Sealed : SealedEnum.NotSealed;
                AbstractEnum = type.IsAbstract ? AbstractEnum.Abstract : AbstractEnum.NotAbstract;
            }
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

        private TypeMetadata EmitDeclaringType(Type declaringType)
        {
            if (declaringType == null)
                return null;
            StoreType(declaringType);
            return EmitReference(declaringType);
        }

        internal static ICollection<TypeMetadata> EmitAttributes(IEnumerable<Attribute> attributes)
        {
            List<TypeMetadata> list = new List<TypeMetadata>();
            foreach (Attribute attribute in attributes)
            {
                string fullName = attribute.ToString();
                list.Add(EmitReference(attribute.GetType()));
            }
            return list;
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
            return  type.IsEnum ? TypeKind.Enum :
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