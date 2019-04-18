using System;
using System.Globalization;

namespace ConfirmitTest
{
    public class ConsoleReceiptPrinter : IReceiptPrinter
    {
        public void Print(ICartState cart)
        {
            Console.WriteLine();
            Console.WriteLine(new string('-', 100));

            foreach (var product in cart.Products)
            {
                var itemPrice = product.Key.Price;
                if (cart.ProductDiscounts.ContainsKey(product.Key))
                {
                    var discount = cart.ProductDiscounts[product.Key];
                    itemPrice = discount.Make(product.Key);
                }

                var totalPrice = itemPrice * product.Value;
                Console.WriteLine($"{product.Key}\n\tCount: {product.Value}\t Price: {product.Key.Price} \t Total: {totalPrice}");
            }

            Console.WriteLine(new string('-', 25));
            Console.WriteLine($"Total: {cart.Total.ToString("C", CultureInfo.GetCultureInfo("en-us"))}");

            Console.WriteLine(new string('-', 100));
            Console.WriteLine();
        }
    }
}