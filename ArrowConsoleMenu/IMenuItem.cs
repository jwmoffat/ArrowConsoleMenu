namespace ArrowConsoleMenu
{
    public interface IMenuItem
    {
        string Description { get; }

        void RunAction();
    }
}