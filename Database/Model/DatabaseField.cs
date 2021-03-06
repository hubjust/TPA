﻿using DBCore.Model;
using DBCore.Enum;

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Database.Model
{
    [Table("Field")]
    public class DatabaseField
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public DatabaseType Type { get; set; }
        public AccessLevel AccessLevel { get; set; }
        public StaticEnum StaticEnum { get; set; }

        public DatabaseField()
        {
            MethodParameters = new HashSet<DatabaseMethod>();
            TypeFields = new HashSet<DatabaseType>();
        }

        public DatabaseField(FieldBase fieldBase)
        {
            Name = fieldBase.Name;
            Type = DatabaseType.GetOrAdd(fieldBase.Type);
            AccessLevel = fieldBase.AccessLevel;
            StaticEnum = fieldBase.StaticEnum;
        }

        public virtual ICollection<DatabaseMethod> MethodParameters { get; set; }
        public virtual ICollection<DatabaseType> TypeFields { get; set; }
    }
}
