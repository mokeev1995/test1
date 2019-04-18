using ConfirmitTest.Products;

namespace ConfirmitTest.Discounts
{
    public class ProductDiscount
    {
        public double PercentValue { get; }
        private readonly IProduct _product;

        public ProductDiscount(IProduct product, double percentValue)
        {
            _product = product;
            PercentValue = percentValue;
        }

        public double Make(IProduct product)
        {
            return product.Id == _product.Id 
                ? GetNewPrice(product) 
                : product.Price;
        }

        private double GetNewPrice(IProduct product)
        {
            return product.Price * (100 - PercentValue) / 100;
        }
    }
}