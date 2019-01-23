using DBCore.Model;

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Model
{
    [Table("Parameter")]
    public class DatabaseParameter
    {
        [Key]
        public int ID { get; set; }     
        public string Name { get; set; }
        public DatabaseType Type { get; set; }

        public DatabaseParameter()
        {
            MethodParameters = new HashSet<DatabaseMethod>();
            TypeFields = new HashSet<DatabaseType>();
        }

        public DatabaseParameter(ParameterBase parameterBase)
        {
            Name = parameterBase.Name;
            Type = DatabaseType.GetOrAdd(parameterBase.Type);
        }

        public virtual ICollection<DatabaseMethod> MethodParameters { get; set; }
        public virtual ICollection<DatabaseType> TypeFields { get; set; }

    }
}
