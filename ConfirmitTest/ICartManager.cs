using ConfirmitTest.Products;

namespace ConfirmitTest
{
    public interface ICartManager
    {
        void AddDiscount(string code);
        void AddProduct(IProduct product, int count = 1);
        void RemoveProduct(IProduct product, int count = 1);
        void Undo(int count = 1);
        void Redo(int count = 1);

        void PrintReceipt();
    }
}