using CommunityToolkit.Mvvm.Messaging.Messages;

namespace TheImageComparer.UI.Messages;

public class OpenDbMessage : ValueChangedMessage<string>
{
    public string DbFilePath { get; }
    public OpenDbMessage(string value) : base(value)
    {
        DbFilePath = value;
    }
}
