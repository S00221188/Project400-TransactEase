namespace Project400_TransactEase.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using BCrypt.Net; //Hashed Passwords
    using System.Linq;
    using Project400_TransactEase.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<Project400_TransactEase.Models.AppDBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Project400_TransactEase.Models.AppDBContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.

            //Seeding Admin employee
            context.Employees.AddOrUpdate(e => e.Username,
                new Models.Employee
                {
                    Username = "admin",
                    Password = BCrypt.HashPassword("314159"), //Password changed now to Pin code
                    FirstName = "Malachy",
                    LastName = "Sharkey",
                    Role = "Admin"
                });
            context.Employees.AddOrUpdate(e => e.Username,
               new Models.Employee
               {
                   Username = "employee",
                   Password = BCrypt.HashPassword("1234"), //Password changed now to Pin code
                   FirstName = "Malachy",
                   LastName = "Sharkey",
                   Role = "Employee"
               });

            //Seeding Initial Products
            context.Products.AddOrUpdate(p => p.ProductName,
        // DRAFT PINTS - ChatGPT Products generated
        new Product { ProductName = "Guinness Pint", ProductPrice = 6.00m, ProductType = "Draft", ProductStockCount = 50, VATRate= 0.23m },
        new Product { ProductName = "Heineken Pint", ProductPrice = 6.00m, ProductType = "Draft", ProductStockCount = 50,VATRate = 0.23m },
        new Product { ProductName = "Carlsberg Pint", ProductPrice = 5.80m, ProductType = "Draft", ProductStockCount = 40, VATRate = 0.23m },
        new Product { ProductName = "Coors Pint", ProductPrice = 5.90m, ProductType = "Draft", ProductStockCount = 45, VATRate = 0.23m },
        new Product { ProductName = "Rockshore Pint", ProductPrice = 5.70m, ProductType = "Draft", ProductStockCount = 40, VATRate = 0.23m },
        new Product { ProductName = "Hop House 13 Pint", ProductPrice = 6.10m, ProductType = "Draft", ProductStockCount = 35, VATRate = 0.23m },
        new Product { ProductName = "Smithwick’s Pint", ProductPrice = 5.80m, ProductType = "Draft", ProductStockCount = 30, VATRate = 0.23m },
        //DRAFT HALF PINT (Will later share stock with parent Pint)
        //Will reduce stock by 0.5
        new Product { ProductName = "Guinness Half Pint", ProductPrice = 3.50m, ProductType = "Draft", ProductStockCount = 50, VATRate = 0.23m },
        new Product { ProductName = "Heineken Half Pint", ProductPrice = 3.50m, ProductType = "Draft", ProductStockCount = 50, VATRate = 0.23m },
        new Product { ProductName = "Carlsberg Half Pint", ProductPrice = 3.50m, ProductType = "Draft", ProductStockCount = 40, VATRate = 0.23m },
        new Product { ProductName = "Coors Half Pint", ProductPrice = 3.50m, ProductType = "Draft", ProductStockCount = 45, VATRate = 0.23m },
        new Product { ProductName = "Rockshore Half Pint", ProductPrice = 3.50m, ProductType = "Draft", ProductStockCount = 40, VATRate = 0.23m },
        new Product { ProductName = "Hop House 13 Half Pint", ProductPrice = 3.50m, ProductType = "Draft", ProductStockCount = 35, VATRate = 0.23m },
        new Product { ProductName = "Smithwick’s Half Pint", ProductPrice = 3.50m, ProductType = "Draft", ProductStockCount = 30, VATRate = 0.23m },
        // BOTTLES
        new Product { ProductName = "Corona Bottle", ProductPrice = 5.50m, ProductType = "Bottle", ProductStockCount = 30, VATRate = 0.23m },
        new Product { ProductName = "Budweiser Bottle", ProductPrice = 5.00m, ProductType = "Bottle", ProductStockCount = 35, VATRate = 0.23m },
        new Product { ProductName = "Bulmers Pint Bottle", ProductPrice = 6.20m, ProductType = "Bottle", ProductStockCount = 25, VATRate = 0.23m },
        new Product { ProductName = "Rockshore Cider Bottle", ProductPrice = 6.00m, ProductType = "Bottle", ProductStockCount = 25, VATRate = 0.23m },
        new Product { ProductName = "Smirnoff Ice", ProductPrice = 5.00m, ProductType = "Bottle", ProductStockCount = 20, VATRate = 0.23m },
        new Product { ProductName = "Wkd Blue", ProductPrice = 4.50m, ProductType = "Bottle", ProductStockCount = 20, VATRate = 0.23m },

        // SHOTS (When Stock adding is added later, it will add 28 shots per litre (1 shot is 35.5ml out of 1000ml/1L bottle)
        new Product { ProductName = "Jameson Shot", ProductPrice = 5.00m, ProductType = "Shot", ProductStockCount = 60, VATRate = 0.23m },
        new Product { ProductName = "Jägermeister Shot", ProductPrice = 5.50m, ProductType = "Shot", ProductStockCount = 50, VATRate = 0.23m },
        new Product { ProductName = "Tequila Shot", ProductPrice = 6.00m, ProductType = "Shot", ProductStockCount = 40, VATRate = 0.23m },
        new Product { ProductName = "Sambuca Shot", ProductPrice = 5.50m, ProductType = "Shot", ProductStockCount = 45, VATRate = 0.23m },
        new Product { ProductName = "Vodka Shot", ProductPrice = 4.80m, ProductType = "Shot", ProductStockCount = 55, VATRate = 0.23m },
        new Product { ProductName = "Baby Guinness", ProductPrice = 6.50m, ProductType = "Shot", ProductStockCount = 30, VATRate = 0.23m },

        // COCKTAILS (Will decide logic for Stock count later i.e. Amount of Liquor + other ingredients used)
        new Product { ProductName = "Mojito", ProductPrice = 12.00m, ProductType = "Cocktail", ProductStockCount = 25, VATRate = 0.23m },
        new Product { ProductName = "Espresso Martini", ProductPrice = 12.00m, ProductType = "Cocktail", ProductStockCount = 20, VATRate = 0.23m },
        new Product { ProductName = "Pornstar Martini", ProductPrice = 12.00m, ProductType = "Cocktail", ProductStockCount = 15, VATRate = 0.23m },
        new Product { ProductName = "Whiskey Sour", ProductPrice = 12.00m, ProductType = "Cocktail", ProductStockCount = 20, VATRate = 0.23m },
        new Product { ProductName = "Old Fashioned", ProductPrice = 12.00m, ProductType = "Cocktail", ProductStockCount = 10, VATRate = 0.23m },
        new Product { ProductName = "Cosmopolitan", ProductPrice = 12.00m, ProductType = "Cocktail", ProductStockCount = 20, VATRate = 0.23m },
        new Product { ProductName = "Long Island Iced Tea", ProductPrice = 15.00m, ProductType = "Cocktail", ProductStockCount = 20, VATRate = 0.23m },

        // NON-ALCOHOLIC
        new Product { ProductName = "Coke", ProductPrice = 2.50m, ProductType = "Soft Drink", ProductStockCount = 80, VATRate = 0.23m },
        new Product { ProductName = "Diet Coke", ProductPrice = 2.50m, ProductType = "Soft Drink", ProductStockCount = 70, VATRate = 0.23m },
        new Product { ProductName = "7Up", ProductPrice = 2.50m, ProductType = "Soft Drink", ProductStockCount = 60, VATRate = 0.23m },
        new Product { ProductName = "Fanta", ProductPrice = 2.50m, ProductType = "Soft Drink", ProductStockCount = 60, VATRate = 0.23m },
        new Product { ProductName = "Sparkling Water", ProductPrice = 2.00m, ProductType = "Soft Drink", ProductStockCount = 60, VATRate = 0.23m },
        new Product { ProductName = "Still Water", ProductPrice = 2.00m, ProductType = "Soft Drink", ProductStockCount = 50, VATRate = 0.23m },
        new Product { ProductName = "Orange Juice", ProductPrice = 3.00m, ProductType = "Soft Drink", ProductStockCount = 40, VATRate = 0.23m },
        new Product { ProductName = "Red Bull", ProductPrice = 3.50m, ProductType = "Soft Drink", ProductStockCount = 30, VATRate = 0.23m },
        new Product { ProductName = "Apple Juice", ProductPrice = 3.00m, ProductType = "Soft Drink", ProductStockCount = 40, VATRate = 0.23m },

        // SNACKS
        new Product { ProductName = "Tayto Cheese & Onion", ProductPrice = 1.80m, ProductType = "Snack", ProductStockCount = 100, VATRate = 0.23m },
        new Product { ProductName = "King Crisps", ProductPrice = 1.80m, ProductType = "Snack", ProductStockCount = 100, VATRate = 0.23m },
        new Product { ProductName = "Peanuts", ProductPrice = 2.00m, ProductType = "Snack", ProductStockCount = 80, VATRate = 0.23m }

        // VAT Rate are the following -- To be changed January 2026, government budget VAT reductions
        // Draft, Bottle, Shot, Cocktail, Soft Drink, Snack = 0.23m
        // If hot food is added in future updates = 0.135m

        );
        }
    }
}
