﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text.Json;

namespace C__Final
{
    internal class Program
    {

        static bool SignInAdmin()
        {
            Console.Write("Enter username: ");
            var username = Console.ReadLine();
            Console.Write("Enter password: ");
            var password = Console.ReadLine();

            if (username == "admin" && password == "admin")
            {
                return true;
            }
            else
            {
                Console.WriteLine("Your username or password is wrong");
                return false;
            }
        }

        static void SaveStock(Stock stock)
        {
            string jsonProducts = JsonSerializer.Serialize(stock);
            File.WriteAllText("stock.json", jsonProducts);
        }

        static void InitializeDefaultProducts(Stock stock)
        {
            Product product1 = new Product("Alma", 1.5, 10);
            Product product2 = new Product("Armud", 2, 15);
            Product product3 = new Product("Cola", 1, 20);
            Product product4 = new Product("Dogranmis Corek", 1, 25);

            stock.Products.Add(product1);
            stock.Products.Add(product2);
            stock.Products.Add(product3);
            stock.Products.Add(product4);

            Category category1 = new Category("Meyve Terevez");
            Category category2 = new Category("Icki");
            Category category3 = new Category("Un Memulatlari");

            category1.AddProduct(product1);
            category1.AddProduct(product2);
            category2.AddProduct(product3);
            category3.AddProduct(product4);

            stock.Categories.Add(category1);
            stock.Categories.Add(category2);
            stock.Categories.Add(category3);

            SaveStock(stock);
        }

        static void Main(string[] args)
        {
            Stock stock;
            if (File.Exists("stock.json"))
            {
                string stockJson = File.ReadAllText("stock.json");
                stock = JsonSerializer.Deserialize<Stock>(stockJson);
            }
            else
            {
                stock = new Stock();
            }


            if (stock.Products.Count == 0 && stock.Categories.Count == 0)
            {
                InitializeDefaultProducts(stock);
            }

            string jsonProducts = JsonSerializer.Serialize(stock);
            File.WriteAllText("stock.json", jsonProducts);



            bool LoggedIn = false;
            while (true)
            {
                if (SignInAdmin())
                {
                    LoggedIn = true;
                    break;
                }
            }

            bool inMenu = true;
            Console.Clear();
            while (inMenu)
            {
                if (LoggedIn)
                {
                    Console.WriteLine("Welcome!!!");
                    Console.WriteLine("1)Stock");
                    Console.WriteLine("2)Kategoriyalar");
                    Console.WriteLine("3)Statistika");
                    Console.WriteLine("4)Cixis");
                    Console.Write("Secim edin: ");
                    var secim = Console.ReadLine();
                    switch (secim)
                    {

                        case "1":
                            #region Stock
                            Console.Clear();
                            foreach (var product in stock.Products)
                            {
                                Console.WriteLine($"Name: {product.Name}  Price: {product.Price} azn   Count: {product.Count}");
                            }
                            try
                            {

                                Console.WriteLine("1)Produkt elave et");
                                Console.WriteLine("2)Produkt melumatlarini deyis");
                                Console.WriteLine("3)Evvelki menu");
                                Console.Write("Secim edin: ");
                                var secimStock = Console.ReadLine();

                                if (Convert.ToInt32(secimStock) == 1)
                                {
                                    Console.Clear();
                                    try
                                    {
                                        Console.Write("Enter category of product: ");
                                        var categoryOfProduct = Console.ReadLine();
                                        Console.Write("Enter name of product: ");
                                        var nameOfProduct = Console.ReadLine();
                                        Console.Write("Enter price of product: ");
                                        var priceOfProduct = Console.ReadLine();
                                        Console.Write("Enter count of product: ");
                                        var countOfProduct = Console.ReadLine();
                                        Category category = stock.GetCategoryByName(categoryOfProduct);
                                        if (category != null)
                                        {
                                            stock.AddProductToCategory(categoryOfProduct, nameOfProduct, Convert.ToDouble(priceOfProduct, CultureInfo.InvariantCulture), Convert.ToInt32(countOfProduct));
                                            SaveStock(stock);
                                            Console.WriteLine("Produkt stoka elave olundu");
                                        }
                                        else
                                        {
                                            Console.WriteLine("Kategoriya tapilmadi");
                                        }

                                        Thread.Sleep(1250);
                                        Console.Clear();
                                    }
                                    catch (Exception)
                                    {
                                        Console.WriteLine("Ad ve ya qiymet bos ola bilmez");
                                        Thread.Sleep(2000);
                                        Console.Clear();
                                    }
                                }

                                else if (Convert.ToInt32(secimStock) == 2)
                                {

                                    Console.Clear();
                                    Console.WriteLine("Price or Count or both?");
                                    Console.WriteLine("1)Price");
                                    Console.WriteLine("2)Count");
                                    Console.WriteLine("3)Both");
                                    Console.Write("Secim edin: ");
                                    var secimUptade = Console.ReadLine();
                                    if (Convert.ToInt32(secimUptade) == 1)
                                    {
                                        Console.WriteLine("Enter name of product for searching in the stock: ");
                                        var nameForSearch = Console.ReadLine();
                                        Console.WriteLine("Enter new price: ");
                                        var newPrice = Console.ReadLine();
                                        var productToUpdate = stock.Products.Find(p => p.Name == nameForSearch);
                                        if (productToUpdate != null)
                                        {
                                            productToUpdate.Price = Convert.ToDouble(newPrice, CultureInfo.InvariantCulture);
                                            SaveStock(stock);
                                            Console.WriteLine("Price updated");
                                            Thread.Sleep(1250);
                                            Console.Clear();
                                        }
                                        else
                                        {
                                            Console.WriteLine("Product not finded!!!");
                                            Thread.Sleep(1250);
                                            Console.Clear();
                                        }
                                    }
                                    else if (Convert.ToInt32(secimUptade) == 2)
                                    {
                                        Console.WriteLine("Enter name of product for searching in the stock: ");
                                        var nameForSearch = Console.ReadLine();
                                        Console.WriteLine("Enter new count: ");
                                        var newCount = Console.ReadLine();
                                        var productToUpdate = stock.Products.Find(p => p.Name == nameForSearch);
                                        if (productToUpdate != null)
                                        {
                                            productToUpdate.Count = Convert.ToInt32(newCount);
                                            SaveStock(stock);
                                            Console.WriteLine("Count updated");
                                            Thread.Sleep(1250);
                                            Console.Clear();
                                        }
                                        else
                                        {
                                            Console.WriteLine("Product not finded!!!");
                                            Thread.Sleep(1250);
                                            Console.Clear();
                                        }
                                    }
                                    else if (Convert.ToInt32(secimUptade) == 3)
                                    {
                                        Console.WriteLine("Enter name of product for searching in the stock: ");
                                        var nameForSearch = Console.ReadLine();
                                        Console.WriteLine("Enter new price: ");
                                        var newPrice = Console.ReadLine();
                                        Console.WriteLine("Enter new count: ");
                                        var newCount = Console.ReadLine();
                                        var productToUpdate = stock.Products.Find(p => p.Name == nameForSearch);
                                        if (productToUpdate != null)
                                        {
                                            productToUpdate.Price = Convert.ToDouble(newPrice, CultureInfo.InvariantCulture);
                                            productToUpdate.Count = Convert.ToInt32(newCount);
                                            SaveStock(stock);
                                            Console.WriteLine("Price and Count updated");
                                            Thread.Sleep(1250);
                                            Console.Clear();
                                        }
                                        else
                                        {
                                            Console.WriteLine("Product not finded!!!");
                                            Thread.Sleep(1250);
                                            Console.Clear();
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("Wrong input!!!");
                                        Thread.Sleep(1250);
                                        Console.Clear();
                                    }
                                }
                                else if (Convert.ToInt32(secimStock) == 3)
                                {
                                    Console.Clear();
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("Wrong input!!!");
                                    Thread.Sleep(1250);
                                    Console.Clear();

                                }
                            }
                            catch (Exception)
                            {
                                Console.WriteLine("Secim bos ola bilmez");
                                Thread.Sleep(1500);
                                Console.Clear();
                            }
                            break;
                        #endregion
                        case "2":
                            #region Kategoriya              
                            Console.Clear();
                            for (int i = 0; i < stock.Categories.Count; i++)
                            {
                                Console.WriteLine($"{i + 1}) {stock.Categories[i].Name}");
                            }
                            Console.WriteLine();
                            Console.WriteLine("+)Kategoriya elave edin");
                            Console.WriteLine("-)Evvelki menu");
                            Console.Write("Secim edin: ");
                            var secimKategoriya = Console.ReadLine();
                            if (secimKategoriya == "+")
                            {
                                try
                                {
                                    Console.Write("Elave etmek istediyiniz kategoriyanin adini yazin: ");
                                    var newCategory = Console.ReadLine();
                                    stock.AddCategory(newCategory);

                                    Console.Write("Elave etmek istediyiniz produktlarin sayini girin: ");
                                    var sayProdukt = Console.ReadLine();
                                    for (int i = 0; i < Convert.ToInt32(sayProdukt); i++)
                                    {
                                        Console.Write($"{i + 1}-ci produktun adini yazin: ");
                                        var newProduktName = Console.ReadLine();
                                        Console.Write($"{i + 1}-ci produktun qiymetini yazin: ");
                                        var newProduktPrice = Console.ReadLine();
                                        Console.Write($"{i + 1}-ci produktun sayini yazin: ");
                                        var newProduktCount = Console.ReadLine();
                                        stock.AddProductToCategory(newCategory, newProduktName, Convert.ToDouble(newProduktPrice, CultureInfo.InvariantCulture), Convert.ToInt32(newProduktCount));


                                    }
                                    SaveStock(stock);
                                    Console.WriteLine("Elave olundu");
                                    Thread.Sleep(1250);
                                    Console.Clear();
                                }
                                catch (Exception)
                                {
                                    Console.WriteLine("Input bos ola bilmez");
                                    Thread.Sleep(1250);
                                    Console.Clear();
                                }
                            }
                            else if (secimKategoriya == "-")
                            {
                                Console.Clear();
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Wrong input!!!");
                                Thread.Sleep(1250);
                                Console.Clear();
                            }
                            break;
                        #endregion
                        case "3":
                            #region Stat              
                            Console.Clear();
                            Console.WriteLine($"Product count: {stock.Products.Count}");
                            Console.WriteLine($"Category count: {stock.Categories.Count}");
                            foreach (var product in stock.Products)
                            {
                                Console.WriteLine($"Product: {product.Name}, Price: {product.Price}, Count: {product.Count}");
                            }
                            Console.WriteLine();
                            Console.Write("Evvelki menu(-): ");
                            var statMenu = Console.ReadLine();
                            if (statMenu == "-")
                            {
                                Console.Clear();
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Wrong input!!!");
                                Thread.Sleep(1250);
                                Console.Clear();
                            }
                            break;
                        #endregion
                        case "4":
                            inMenu = false;
                            break;
                        default:
                            Console.WriteLine("Wrong input!!!");
                            Thread.Sleep(1000);
                            Console.Clear();
                            break;
                    }
                }
            }
        }
    }
}
