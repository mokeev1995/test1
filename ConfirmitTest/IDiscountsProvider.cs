using System.Collections.Generic;
using ConfirmitTest.Products;

namespace ConfirmitTest
{
    public interface IDiscountsProvider
    {
        Dictionary<string, int> GetCartDiscounts();
        Dictionary<string, (IProduct Product, int Value)> GetProductsDiscounts();
    }
}