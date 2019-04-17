namespace ConfirmitTest.Products
{
    public class Car : AbstractProduct, IProduct
    {
        public Car(int id, string brand, string model, double price) : base(id, price)
        {
            Brand = brand;
            Model = model;
        }

        public string Brand { get; }
        public string Model { get; }

        public override string ToString()
        {
            return $"{Brand} {Model}";
        }
    }
}