using CommunityToolkit.Mvvm.Messaging.Messages;
using TheImageComparer.Logic.Models;

namespace TheImageComparer.UI.Messages;
internal class ShowImageDetailsMessage : ValueChangedMessage<ImageModel>
{
    public ImageModel Image { get; }
    public ShowImageDetailsMessage(ImageModel image) : base(image)
    {
        Image = image;
    }
}
