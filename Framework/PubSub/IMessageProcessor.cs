namespace Framework.PubSub
{
    public interface IMessageProcessor
    {
        void Initialize();

        void Shutdown();
    }
}