using System;
using ConfirmitTest;
using ConfirmitTest.Commands;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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

    [TestClass]
    public class CommandsHistoryTests
    {
        [TestMethod]
        public void PopTest()
        {
            var c1 = new TestCommand("1");
            var c2 = new TestCommand("2");
            var c3 = new TestCommand("3");
            var ch = new CommandsHistory();
            ch.Push(c1);
            ch.Push(c2);
            ch.Push(c3);

            Assert.AreEqual(c3, ch.Pop());
            Assert.AreEqual(c2, ch.Pop());
            Assert.AreEqual(c1, ch.Pop());
        }

        [TestMethod]
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

            Assert.IsTrue(moved);
            Assert.AreEqual(c2, actual);
        }

        [TestMethod]
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

            Assert.AreEqual(c3, ch.Pop());
        }

        [TestMethod]
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

            Assert.IsFalse(moved);
            Assert.AreEqual(null, actual);
        }

        [TestMethod]
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

            Assert.AreEqual(c4, ch.Pop());
        }
    }
}