using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SimpleTemplate_Shop.Models;
using SimpleTemplate_Shop.Models.ViewModels;
using System;
using System.Linq;


namespace SimpleTemplate_Shop.Infrastructure
{
    public class DbInitializer : IDbInitializer
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DbInitializer(ApplicationDbContext context, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public void Initialize()
        {
            try
            {
                if (_context.Database.GetPendingMigrations().Count() > 0)
                {
                    _context.Database.Migrate();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            if (_context.Roles.Any(i => i.Name == SD.Role_Admin)) return;

            _roleManager.CreateAsync(new IdentityRole(SD.Role_Admin)).GetAwaiter().GetResult();
            _roleManager.CreateAsync(new IdentityRole(SD.Role_Employee)).GetAwaiter().GetResult();
            _roleManager.CreateAsync(new IdentityRole(SD.Role_Preview)).GetAwaiter().GetResult();

            _userManager.CreateAsync(new LoginModel
            {
                UserName = "admin@mail.com",
                Email = "admin@mail.com",
                EmailConfirmed = true,
                Name = "admin"
            }, "111111Admin$").GetAwaiter().GetResult();

            _userManager.CreateAsync(new LoginModel
            {
                UserName = "preview@mail.com",
                Email = "preview@mail.com",
                EmailConfirmed = true,
                Name = "preview"
            }, "111111Preview$").GetAwaiter().GetResult();

            LoginModel user = _context.LoginModels.Where(i => i.Email == "admin@mail.com").FirstOrDefault();
            LoginModel preview = _context.LoginModels.Where(i => i.Email == "preview@mail.com").FirstOrDefault();

            _userManager.AddToRoleAsync(user, SD.Role_Admin).GetAwaiter().GetResult();
            _userManager.AddToRoleAsync(user, SD.Role_Employee).GetAwaiter().GetResult();
            _userManager.AddToRoleAsync(preview, SD.Role_Preview).GetAwaiter().GetResult();
        }
    }
}
