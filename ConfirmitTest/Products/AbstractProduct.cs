using System;

namespace ConfirmitTest.Products
{
    public abstract class AbstractProduct : IProduct
    {
        protected AbstractProduct(int id, double price)
        {
            Id = id;
            Price = price;
        }

        public virtual bool Equals(IProduct other)
        {
            return other?.Id == Id;
        }

        protected bool Equals(AbstractProduct other)
        {
            return Id == other.Id;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((AbstractProduct) obj);
        }

        public override int GetHashCode()
        {
            return Id;
        }

        public int Id { get; }
        public double Price { get; set; }
    }
}