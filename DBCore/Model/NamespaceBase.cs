using System.Collections.Generic;

namespace DBCore.Model
{
    public class NamespaceBase
    {
        public string Name { get; set; }

        public IEnumerable<TypeBase> Types { get; set; }
    }
}
