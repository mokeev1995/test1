using ConfirmitTest.Discounts;
using ConfirmitTest.Products;

namespace ConfirmitTest
{
    public interface ICart
    {
        ICartState CurrentState { get; set; }
        void AddDiscount(ProductDiscount discount, IProduct product);
        void AddDiscount(CartDiscount discount);
        void AddProduct(IProduct product, int count = 1);
        void RemoveProduct(IProduct product, int count = 1);
    }
}