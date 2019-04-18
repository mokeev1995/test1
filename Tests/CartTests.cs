using System.Collections.Generic;
using System.Linq;
using ConfirmitTest;
using ConfirmitTest.Discounts;
using ConfirmitTest.Products;
using Xunit;

namespace Tests
{
    public class TestCartState : ICartState
    {
        public IDictionary<IProduct, int> Products { get; }
        public ICollection<CartDiscount> CartDiscounts { get; }
        public IDictionary<IProduct, ProductDiscount> ProductDiscounts { get; }
        public double Total { get; }
        public void Restore()
        {
            throw new System.NotImplementedException();
        }

        public ICartState Clone()
        {
            throw new System.NotImplementedException();
        }
    } 
    
    public class CartTests
    {
        [Fact]
        public void AddProductTest()
        {
            var cart = new Cart();
            var product = TestProducts.Cars.First();
            const int count = 10;
            cart.AddProduct(product, count);
            
            Assert.Equal(cart.CurrentState.Products.Count, 1);
            Assert.True(cart.CurrentState.Products.ContainsKey(product));
            Assert.Equal(cart.CurrentState.Products[product], count);
        }
        
        [Fact]
        public void AddExistingProductTest()
        {
            var cart = new Cart();
            var product = TestProducts.Cars.First();
            const int count = 10;
            cart.AddProduct(product, count);

            const int more = 1;
            cart.AddProduct(product, more);
            
            Assert.Equal(cart.CurrentState.Products.Count, 1);
            Assert.True(cart.CurrentState.Products.ContainsKey(product));
            Assert.Equal(cart.CurrentState.Products[product], count + more);
        }

        [Fact]
        public void RemoveProductTest()
        {
            var cart = new Cart();
            var c1 = TestProducts.Cars.First();
            var c2 = TestProducts.Cars.Skip(1).First();
            cart.AddProduct(c1, 2);
            cart.AddProduct(c2, 1);
            
            cart.RemoveProduct(c1, 3);

            Assert.Equal(cart.CurrentState.Products.Count, 1);
            Assert.False(cart.CurrentState.Products.ContainsKey(c1));
            Assert.True(cart.CurrentState.Products.ContainsKey(c2));
        }

        [Fact]
        public void RemoveProductNotInCartTest()
        {
            var cart = new Cart();
            var c1 = TestProducts.Cars.First();
            var c2 = TestProducts.Cars.Skip(1).First();
            cart.AddProduct(c1, 2);
            
            cart.RemoveProduct(c2, 3);
            
            Assert.True(cart.CurrentState.Products.ContainsKey(c1));
            Assert.Equal(cart.CurrentState.Products.Count, 1);
        }

        [Fact]
        public void RemoveNotAllProductsTest()
        {
            var cart = new Cart();
            var c1 = TestProducts.Cars.First();
            var c2 = TestProducts.Cars.Skip(1).First();
            cart.AddProduct(c1, 2);
            cart.AddProduct(c2, 3);
            
            cart.RemoveProduct(c1, 1);
            
            Assert.True(cart.CurrentState.Products.ContainsKey(c1));
            Assert.Equal(cart.CurrentState.Products.Count, 2);
            Assert.Equal(cart.CurrentState.Products[c1], 1);
        }

        [Fact]
        public void CreateWithExternalState()
        {
            var state = new TestCartState();
            var cart = new Cart(state);
            
            Assert.Equal(state, cart.CurrentState);
        }

        [Fact]
        public void AddCartDiscountTest()
        {
            var cart = new Cart();
            var discount = new CartDiscount(10);
            cart.AddDiscount(discount);
            
            Assert.Equal(1, cart.CurrentState.CartDiscounts.Count);
            Assert.Equal(discount, cart.CurrentState.CartDiscounts.First());
        }

        [Fact]
        public void AddProductDiscountTest()
        {
            var cart = new Cart();
            var product = TestProducts.Cars.First();
            var discount = new ProductDiscount(product, 10);
            cart.AddDiscount(discount, product);
            
            Assert.Equal(1, cart.CurrentState.ProductDiscounts.Count);
            Assert.True(cart.CurrentState.ProductDiscounts.ContainsKey(product));
            Assert.Equal(discount, cart.CurrentState.ProductDiscounts[product]);
        }
    }
}