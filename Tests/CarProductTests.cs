using ConfirmitTest.Products;
using Xunit;

namespace Tests
{
    public class CarProductTests
    {
        [Fact]
        public void CreateTest()
        {
            var brand = "brand";
            var model = "model";
            var price = 100.0;
            var c = new Car(1, brand, model, price);
            
            Assert.Equal(brand, c.Brand);
            Assert.Equal(model, c.Model);
            Assert.Equal(price, c.Price);
            Assert.Equal($"{brand} {model}", c.ToString());
        }
    }
}