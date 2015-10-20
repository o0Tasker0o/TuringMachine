using System.Collections.ObjectModel;
namespace TuringMachine
{
    public interface ITape
    {
        char GetSymbol(int index);
        int GetIndex();

        char Read();
        void Write(char symbol);

        void MoveLeft();
        void MoveRight();
    }
}
