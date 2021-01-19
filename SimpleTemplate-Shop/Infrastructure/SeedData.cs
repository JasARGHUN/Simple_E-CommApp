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
                        Category = "Category-1",
                        ProductDescription = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. " +
                        "Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit" +
                        " in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt" +
                        " mollit anim id est laborum.",
                        ProductPrice = 50,
                        Manufacturer = "Manufacturer-1",
                        DateOfManufacture = "20.12.2020",
                        QuantityInStock = 2
                    },
                    new Product
                    {
                        Name = "Est",
                        Category = "Category-1",
                        ProductDescription = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. " +
                        "Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit" +
                        " in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt" +
                        " mollit anim id est laborum.",
                        ProductPrice = 28,
                        Manufacturer = "Manufacturer-2",
                        DateOfManufacture = "05.12.2020",
                        QuantityInStock = 5
                    },
                    new Product
                    {
                        Name = "Laborum",
                        Category = "Category-2",
                        ProductDescription = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. " +
                        "Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit" +
                        " in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt" +
                        " mollit anim id est laborum.",
                        ProductPrice = 73,
                        Manufacturer = "Manufacturer-3",
                        DateOfManufacture = "15.12.2020",
                        QuantityInStock = 0
                    },
                    new Product
                    {
                        Name = "Velit",
                        Category = "Category-2",
                        ProductDescription = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. " +
                        "Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit" +
                        " in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt" +
                        " mollit anim id est laborum.",
                        ProductPrice = 25,
                        Manufacturer = "Manufacturer-4",
                        DateOfManufacture = "29.12.2020",
                        QuantityInStock = 0
                    },
                    new Product
                    {
                        Name = "Cillum",
                        Category = "Category-3",
                        ProductDescription = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. " +
                        "Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit" +
                        " in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt" +
                        " mollit anim id est laborum.",
                        ProductPrice = 17,
                        Manufacturer = "Manufacturer-5",
                        DateOfManufacture = "08.12.2020",
                        QuantityInStock = 0
                    },
                    new Product
                    {
                        Name = "Dolor",
                        Category = "Category-3",
                        ProductDescription = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. " +
                        "Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit" +
                        " in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt" +
                        " mollit anim id est laborum.",
                        ProductPrice = 52,
                        Manufacturer = "Manufacturer-6",
                        DateOfManufacture = "17.09.2020",
                        QuantityInStock = 0
                    },
                    new Product
                    {
                        Name = "Amet",
                        Category = "Category-4",
                        ProductDescription = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. " +
                        "Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit" +
                        " in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt" +
                        " mollit anim id est laborum.",
                        ProductPrice = 23,
                        Manufacturer = "Manufacturer-7",
                        DateOfManufacture = "22.03.2020",
                        QuantityInStock = 0
                    },
                    new Product
                    {
                        Name = "Nostrud",
                        Category = "Category-4",
                        ProductDescription = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. " +
                        "Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit" +
                        " in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt" +
                        " mollit anim id est laborum.",
                        ProductPrice = 75,
                        Manufacturer = "Manufacturer-10",
                        DateOfManufacture = "27.07.2020",
                        QuantityInStock = 0
                    },
                    new Product
                    {
                        Name = "Quis",
                        Category = "Category-5",
                        ProductDescription = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. " +
                        "Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit" +
                        " in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt" +
                        " mollit anim id est laborum.",
                        ProductPrice = 156,
                        Manufacturer = "Manufacturer-8",
                        DateOfManufacture = "11.01.2020",
                        QuantityInStock = 0
                    },
                    new Product
                    {
                        Name = "Cupidatat",
                        Category = "Category-5",
                        ProductDescription = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. " +
                        "Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit" +
                        " in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt" +
                        " mollit anim id est laborum.",
                        ProductPrice = 285,
                        Manufacturer = "Manufacturer-9",
                        DateOfManufacture = "23.06.2020",
                        QuantityInStock = 0
                    },
                    new Product
                    {
                        Name = "Tetur",
                        Category = "Category-1",
                        ProductDescription = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. " +
                        "Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit" +
                        " in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt" +
                        " mollit anim id est laborum.",
                        ProductPrice = 85,
                        Manufacturer = "Manufacturer-3",
                        DateOfManufacture = "13.12.2020",
                        QuantityInStock = 0
                    },
                    new Product
                    {
                        Name = "Scing",
                        Category = "Category-2",
                        ProductDescription = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. " +
                        "Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit" +
                        " in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt" +
                        " mollit anim id est laborum.",
                        ProductPrice = 185,
                        Manufacturer = "Manufacturer-4",
                        DateOfManufacture = "27.10.2020",
                        QuantityInStock = 0
                    },
                    new Product
                    {
                        Name = "Ut",
                        Category = "Category-3",
                        ProductDescription = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. " +
                        "Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit" +
                        " in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt" +
                        " mollit anim id est laborum.",
                        ProductPrice = 266,
                        Manufacturer = "Manufacturer-3",
                        DateOfManufacture = "12.01.2020",
                        QuantityInStock = 0
                    }

                );
                context.SaveChanges();
            }
        }
    }
}
