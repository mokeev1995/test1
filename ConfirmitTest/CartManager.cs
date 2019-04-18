using System;
using System.Collections.Generic;
using ConfirmitTest.Commands;
using ConfirmitTest.Exceptions;
using ConfirmitTest.Products;

namespace ConfirmitTest
{
    
    public class CartManager : ICartManager
    {
        private enum DiscountType
        {
            Unknown, Cart, Product
        }
        
        private readonly ICart _cart;
        private readonly IReceiptPrinter _receiptPrinter;

        private readonly Dictionary<string, int> _cartDiscounts;

        private readonly CommandsHistory _commandsHistory = new CommandsHistory();

        private readonly Dictionary<string, (IProduct Product, int Value)> _productsDiscounts;

        public CartManager(ICart cart, IReceiptPrinter receiptPrinter, IDiscountsProvider discountsProvider)
        {
            _cart = cart;
            _receiptPrinter = receiptPrinter;
            _cartDiscounts = discountsProvider.GetCartDiscounts();
            _productsDiscounts = discountsProvider.GetProductsDiscounts();

        }

        public void AddDiscount(string code)
        {
            var discountType = GetDiscountTypeByCode(code);
            ICommand command;
            switch (discountType)
            {
                case DiscountType.Product:
                {
                    var (product, value) = _productsDiscounts[code];
                    command = new AddDiscountToProductInCartCommand(_cart, product, value);
                    break;
                }

                case DiscountType.Cart:
                {
                    var value = _cartDiscounts[code];
                    command = new AddDiscountToCartCommand(_cart, value);
                    break;
                }

                default:
                    throw new DiscountNotFoundException($"Discount with code `{code}` was not found");
            }

            _commandsHistory.Push(command);
            command.Execute();
        }

        public void AddProduct(IProduct product, int count = 1)
        {
            var command = new AddProductToCartCommand(_cart, product, count);
            _commandsHistory.Push(command);
            command.Execute();
        }

        public void RemoveProduct(IProduct product, int count = 1)
        {
            var command = new RemoveFromCartCommand(_cart, product, count);
            _commandsHistory.Push(command);
            command.Execute();
        }

        public void Undo(int count = 1)
        {
            for (var i = 0; i < count; i++)
            {
                var command = _commandsHistory.Pop();
                command.Undo();
            }
        }

        public void Redo(int count = 1)
        {
            for (var i = 0; i < count; i++)
            {
                _commandsHistory.TryMoveForward(out var command);
                command?.Execute();
            }
        }

        public void PrintReceipt()
        {
            _receiptPrinter.Print(_cart.CurrentState);
        }

        private DiscountType GetDiscountTypeByCode(string code)
        {
            if (_productsDiscounts.ContainsKey(code))
                return DiscountType.Product;
            if (_cartDiscounts.ContainsKey(code))
                return DiscountType.Cart;
            
            return DiscountType.Unknown;
        }
    }
}