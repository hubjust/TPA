using Database.Model;
using System;
using System.ComponentModel.Composition;
using System.Data.Entity;
using System.Linq;
using DBCore.Model;

using Interfaces;

namespace Database
{
    [Export(typeof(ISerializer<AssemblyBase>))]
    public class DBSerializer : ISerializer<AssemblyBase>
    {
        public AssemblyBase Deserialize(IFileSelector supplier)
        {
            AssemblyBase assembly;
            using (var ctx = new DatabaseContext())
            {
                ctx.AssemblyModel.Load();
                ctx.FieldModel.Load();
                ctx.NamespaceModel.Load();
                ctx.TypeModel.Load();
                ctx.MethodModel.Load();
                ctx.PropertyModel.Load();
                ctx.ParameterModel.Load();

                assembly = DataTransferGraph.AssemblyBase(ctx.AssemblyModel.FirstOrDefault());

                if (assembly == null)
                {
                    throw new ArgumentException("Database is empty");
                }
            }
            return assembly;
        }

        public AssemblyBase Deserialize(string filePath)
        {
            throw new NotImplementedException();
        }

        public void Serialize(IFileSelector supplier, AssemblyBase target)
        {
            System.Data.Entity.Database.SetInitializer(new DropCreateDatabaseAlways<DatabaseContext>());
            DatabaseAssembly serializationModel = new DatabaseAssembly(target);
            using (var ctx = new DatabaseContext())
            {
                ctx.AssemblyModel.Add(serializationModel);
                ctx.SaveChanges();
            }
        }

        public void Serialize(string filePath, AssemblyBase target)
        {
            throw new NotImplementedException();
        }
    }
}
