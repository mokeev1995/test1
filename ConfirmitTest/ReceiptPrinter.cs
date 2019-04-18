using System;
using System.Globalization;

namespace ConfirmitTest
{
    public class ConsoleReceiptPrinter : IReceiptPrinter
    {
        public void Print(ICartState cart)
        {
            var culture = CultureInfo.GetCultureInfo("en-us");
            Console.WriteLine();
            Console.WriteLine(new string('-', 100));

            foreach (var product in cart.Products)
            {
                var itemPrice = product.Key.Price;
                if (cart.ProductDiscounts.ContainsKey(product.Key))
                {
                    var discount = cart.ProductDiscounts[product.Key];
                    itemPrice = discount.Make();
                }

                var totalPrice = (itemPrice * product.Value).ToString("C", culture);
                var itemPriceStr = product.Key.Price.ToString("C", culture);
                Console.WriteLine($"{product.Key}\n\tCount: {product.Value}\t Price: {itemPriceStr} \t Total: {totalPrice}");
            }

            Console.WriteLine(new string('-', 25));
            Console.WriteLine($"Total: {cart.Total.ToString("C", culture)}");

            Console.WriteLine(new string('-', 100));
            Console.WriteLine();
        }
    }
}