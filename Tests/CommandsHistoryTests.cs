using System;
using ConfirmitTest;
using Xunit;

namespace Tests
{
    internal class TestCommand : ICommand
    {
        public TestCommand(string name)
        {
            Name = name;
        }

        public string Name { get; }

        public void Execute()
        {
            throw new NotImplementedException();
        }

        public void Undo()
        {
            throw new NotImplementedException();
        }
    }

    public class CommandsHistoryTests
    {
        [Fact]
        public void PopTest()
        {
            var c1 = new TestCommand("1");
            var c2 = new TestCommand("2");
            var c3 = new TestCommand("3");
            var ch = new CommandsHistory();
            ch.Push(c1);
            ch.Push(c2);
            ch.Push(c3);

            Assert.Equal(c3, ch.Pop());
            Assert.Equal(c2, ch.Pop());
            Assert.Equal(c1, ch.Pop());
        }

        [Fact]
        public void MovesForward()
        {
            var c1 = new TestCommand("1");
            var c2 = new TestCommand("2");
            var c3 = new TestCommand("3");
            var ch = new CommandsHistory();
            ch.Push(c1);
            ch.Push(c2);
            ch.Push(c3);

            ch.Pop();
            ch.Pop();
            var moved = ch.TryMoveForward(out var actual);

            Assert.True(moved);
            Assert.Equal(c2, actual);
        }

        [Fact]
        public void PushAfterPop()
        {
            var c1 = new TestCommand("1");
            var c2 = new TestCommand("2");
            var c3 = new TestCommand("3");
            var ch = new CommandsHistory();
            ch.Push(c1);
            ch.Push(c2);
            ch.Push(c3);

            ch.Pop();
            ch.Push(c3);

            Assert.Equal(c3, ch.Pop());
        }

        [Fact]
        public void CantMoveForward()
        {
            var c1 = new TestCommand("1");
            var c2 = new TestCommand("2");
            var c3 = new TestCommand("3");
            var ch = new CommandsHistory();
            ch.Push(c1);
            ch.Push(c2);
            ch.Push(c3);

            var moved = ch.TryMoveForward(out var actual);

            Assert.False(moved);
            Assert.Equal(null, actual);
        }

        [Fact]
        public void ClearingUnusedItems()
        {
            var c1 = new TestCommand("1");
            var c2 = new TestCommand("2");
            var c3 = new TestCommand("3");
            var c4 = new TestCommand("4");
            var ch = new CommandsHistory();
            ch.Push(c1);
            ch.Push(c2);
            ch.Push(c3);
            ch.Push(c4);

            ch.Pop();
            ch.Pop();
            ch.Pop();
            ch.Push(c4);

            Assert.Equal(c4, ch.Pop());
        }
    }
}