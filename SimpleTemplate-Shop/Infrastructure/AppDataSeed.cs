using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SimpleTemplate_Shop.Models;
using System.Linq;

namespace SimpleTemplate_Shop.Infrastructure
{
    public static class AppDataSeed
    {
        public static void SeedData(IApplicationBuilder app)
        {
            ApplicationDbContext context = app.ApplicationServices.GetRequiredService<ApplicationDbContext>();
            context.Database.Migrate();
            if (!context.Infos.Any())
            {
                context.Infos.Add(
                    new Info
                    {
                        AppName = "Application Name",
                        AppHomeImageText = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",
                        AppHomeImageTextFirst = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",
                        AppHomeImageTextSecond = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua."
                    });
                context.SaveChanges();
            }
        }
    }
}
