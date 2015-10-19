namespace TuringMachine
{
    public interface ITape
    {
        char? Read();
        void Write(char? symbol);

        void MoveLeft();
        void MoveRight();
    }
}
