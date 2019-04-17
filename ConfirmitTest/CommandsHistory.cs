using System.Collections.Generic;
using ConfirmitTest.Commands;

namespace ConfirmitTest
{
    public class CommandsHistory
    {
        private readonly List<ICommand> _commands = new List<ICommand>();
        private int _currentCommandPosition = -1;

        public void Push(ICommand command)
        {
            if (HasNextCommand())
            {
                _currentCommandPosition++;
                _commands[_currentCommandPosition] = command;
                ClearUnused();
            }
            else
            {
                _commands.Add(command);
                _currentCommandPosition++;
            }
        }

        public ICommand Pop()
        {
            var command = _commands[_currentCommandPosition];
            _currentCommandPosition--;
            return command;
        }

        public bool TryMoveForward(out ICommand command)
        {
            if (!HasNextCommand())
            {
                command = default;
                return false;
            }

            _currentCommandPosition++;
            command = _commands[_currentCommandPosition];
            return true;
        }

        private void ClearUnused()
        {
            for (var i = _currentCommandPosition + 1; i < _commands.Count; i++) _commands[i] = null;
        }

        private bool HasNextCommand()
        {
            return _currentCommandPosition < _commands.Count - 1;
        }
    }
}