namespace Mql2Fdk
{
    public interface IStrategyLog
    {
        void Comment(params object[] args);

        void Print(params object[] args);

        void Alert(params object[] args);
    }
}
