using DBCore.Model;
using DBCore.Enum;

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Database.Model
{
    [Table("Method")]
    public class DatabaseMethod
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public bool Extension { get; set; }
        public DatabaseType ReturnType { get; set; }
        public ICollection<DatabaseType> GenericArguments { get; set; }
        public ICollection<DatabaseParameter> Parameters { get; set; }
        public AccessLevel AccessLevel { get; set; }
        public AbstractEnum AbstractEnum { get; set; }
        public StaticEnum StaticEnum { get; set; }
        public VirtualEnum VirtualEnum { get; set; }

        public DatabaseMethod()
        {
            ReturnType = new DatabaseType();
            GenericArguments = new List<DatabaseType>();
            Parameters = new List<DatabaseParameter>();
        }

        public DatabaseMethod(MethodBase baseMethod)
        {
            this.Name = baseMethod.Name;
            this.AbstractEnum = baseMethod.AbstractEnum;
            this.AccessLevel = baseMethod.AccessLevel;
            this.Extension = baseMethod.Extension;
            this.ReturnType = DatabaseType.GetOrAdd(baseMethod.ReturnType);
            this.StaticEnum = baseMethod.StaticEnum;
            this.VirtualEnum = baseMethod.VirtualEnum;

            GenericArguments = baseMethod.GenericArguments?.Select(DatabaseType.GetOrAdd).ToList();

            Parameters = baseMethod.Parameters?.Select(t => new DatabaseParameter(t)).ToList();
        }
    }
}
