using System;
using ConfirmitTest.Products;

namespace ConfirmitTest.Commands
{
    internal class RemoveFromCartCommand : ICommand
    {
        private ICartState _state;

        private readonly ICart _cart;
        private readonly IProduct _product;
        private readonly int _count;

        public RemoveFromCartCommand(ICart cart, IProduct product, int count)
        {
            _cart = cart;
            _product = product;
            _count = count;
        }

        public void Execute()
        {
            _state = _cart.CurrentState.Clone();

            _cart.RemoveProduct(_product, _count);
        }

        public void Undo()
        {
            _state.Restore();
        }
    }
}