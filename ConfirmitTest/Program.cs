using System;
using System.Collections.Generic;
using ConfirmitTest.Products;

namespace ConfirmitTest
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            var car1 = new Car(1, "BMW", "X6M", 125_000);
            var car2 = new Car(2, "BMW", "X4", 80_000);
            
            var cartManager = GetCartManager();
            Console.WriteLine("Added 2 cars");
            cartManager.AddProduct(car1, 2);
            cartManager.AddProduct(car2);
            cartManager.PrintReceipt();

            Console.WriteLine("Added 2 discounts");
            cartManager.AddDiscount("SOME_CART_DISCOUNT");
            cartManager.AddDiscount("SOME_PRODUCT_DISCOUNT");
            cartManager.PrintReceipt();

            Console.WriteLine("Removed 1 car");
            cartManager.RemoveProduct(car1);
            cartManager.PrintReceipt();

            Console.WriteLine("Cancelled remove car");
            cartManager.Undo();
            cartManager.PrintReceipt();

            Console.WriteLine("Cancelled add product discount");
            cartManager.Undo();
            cartManager.PrintReceipt();

            Console.WriteLine("Redo add product discount");
            cartManager.Redo();
            cartManager.PrintReceipt();
        }

        public static Dictionary<IProduct, int> GetAvailableProducts()
        {
            return new Dictionary<IProduct, int>
            {
                {new Car(1, "BMW", "X6M", 125_000), 10},
                {new Car(2, "BMW", "X4", 80_000), 20},
                {new Car(3, "BMW", "X3", 70_000), 22},
                {new Car(4, "BMW", "X2", 60_000), 12},
                {new Car(5, "BMW", "X1", 40_000), 7},
                {new Car(6, "BMW", "X7", 130_000), 2},
            };
        }

        public static ICartManager GetCartManager()
        {
            return new CartManager(new Cart(), new ConsoleReceiptPrinter());
        }
    }
}