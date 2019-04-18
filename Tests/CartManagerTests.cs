using System.Linq;
using ConfirmitTest;
using ConfirmitTest.Exceptions;
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

        [Fact]
        public void AddNonExistingDiscount()
        {
            var rp = new TestReceiptPrinter();
            var dp = new DiscountsProvider();
            var m = new CartManager(new Cart(), rp, dp);

            Assert.Throws<DiscountNotFoundException>(() => m.AddDiscount("NON_EXISTING_CODE"));
        }

        [Fact]
        public void RemoveSingleProduct()
        {
            var rp = new TestReceiptPrinter();
            var m = new CartManager(new Cart(), rp, new DiscountsProvider());
            var c1 = TestProducts.Cars.First();
            var c2 = TestProducts.Cars.Skip(1).First();
            m.AddProduct(c1);
            m.AddProduct(c1);
            m.AddProduct(c1);
            m.AddProduct(c2);
            
            m.RemoveProduct(c1);

            m.PrintReceipt();
            var state = rp.CartState;
            
            Assert.Equal(2, state.Products.Count);
            Assert.Equal(2, state.Products[c1]);
        }

        [Fact]
        public void RemoveMultipleProducts()
        {
            var rp = new TestReceiptPrinter();
            var m = new CartManager(new Cart(), rp, new DiscountsProvider());
            var c1 = TestProducts.Cars.First();
            var c2 = TestProducts.Cars.Skip(1).First();
            m.AddProduct(c1);
            m.AddProduct(c1);
            m.AddProduct(c1);
            m.AddProduct(c2);
            
            m.RemoveProduct(c1, 2);

            m.PrintReceipt();
            var state = rp.CartState;
            
            Assert.Equal(2, state.Products.Count);
            Assert.Equal(1, state.Products[c1]);
        }

        [Fact]
        public void RemoveAllProductsOfAKind()
        {
            var rp = new TestReceiptPrinter();
            var m = new CartManager(new Cart(), rp, new DiscountsProvider());
            var c1 = TestProducts.Cars.First();
            var c2 = TestProducts.Cars.Skip(1).First();
            m.AddProduct(c1);
            m.AddProduct(c1);
            m.AddProduct(c1);
            m.AddProduct(c2);
            
            m.RemoveProduct(c1, 3);

            m.PrintReceipt();
            var state = rp.CartState;
            
            Assert.Equal(1, state.Products.Count);
            Assert.False(state.Products.ContainsKey(c1));
        }

        [Fact]
        public void RemoveMoreProductsThanExistsInCartOfAKind()
        {
            var rp = new TestReceiptPrinter();
            var m = new CartManager(new Cart(), rp, new DiscountsProvider());
            var c1 = TestProducts.Cars.First();
            var c2 = TestProducts.Cars.Skip(1).First();
            m.AddProduct(c1);
            m.AddProduct(c1);
            m.AddProduct(c1);
            m.AddProduct(c2);
            
            m.RemoveProduct(c1, 5);

            m.PrintReceipt();
            var state = rp.CartState;
            
            Assert.Equal(1, state.Products.Count);
            Assert.False(state.Products.ContainsKey(c1));
        }

        [Fact]
        public void UndoAddProductTest()
        {
            var rp = new TestReceiptPrinter();
            var m = new CartManager(new Cart(), rp, new DiscountsProvider());
            var c1 = TestProducts.Cars.First();
            var c2 = TestProducts.Cars.Skip(1).First();
            m.AddProduct(c1);
            m.AddProduct(c1);
            m.AddProduct(c2);
            
            m.Undo();

            m.PrintReceipt();
            var state = rp.CartState;
            Assert.Equal(1, state.Products.Count);
            Assert.Equal(2, state.Products[c1]);
        }

        [Fact]
        public void UndoAddProductAndAddNewAfterTest()
        {
            var rp = new TestReceiptPrinter();
            var m = new CartManager(new Cart(), rp, new DiscountsProvider());
            var c1 = TestProducts.Cars.First();
            var c2 = TestProducts.Cars.Skip(1).First();
            m.AddProduct(c1);
            m.AddProduct(c1);
            m.AddProduct(c2);
            
            m.Undo();
            m.AddProduct(c1);

            m.PrintReceipt();
            var state = rp.CartState;
            Assert.Equal(1, state.Products.Count);
            Assert.Equal(3, state.Products[c1]);
        }

        [Fact]
        public void UndoAddDiscountTest()
        {
            var rp = new TestReceiptPrinter();
            var dp = new DiscountsProvider();
            var m = new CartManager(new Cart(), rp, dp);
            
            var productDiscount = dp.GetProductsDiscounts().First();
            var cartDiscount = dp.GetCartDiscounts().First();
            
            var c1 = TestProducts.Cars.First();
            var c2 = TestProducts.Cars.Skip(1).First();
            m.AddProduct(c1);
            m.AddProduct(c2);
            
            m.AddDiscount(productDiscount.Key);
            m.AddDiscount(cartDiscount.Key);
            
            m.Undo(2);

            m.PrintReceipt();
            var state = rp.CartState;
            Assert.Equal(0, state.CartDiscounts.Count);
            Assert.Equal(0, state.ProductDiscounts.Count);
        }
        
        [Fact]
        public void UndoRemoveSingleProduct()
        {
            var rp = new TestReceiptPrinter();
            var m = new CartManager(new Cart(), rp, new DiscountsProvider());
            var c1 = TestProducts.Cars.First();
            var c2 = TestProducts.Cars.Skip(1).First();
            m.AddProduct(c1);
            m.AddProduct(c1);
            m.AddProduct(c2);
            
            m.RemoveProduct(c1);
            m.Undo();

            m.PrintReceipt();
            var state = rp.CartState;
            
            Assert.Equal(2, state.Products.Count);
            Assert.Equal(2, state.Products[c1]);
        }

        [Fact]
        public void RedoAddProduct()
        {
            var rp = new TestReceiptPrinter();
            var m = new CartManager(new Cart(), rp, new DiscountsProvider());
            var c1 = TestProducts.Cars.First();
            var c2 = TestProducts.Cars.Skip(1).First();
            m.AddProduct(c1);
            m.AddProduct(c1);
            m.AddProduct(c2);
            
            m.Undo();
            m.Redo();

            m.PrintReceipt();
            var state = rp.CartState;
            Assert.Equal(2, state.Products.Count);
            Assert.Equal(2, state.Products[c1]);
            Assert.Equal(1, state.Products[c2]);
        }
    }
}