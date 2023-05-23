using GerenciadorPedidos.Context;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorPedidosTest
{
    public class DbTest
    {
        public static DbContextOptions<Context> dbContextOptions { get; }
        public Context context { get; private set; }
        static DbTest()
        {
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            if (baseDir.Contains("GerenciadorPedidosTest"))
            {
                int index = baseDir.IndexOf("GerenciadorPedidosTest");
                baseDir = baseDir.Substring(0, index);
            }
            dbContextOptions = new DbContextOptionsBuilder<Context>()
                .UseSqlite($"Data Source={baseDir}GerenciadorPedidos\\database.db")
                .Options;
        }
        public DbTest()
        {
            context = new Context(dbContextOptions);
        }

        
    }
}
