using System;

namespace ConfirmitTest.Products
{
    public interface IProduct : IEquatable<IProduct>
    {
        int Id { get; }
        double Price { get; set; }
    }
}