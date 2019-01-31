using System;
using System.ComponentModel.Composition;
using System.Reflection;

using Core.Model;
using MEF;
using Interfaces;

namespace Model
{
    public class Repository
    {
        public AssemblyMetadata Metadata { get; set; }

        [ImportMany]
        ImportSelector<ISerializer<AssemblyBase>> serializer;

        public Repository()
        {
            new Bootstrapper().ComposeApplication(this);
        }

        public void Save(IFileSelector selector)
        {
            if (Metadata == null)
            {
                throw new InvalidOperationException("No metadata to save");
            }

            serializer.GetImport().Serialize(selector, DataTransferGraph.AssemblyBase(Metadata));
        }

        public void Load(IFileSelector selector)
        {
            Metadata = new AssemblyMetadata(serializer.GetImport().Deserialize(selector));
        }

        public void CreateFromFile(string path)
        {
            Metadata = new AssemblyMetadata(Assembly.LoadFrom(path));
        }
    }
}
