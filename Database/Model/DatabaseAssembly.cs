using DBCore.Model;

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Database.Model
{
    [Table("Assembly")]
    public class DatabaseAssembly
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public ICollection<DatabaseNamespace> Namespaces { get; set; }

        public DatabaseAssembly()
        {
            Namespaces = new List<DatabaseNamespace>();
        }

        public DatabaseAssembly(AssemblyBase assemblyBase)
        {
            Name = assemblyBase.Name;
            Namespaces = assemblyBase.Namespaces?.Select(n => new DatabaseNamespace(n)).ToList();
        }

    }
}
