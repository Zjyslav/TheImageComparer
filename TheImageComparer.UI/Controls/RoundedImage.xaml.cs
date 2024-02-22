using System.Windows;
using System.Windows.Controls;

namespace TheImageComparer.UI.Controls;
public partial class RoundedImage : UserControl
{
    public static readonly DependencyProperty ImageSourceProperty = DependencyProperty
        .Register(nameof(ImageSource),
                  typeof(string),
                  typeof(RoundedImage));
    public string ImageSource
    {
        get { return (string)GetValue(ImageSourceProperty); }
        set { SetValue(ImageSourceProperty, value); }
    }
    public RoundedImage()
    {
        InitializeComponent();
    }
}
