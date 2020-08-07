using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EstoqueApi.Repository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace EstoqueApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            GeraArquivoSqlite();
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        public static void GeraArquivoSqlite()
        {
            using (var context = new SqLiteDbContext())
            {
                //Cria arquivo Sqlite quando nao houver
                context.Database.EnsureCreated();
            }
        }
    }
}
