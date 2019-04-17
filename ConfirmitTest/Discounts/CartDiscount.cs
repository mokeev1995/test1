namespace ConfirmitTest.Discounts
{
    public class CartDiscount
    {
        private readonly int _percentValue;

        public CartDiscount(int percentValue)
        {
            _percentValue = percentValue;
        }

        public double Make(double currentProductsPrice)
        {
            return currentProductsPrice * (100 - _percentValue) / 100;
        }
    }
}