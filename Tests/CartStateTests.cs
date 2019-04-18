using System.Linq;
using System.Runtime.CompilerServices;
using ConfirmitTest;
using ConfirmitTest.Discounts;
using Xunit;

namespace Tests
{
    public class CartStateTests
    {
        [Fact]
        public void CreateEmptyState()
        {
            var state = new CartState(new Cart());
            Assert.Equal(0, state.Total);
        }

        [Fact]
        public void CalculateTotalWithProducts()
        {
            var state = new CartState(new Cart());

            var product = TestProducts.Cars.First();
            const int count = 2;
            state.Products.Add(product, count);

            Assert.Equal(product.Price * count, state.Total);
        }

        [Fact]
        public void CalculateTotalWithProductsAndDiscounts()
        {
            var state = new CartState(new Cart());

            var product = TestProducts.Cars.First();
            const int count = 2;
            state.Products.Add(product, count);

            var cartDiscount = new CartDiscount(50);
            var productDiscount = new ProductDiscount(product, 50);

            state.CartDiscounts.Add(cartDiscount);
            state.ProductDiscounts.Add(product, productDiscount);

            Assert.Equal(product.Price * count / 4, state.Total);
        }
    }
}