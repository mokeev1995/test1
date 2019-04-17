using ConfirmitTest.Discounts;
using ConfirmitTest.Products;

namespace ConfirmitTest
{
    public class Cart : ICart
    {
        public Cart()
        {
            CurrentState = new CartState(this);
        }

        public Cart(ICartState cartState)
        {
            CurrentState = cartState;
        }

        public ICartState CurrentState { get; set; }

        public void AddDiscount(ProductDiscount discount, IProduct product)
        {
            CurrentState.ProductDiscounts.Add(product, discount);
        }

        public void AddDiscount(CartDiscount discount)
        {
            CurrentState.CartDiscounts.Add(discount);
        }

        public void AddProduct(IProduct product, int count = 1)
        {
            if (CurrentState.Products.ContainsKey(product))
            {
                CurrentState.Products[product] += count;
            }
            else
            {
                CurrentState.Products.Add(product, count);
            }
        }

        public void RemoveProduct(IProduct product, int count = 1)
        {
            if (!CurrentState.Products.ContainsKey(product))
                return;

            var currentCount = CurrentState.Products[product];

            var newCount = currentCount - count;

            if (newCount < 0)
            {
                CurrentState.Products.Remove(product);
                return;
            }

            CurrentState.Products[product] = newCount;
        }
    }
}