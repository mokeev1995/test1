using ConfirmitTest.Products;
using Xunit;

namespace Tests
{
    class TestProduct : AbstractProduct
    {
        public TestProduct(int id, double price) : base(id, price)
        {
        }
    }
    
    public class AbstractProductTests
    {
        [Fact]
        public void CreateTest()
        {
            const int id = 1;
            const int price = 100;
            var t = new TestProduct(id, price);
            
            Assert.Equal(id, t.Id);
            Assert.Equal(price, t.Price);
        }

        [Fact]
        public void EqualsTest()
        {
            const int id = 1;
            const int price = 100;
            var t1 = new TestProduct(id, price);
            var t2 = new TestProduct(id, price);
            
            Assert.Equal(t1, t2);
            Assert.True(t1.Equals(t2));
            Assert.True(t1.Equals((object)t2));
            Assert.True(t1.Equals((object)t1));
            Assert.False(t1.Equals("asd"));
            Assert.False(t1.Equals(null));
            Assert.False(t1.Equals((object)null));
        }
    }
}