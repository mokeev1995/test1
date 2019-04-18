using ConfirmitTest.Discounts;
using Xunit;

namespace Tests
{
    public class CartDiscountTests
    {
        [Fact]
        public void CalculateDiscount()
        {
            var percent = 50;
            var cd = new CartDiscount(percent);
            Assert.Equal(percent, cd.PercentValue);
            Assert.Equal(50.0, cd.Make(100.0));
        }
    }
}