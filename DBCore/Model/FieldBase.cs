using System.Collections.Generic;
using DBCore.Enum;

namespace DBCore.Model
{
    public class FieldBase
    {
        public string Name { get; set; }

        public TypeBase Type { get; set; }

        public AccessLevel AccessLevel { get; set; }

        public StaticEnum StaticEnum { get; set; }
    }
}
