using System.Collections.Generic;
using System.Linq;
using ConfirmitTest.Discounts;
using ConfirmitTest.Products;

namespace ConfirmitTest
{
    internal class CartState : ICartState
    {
        private readonly ICart _cart;

        public CartState(ICart cart)
            : this(cart, new Dictionary<IProduct, int>(), new List<CartDiscount>(), new Dictionary<IProduct, ProductDiscount>())
        {
            _cart = cart;
        }

        private CartState(ICart cart, IDictionary<IProduct, int> products, ICollection<CartDiscount> cartDiscounts,
            IDictionary<IProduct, ProductDiscount> productDiscounts)
        {
            _cart = cart;
            Products = products;
            CartDiscounts = cartDiscounts;
            ProductDiscounts = productDiscounts;
        }

        public IDictionary<IProduct, int> Products { get; }
        public ICollection<CartDiscount> CartDiscounts { get; }
        public IDictionary<IProduct, ProductDiscount> ProductDiscounts { get; }

        public double Total => CalculateTotalPrice();

        public void Restore()
        {
            _cart.CurrentState = this;
        }

        public ICartState Clone()
        {
            return new CartState(
                _cart,
                new Dictionary<IProduct, int>(Products),
                new List<CartDiscount>(CartDiscounts),
                new Dictionary<IProduct, ProductDiscount>(ProductDiscounts)
            );
        }

        private double CalculateTotalPrice()
        {
            var allProductsPrice = Products.Sum(pair => CalculatePriceForProduct(pair.Key, pair.Value));

            return CartDiscounts.Aggregate(allProductsPrice, (current, cartDiscount) => cartDiscount.Make(current));
        }

        private double CalculatePriceForProduct(IProduct product, int count)
        {
            var itemPrice = ProductDiscounts.ContainsKey(product)
                ? ProductDiscounts[product].Make()
                : product.Price;

            return itemPrice * count;
        }
    }
}