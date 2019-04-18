using System.Linq;
using ConfirmitTest;
using Xunit;

namespace Tests
{
    class TestReceiptPrinter : IReceiptPrinter
    {
        public ICartState CartState { get; private set; }
        
        public void Print(ICartState cart)
        {
            CartState = cart;
        }
    }

    public class CartManagerTests
    {
        [Fact]
        public void AddProducts()
        {
            var rp = new TestReceiptPrinter();
            var m = new CartManager(new Cart(), rp, new DiscountsProvider());
            var c1 = TestProducts.Cars.First();
            var c2 = TestProducts.Cars.Skip(1).First();
            m.AddProduct(c1);
            m.AddProduct(c1);
            m.AddProduct(c2);

            m.PrintReceipt();
            var state = rp.CartState;
            Assert.Equal(2, state.Products.Count);
            Assert.Equal(2, state.Products[c1]);
        }

        [Fact]
        public void AddDiscounts()
        {
            var rp = new TestReceiptPrinter();
            var dp = new DiscountsProvider();
            var m = new CartManager(new Cart(), rp, dp);
            
            var productDiscount = dp.GetProductsDiscounts().First();
            var cartDiscount = dp.GetCartDiscounts().First();
            
            m.AddDiscount(productDiscount.Key);
            m.AddDiscount(cartDiscount.Key);
            
            var c1 = TestProducts.Cars.First();
            var c2 = TestProducts.Cars.Skip(1).First();
            m.AddProduct(c1);
            m.AddProduct(c2);
            
            m.PrintReceipt();
            var state = rp.CartState;
            
            Assert.Equal(1, state.ProductDiscounts.Count);
            Assert.Equal(c1, state.ProductDiscounts.First().Key);
            Assert.Equal(productDiscount.Value.Value, state.ProductDiscounts.First().Value.PercentValue);
            
            Assert.Equal(1, state.CartDiscounts.Count);
            Assert.Equal(cartDiscount.Value, state.CartDiscounts.First().PercentValue);
        }
    }
}