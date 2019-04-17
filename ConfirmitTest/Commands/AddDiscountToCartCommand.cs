using ConfirmitTest.Discounts;

namespace ConfirmitTest.Commands
{
    internal class AddDiscountToCartCommand : ICommand
    {
        private ICartState _state;
        private readonly ICart _cart;
        private readonly int _discountPercent;

        public AddDiscountToCartCommand(ICart cart, int discountPercent)
        {
            _cart = cart;
            _discountPercent = discountPercent;
        }

        public void Execute()
        {
            _state = _cart.CurrentState.Clone();
            _cart.AddDiscount(new CartDiscount(_discountPercent));
        }

        public void Undo()
        {
            _state.Restore();
        }
    }
}