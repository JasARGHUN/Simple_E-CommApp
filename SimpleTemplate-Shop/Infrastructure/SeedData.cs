using System;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using SimpleTemplate_Shop.Models;

namespace SimpleTemplate_Shop.Infrastructure
{
    public static class SeedData
    {
        public static void EnsurePopulated(IServiceProvider services)
        {
            ApplicationDbContext context = services.GetRequiredService<ApplicationDbContext>();
            if (!context.Products.Any())
            {
                context.AddRangeAsync(
                    new Product
                    {
                        Name = "Fugiat",
                        Category = new Category() { Name ="Category-1" },
                        ProductDescription = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. " +
                        "Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit" +
                        " in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt" +
                        " mollit anim id est laborum.",
                        ProductPrice = 50,
                        Manufacturer = "Manufacturer-1",
                        DateOfManufacture = "20.12.2020",
                        QuantityInStock = 2,
                        Type = "Midle Tower",
                        Processor = "i5-10400f",
                        RAM = "16",
                        PowerSupply = "650",
                        StorageDevice = "480 SSD",
                        VideoCard = "8",
                        OperatingSystem = "Windows 10 PRO"
                    },
                    new Product
                    {
                        Name = "Est",
                        Category = new Category() { Name = "Category-2" },
                        ProductDescription = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. " +
                        "Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit" +
                        " in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt" +
                        " mollit anim id est laborum.",
                        ProductPrice = 28,
                        Manufacturer = "Manufacturer-2",
                        DateOfManufacture = "05.12.2020",
                        QuantityInStock = 5,
                        Type = "Midle Tower",
                        Processor = "i5-10400f",
                        RAM = "16",
                        PowerSupply = "650",
                        StorageDevice = "480 SSD",
                        VideoCard = "8",
                        OperatingSystem = "Windows 10 PRO"
                    },
                    new Product
                    {
                        Name = "Laborum",
                        Category = new Category() { Name = "Category-3" },
                        ProductDescription = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. " +
                        "Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit" +
                        " in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt" +
                        " mollit anim id est laborum.",
                        ProductPrice = 73,
                        Manufacturer = "Manufacturer-3",
                        DateOfManufacture = "15.12.2020",
                        QuantityInStock = 2,
                        Type = "Midle Tower",
                        Processor = "i5-10400f",
                        RAM = "16",
                        PowerSupply = "650",
                        StorageDevice = "480 SSD",
                        VideoCard = "8",
                        OperatingSystem = "Windows 10 PRO"
                    },
                    new Product
                    {
                        Name = "Velit",
                        Category = new Category() { Name = "Category-4" },
                        ProductDescription = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. " +
                        "Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit" +
                        " in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt" +
                        " mollit anim id est laborum.",
                        ProductPrice = 25,
                        Manufacturer = "Manufacturer-4",
                        DateOfManufacture = "29.12.2020",
                        QuantityInStock = 1,
                        Type = "Midle Tower",
                        Processor = "i5-10400f",
                        RAM = "16",
                        PowerSupply = "650",
                        StorageDevice = "480 SSD",
                        VideoCard = "8",
                        OperatingSystem = "Windows 10 PRO"
                    },
                    new Product
                    {
                        Name = "Cillum",
                        Category = new Category() { Name = "Category-5" },
                        ProductDescription = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. " +
                        "Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit" +
                        " in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt" +
                        " mollit anim id est laborum.",
                        ProductPrice = 17,
                        Manufacturer = "Manufacturer-5",
                        DateOfManufacture = "08.12.2020",
                        QuantityInStock = 5,
                        Type = "Midle Tower",
                        Processor = "i5-10400f",
                        RAM = "16",
                        PowerSupply = "650",
                        StorageDevice = "480 SSD",
                        VideoCard = "8",
                        OperatingSystem = "Windows 10 PRO"
                    },
                    new Product
                    {
                        Name = "Dolor",
                        Category = new Category() { Name = "Category-6" },
                        ProductDescription = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. " +
                        "Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit" +
                        " in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt" +
                        " mollit anim id est laborum.",
                        ProductPrice = 52,
                        Manufacturer = "Manufacturer-6",
                        DateOfManufacture = "17.09.2020",
                        QuantityInStock = 0,
                        Type = "Midle Tower",
                        Processor = "i5-10400f",
                        RAM = "16",
                        PowerSupply = "650",
                        StorageDevice = "480 SSD",
                        VideoCard = "8",
                        OperatingSystem = "Windows 10 PRO"
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
