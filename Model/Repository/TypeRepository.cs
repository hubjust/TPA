using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Repository
{
    public class TypeRepository
    {
        public AssemblyMetadata AssemblyMetadata { get; set; }

        public static Dictionary<string, TypeMetadata> storedTypes = new Dictionary<string, TypeMetadata>();
    }
}
