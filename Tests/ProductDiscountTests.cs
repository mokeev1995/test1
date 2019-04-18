using ConfirmitTest.Discounts;
using ConfirmitTest.Products;
using Xunit;

namespace Tests
{
    public class ProductDiscountTests
    {
        [Fact]
        public void CalculateDiscount()
        {
            var percent = 50;
            var c1 = new Car(1, "", "", 100.0);
            var cd = new ProductDiscount(c1, percent);
            Assert.Equal(percent, cd.PercentValue);
            Assert.Equal(50.0, cd.Make());
        }
    }
}