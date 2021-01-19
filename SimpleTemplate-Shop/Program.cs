using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using SimpleTemplate_Shop.Models;
using Microsoft.EntityFrameworkCore;

namespace SimpleTemplate_Shop
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var webHost = BuildWebHost(args); //�������� ����� BuildWebHost ��������� ����.

            using (var scope = webHost.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetService<ApplicationDbContext>();
                context.Database.Migrate();
            }

            webHost.Run();
        }

        public static IWebHost BuildWebHost(string[] args) => //BuildWebHost � ����� �������� �������������� ����������.
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>() //������������� ���-������� ��� ������������� ���������� ������� UseStartup<Startup>() � ������� ���������� ��������� ����� ���������� Startup.
                .UseDefaultServiceProvider(webBuilder =>
                    webBuilder.ValidateScopes = false).Build();
    }
}
