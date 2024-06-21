using System.Windows;
using System.Windows.Controls;
using TheImageComparer.UI.Models;

namespace TheImageComparer.UI.Controls;
public partial class ImageSummaryPanel : UserControl
{
    public static readonly DependencyProperty ImageProperty = DependencyProperty
        .Register(nameof(Image),
                  typeof(ImageUIModel),
                  typeof(ImageSummaryPanel));
    public ImageUIModel Image
    {
        get { return (ImageUIModel)GetValue(ImageProperty); }
        set { SetValue(ImageProperty, value); }
    }
    public ImageSummaryPanel()
    {
        InitializeComponent();
    }
}
