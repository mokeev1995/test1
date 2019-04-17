using System.Collections.Generic;
using ConfirmitTest.Discounts;
using ConfirmitTest.Products;

namespace ConfirmitTest
{
    public interface ICartState
    {
        IDictionary<IProduct, int> Products { get; }
        ICollection<CartDiscount> CartDiscounts { get; }
        IDictionary<IProduct, ProductDiscount> ProductDiscounts { get; }
        double Total { get; }
        void Restore();
        ICartState Clone();
    }
}