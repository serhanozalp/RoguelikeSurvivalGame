public interface ICommand
{
    public void Execute();
}

public interface ICommandWithUndo : ICommand
{
    public void Undo();
}