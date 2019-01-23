using DBCore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;

namespace Model
{
    [DataContract]
    public class FieldMetadata : BaseMetadata
    {
        [DataMember]
        public TypeMetadata Type;
        [DataMember]
        public ICollection<TypeMetadata> AttributesMetadata { get; set; }

        public AccessLevel AccessLevel { get; set; }

        public StaticEnum StaticEnum { get; set; }

        public FieldMetadata(FieldInfo field)
            : base(field.Name)
        {
            Type = TypeMetadata.EmitReference(field.FieldType);
            AttributesMetadata = TypeMetadata.EmitAttributes(field.GetCustomAttributes());

            EmitModifiers(field);
        }

        public FieldMetadata(FieldBase fieldMetadata)
        {
            this.Name = fieldMetadata.Name;
            this.Type = TypeMetadata.GetOrAdd(fieldMetadata.Type);
            this.AccessLevel = fieldMetadata.AccessLevel.ToLogicEnum();
            this.StaticEnum = fieldMetadata.StaticEnum.ToLogicEnum();
        }

        public FieldMetadata() { }

        internal static ICollection<FieldMetadata> EmitFields(IEnumerable<FieldInfo> fields)
        {
            return (from field in fields
                    //where field.GetVisible()
                    select new FieldMetadata(field)).ToList();
        }

        private void EmitModifiers(FieldInfo field)
        {
            AccessLevel = field.IsPublic ? AccessLevel.IsPublic :
                field.IsFamily ? AccessLevel.IsProtected :
                field.IsAssembly ? AccessLevel.Internal : AccessLevel.IsPrivate;

            StaticEnum = field.IsStatic ? StaticEnum.Static : StaticEnum.NotStatic;
        }
    }
}
