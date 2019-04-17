namespace ConfirmitTest
{
    public interface ICommand
    {
        void Execute();
        void Undo();
    }
}