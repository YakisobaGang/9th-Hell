
namespace ProjectD.Interfaces
{
    public interface ICommand
    {
        void Execute();
        void Undo();
    }
}
