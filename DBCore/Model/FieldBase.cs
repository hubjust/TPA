using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBCore.Enum;
using DBCore.Model;

namespace DBCore.Model
{
    public class FieldBase
    {
        public string Name { get; set; }

        public TypeBase Type { get; set; }

        public ICollection<TypeBase> AttributesMetadata { get; set; }

        public AccessLevel AccessLevel { get; set; }

        public StaticEnum StaticEnum { get; set; }
    }
}
