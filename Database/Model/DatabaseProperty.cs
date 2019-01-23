using DBCore.Model;

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Model
{
    [Table("Property")]
    public class DatabaseProperty
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public DatabaseType Type { get; set; }
        public virtual ICollection<DatabaseType> databaseTypes { get; set; }

        public DatabaseProperty()
        {
            Type = new DatabaseType();
        }

        public DatabaseProperty(PropertyBase propertyBase)
        {
            Name = propertyBase.Name ?? "default";
            Type = DatabaseType.GetOrAdd(propertyBase.Type);
        }
    }
}
