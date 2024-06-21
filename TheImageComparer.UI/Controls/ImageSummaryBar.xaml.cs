using System.Windows;
using System.Windows.Controls;
using TheImageComparer.UI.Models;

namespace TheImageComparer.UI.Controls
{
    public partial class ImageSummaryBar : UserControl
    {
        public static readonly DependencyProperty ImageProperty = DependencyProperty
        .Register(nameof(Image),
                  typeof(ImageUIModel),
                  typeof(ImageSummaryBar));
        public ImageUIModel Image
        {
            get { return (ImageUIModel)GetValue(ImageProperty); }
            set { SetValue(ImageProperty, value); }
        }
        public ImageSummaryBar()
        {
            InitializeComponent();
        }
    }
}
