using System.Collections.Generic;
using ConfirmitTest.Products;

namespace Tests
{
    public static class TestProducts
    {
        public static readonly IReadOnlyList<IProduct> Cars = new List<IProduct>
        {
            new Car(1, "BMW", "X6M", 125_000),
            new Car(2, "BMW", "X4", 80_000),
            new Car(3, "BMW", "X3", 70_000),
            new Car(4, "BMW", "X2", 60_000),
            new Car(5, "BMW", "X1", 40_000),
            new Car(6, "BMW", "X7", 130_000),
        };
    }
}