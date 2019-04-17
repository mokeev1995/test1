using System.Collections.Generic;
using System.Linq;
using ConfirmitTest;
using ConfirmitTest.Products;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    public static class TestProducts
    {
        public static readonly IReadOnlyList<IProduct> Cars = new List<IProduct>
        {
            new Car(1, "BMW", "X6M", 125_000),
            new Car(1, "BMW", "X5", 80_000)
        };
    }

    [TestClass]
    public class CartManagerTests
    {
        [TestMethod]
        public void AddProducts()
        {
            var m = new CartManager(null, null);
            m.AddProduct(TestProducts.Cars.First());
            m.AddProduct(TestProducts.Cars.First());
            m.AddDiscount("SOME_PRODUCT_DISCOUNT");
            m.AddProduct(TestProducts.Cars.Skip(1).First());

            m.PrintReceipt();
        }
    }
}