using DBCore.Model;
using DBCore.Enum;

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Database.Model
{
    [Table("Type")]
    public class DatabaseType
    {
        public static Dictionary<string, DatabaseType> DictionaryType = new Dictionary<string, DatabaseType>();

        [Key, StringLength(150)]
        public string Name { get; set; }
        public string NamespaceName { get; set; }
        public DatabaseType BaseType { get; set; }
        public DatabaseType DeclaringType { get; set; }
        public TypeKind Type { get; set; }
        public AccessLevel AccessLevel { get; set; }
        public AbstractEnum AbstractEnum { get; set; }
        public StaticEnum StaticEnum { get; set; }
        public SealedEnum SealedEnum { get; set; }
        public ICollection<DatabaseType> GenericArguments { get; set; }
        public ICollection<DatabaseType> Attributes { get; set; }
        public ICollection<DatabaseType> ImplementedInterfaces { get; set; }
        public ICollection<DatabaseType> NestedTypes { get; set; }
        public ICollection<DatabaseField> Fields { get; set; }
        public ICollection<DatabaseProperty> Properties { get; set; }
        public ICollection<DatabaseMethod> Methods { get; set; }
        public ICollection<DatabaseMethod> Constructors { get; set; }

        public DatabaseType()
        {
            GenericArguments = new List<DatabaseType>();
            Attributes = new List<DatabaseType>();
            ImplementedInterfaces = new List<DatabaseType>();
            NestedTypes = new List<DatabaseType>();
            Fields = new List<DatabaseField>();
            Properties = new List<DatabaseProperty>();
            Methods = new List<DatabaseMethod>();
            Constructors = new List<DatabaseMethod>();
        }

        public DatabaseType(TypeBase typeBase)
        {
            Name = typeBase.Name;
            NamespaceName = typeBase.NamespaceName;

            if (!DictionaryType.ContainsKey(Name))
            {
                DictionaryType.Add(Name, this);
            }

            BaseType = GetOrAdd(typeBase.BaseType);
            Type = typeBase.Type;
            DeclaringType = GetOrAdd(typeBase.DeclaringType);
            AccessLevel = typeBase.AccessLevel;
            SealedEnum = typeBase.SealedEnum;
            AbstractEnum = typeBase.AbstractEnum;
            Constructors = typeBase.Constructors?.Select(c => new DatabaseMethod(c)).ToList();
            Fields = typeBase.Fields?.Select(f => new DatabaseField(f)).ToList();
            GenericArguments = typeBase.GenericArguments?.Select(a => GetOrAdd(a)).ToList();
            ImplementedInterfaces = typeBase.ImplementedInterfaces?.Select(i => GetOrAdd(i)).ToList();
            Methods = typeBase.Methods?.Select(m => new DatabaseMethod(m)).ToList();
            NestedTypes = typeBase.NestedTypes?.Select(t => GetOrAdd(t)).ToList();
            Properties = typeBase.Properties?.Select(p => new DatabaseProperty(p)).ToList();
        }

        public static DatabaseType GetOrAdd(TypeBase baseType)
        {
            if (baseType != null)
            {
                if (DictionaryType.ContainsKey(baseType.Name))
                {
                    return DictionaryType[baseType.Name];
                }
                else
                {
                    return new DatabaseType(baseType);
                }
            }
            else
                return null;
        }

    }
}
