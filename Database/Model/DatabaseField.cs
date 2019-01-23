using DBCore.Model;
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
        public int ID { get; set; }
        [Key]
        public string Name { get; set; }
        public ICollection<DatabaseType> AttributesMetadata { get; set; }
        public AccessLevel AccessLevel { get; set; }
        public StaticEnum StaticEnum { get; set; }

        public DatabaseField()
        {
            AttributesMetadata = new List<DatabaseType>();
        }

        public DatabaseField(FieldBase fieldBase)
        {
            Name = fieldBase.Name;
            AttributesMetadata = fieldBase.AttributesMetadata?.Select(n => new DatabaseType(n)).ToList();
            AccessLevel = fieldBase.AccessLevel;
            StaticEnum = fieldBase.StaticEnum;
        }
    }
}
