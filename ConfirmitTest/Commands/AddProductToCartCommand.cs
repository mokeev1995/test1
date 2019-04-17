using ConfirmitTest.Products;

namespace ConfirmitTest.Commands
{
    internal class AddProductToCartCommand : ICommand
    {
        private readonly ICart _cart;
        private readonly IProduct _product;
        private readonly int _count;
        private ICartState _state;

        public AddProductToCartCommand(ICart cart, IProduct product, int count)
        {
            _cart = cart;
            _product = product;
            _count = count;
        }

        public void Execute()
        {
            _state = _cart.CurrentState.Clone();
            _cart.AddProduct(_product, _count);
        }

        public void Undo()
        {
            _state.Restore();
        }
    }
}