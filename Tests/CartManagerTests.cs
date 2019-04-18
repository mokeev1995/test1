using System.Collections.Generic;
using System.Linq;
using ConfirmitTest;
using ConfirmitTest.Products;
using Xunit;

namespace Tests
{
    public static class TestProducts
    {
        public static readonly IReadOnlyList<IProduct> Cars = new List<IProduct>
        {
            new Car(1, "BMW", "X6M", 125_000),
            new Car(2, "BMW", "X4", 80_000),
            new Car(3, "BMW", "X3", 70_000),
            new Car(4, "BMW", "X2", 60_000),
            new Car(5, "BMW", "X1", 40_000),
            new Car(6, "BMW", "X7", 130_000),
        };
    }

    public class CartManagerTests
    {
        [Fact]
        public void AddProducts()
        {
            var m = new CartManager(new Cart(), new ConsoleReceiptPrinter(), new DiscountsProvider());
            m.AddProduct(TestProducts.Cars.First());
            m.AddProduct(TestProducts.Cars.First());
            m.AddDiscount("SOME_PRODUCT_DISCOUNT");
            m.AddProduct(TestProducts.Cars.Skip(1).First());

            m.PrintReceipt();
        }
    }
}