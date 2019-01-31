using Core.Model;

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

        public DatabaseProperty()
        {
            TypeProperties = new HashSet<DatabaseType>();
        }

        public DatabaseProperty(PropertyBase propertyBase)
        {
            Name = propertyBase.Name ?? "default";
            Type = DatabaseType.GetOrAdd(propertyBase.Type);
        }

        public virtual ICollection<DatabaseType> TypeProperties { get; set; }
    }
}
