﻿using Database.Model;
using System;
using System.ComponentModel.Composition;
using System.Data.Entity;
using System.Linq;
using Core.Model;

using Interfaces;

namespace Database
{
    [Export(typeof(ISerializer<AssemblyBase>))]
    public class DBSerializer : ISerializer<AssemblyBase>
    {
        public DatabaseAssembly Load(DatabaseContext ctx)
        {
            DatabaseAssembly assembly = ctx.AssemblyModel.FirstOrDefault();

            if (assembly == null)
            {
                throw new ArgumentException("Database is empty");
            }
            return assembly;
        }

        public AssemblyBase Deserialize(IFileSelector supplier)
        {
            using (var ctx = new DatabaseContext())
            {
                ctx.AssemblyModel.Load();
                ctx.NamespaceModel.Load();
                ctx.TypeModel.Load();
                ctx.MethodModel.Load();
                ctx.FieldModel.Load();
                ctx.PropertyModel.Load();
                ctx.ParameterModel.Load();

                return DataTransferGraph.AssemblyBase(Load(ctx));
            }
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
