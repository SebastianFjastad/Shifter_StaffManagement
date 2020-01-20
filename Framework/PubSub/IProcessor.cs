namespace Framework.PubSub
{
    public interface IProcessor
    {
        void Process(object message);
    }
}