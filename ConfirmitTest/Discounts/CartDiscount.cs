namespace ConfirmitTest.Discounts
{
    public class CartDiscount
    {
        public int PercentValue { get; }

        public CartDiscount(int percentValue)
        {
            PercentValue = percentValue;
        }

        public double Make(double currentProductsPrice)
        {
            return currentProductsPrice * (100 - PercentValue) / 100;
        }
    }
}