using ConfirmitTest.Products;

namespace ConfirmitTest.Discounts
{
    public class ProductDiscount
    {
        private readonly double _percentValue;
        private readonly IProduct _product;

        public ProductDiscount(IProduct product, double percentValue)
        {
            _product = product;
            _percentValue = percentValue;
        }

        public double Make(IProduct product)
        {
            return product.Id == _product.Id 
                ? GetNewPrice(product) 
                : product.Price;
        }

        private double GetNewPrice(IProduct product)
        {
            return product.Price * (100 - _percentValue) / 100;
        }
    }
}