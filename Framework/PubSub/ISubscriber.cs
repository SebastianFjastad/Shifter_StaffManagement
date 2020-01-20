namespace Framework.PubSub
{
    public interface ISubscriber : IProcessor
    {
        void Subscribe(string token);

        void UnSubscribe(string token);
    }
}
