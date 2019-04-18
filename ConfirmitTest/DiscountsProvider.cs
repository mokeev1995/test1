using System.Collections.Generic;
using ConfirmitTest.Products;

namespace ConfirmitTest
{
    public class DiscountsProvider : IDiscountsProvider
    {
        public Dictionary<string, int> GetCartDiscounts()
        {
            return new Dictionary<string, int>
            {
                {"SOME_CART_DISCOUNT", 10}
            };
        }

        public Dictionary<string, (IProduct Product, int Value)> GetProductsDiscounts()
        {
            return new Dictionary<string, (IProduct Product, int Value)>
            {
                {"SOME_PRODUCT_DISCOUNT", (new Car(1, "BMW", "X6M", 125_000), 5)}
            };
        }
    }
}