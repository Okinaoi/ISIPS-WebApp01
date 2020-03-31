using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ISIPS_WebApp01
{
    // C'est ici que notre webapp démarre , c'est la fonctin Main ou le point d'entrée de notre projet,
    // nous ne modifions rien ici, c'est un fichier standard auto généré à la creation du projet,
    // il va lancer le server IIS et lire tout nos fichiers et creer tout l'environnement pour nous grace a la classe Startup 
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
