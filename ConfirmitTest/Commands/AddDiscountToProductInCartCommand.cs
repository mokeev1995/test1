using ConfirmitTest.Discounts;
using ConfirmitTest.Products;

namespace ConfirmitTest.Commands
{
    internal class AddDiscountToProductInCartCommand : ICommand
    {
        private ICartState _state;

        private readonly ICart _cart;
        private readonly IProduct _product;
        private readonly int _percentValue;

        public AddDiscountToProductInCartCommand(ICart cart, IProduct product, int percentValue)
        {
            _cart = cart;
            _product = product;
            _percentValue = percentValue;
        }

        public void Execute()
        {
            _state = _cart.CurrentState.Clone();

            var discount = new ProductDiscount(_product, _percentValue);
            _cart.AddDiscount(discount, _product);
        }

        public void Undo()
        {
            _state.Restore();
        }
    }
}