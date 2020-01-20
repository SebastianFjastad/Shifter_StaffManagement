namespace Framework.PubSub.Messages
{
    public interface IMessage
    {

        IMessageHeader Header { get; set; }

        object Body { get; set; }
    }
}