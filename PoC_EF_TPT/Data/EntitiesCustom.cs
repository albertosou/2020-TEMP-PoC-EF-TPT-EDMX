using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PoC_EF_TPT.Data
{
    public class EntitiesCustom : Entities
    {
        //public virtual DbSet<OperationExport> OperationExport { get; set; }
        //public virtual DbSet<OperationImport> OperationImport { get; set; }
        public EntitiesCustom(string nameOrConnectionString) : base(nameOrConnectionString)
        {

        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Configurations.AddFromAssembly(this.GetType().Assembly);
            modelBuilder.Entity<Data.Operation>().HasKey(_ => _.Id).ToTable("Operation");
            modelBuilder.Entity<Data.OperationExport>().HasKey(_ => _.Id).ToTable("OperationExport")
                .Map(_=> _.MapInheritedProperties());
            modelBuilder.Entity<Data.OperationImport>().HasKey(_ => _.Id).ToTable("OperationImport")
                .Map(_=> _.MapInheritedProperties());
            modelBuilder.Entity<Data.OperationFile>().HasKey(_ => _.Id).ToTable("OperationFile");
        }
    }
    public partial class Entities
    {
        public Entities(string nameOrConnectionString) :base(nameOrConnectionString)
        {

        }
        public static Entities GetEntities()
        {
            return new EntitiesCustom("Entities_New");
        }
    }
}