using DBCore.Model;

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Database.Model
{
    [Table("Namespace")]
    public class DatabaseNamespace
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public ICollection<DatabaseType> Types { get; set; }

        public DatabaseNamespace() { }

        public DatabaseNamespace(NamespaceBase namespaceBase)
        {
            Name = namespaceBase.Name;
            Types = namespaceBase.Types?.Select(t => DatabaseType.GetOrAdd(t)).ToList();
        }


    }

}