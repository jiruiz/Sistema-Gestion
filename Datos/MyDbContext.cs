using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class MyDbContext : DbContext
    {
        public MyDbContext() : base("name=AdministracionEntities")
        {
            
            //Database.SetInitializer<MyDbContext>(new CreateDatabaseIfNotExists<MyDbContext>());

        }

        public DbSet<Agencia> Agencia { get; set; }
        public DbSet<Deposito> Deposito { get; set; }
        

    }
}
