using CommunityToolkit.Mvvm.Messaging.Messages;

namespace TheImageComparer.UI.Messages;
internal class OpenFolderMessage : ValueChangedMessage<string>
{
    public string FolderPath { get; }
    public OpenFolderMessage(string folderPath) : base(folderPath)
    {
        FolderPath = folderPath;
    }
}
