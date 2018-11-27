using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Model;
using Model.Repository;


namespace Serializers
{
    [DataContract]
    public class DataContext
    {
        [DataMember]
        public AssemblyMetadata AssemblyMetadata { get; set; }

        [DataMember]
        public Dictionary<string, TypeMetadata> Dictionary { get; set; }

        public DataContext(TypeRepository typeRepository)
        {
            AssemblyMetadata = typeRepository.AssemblyMetadata;

            Dictionary = new Dictionary<string, TypeMetadata>(TypeRepository.storedTypes);
        }
    }
}
