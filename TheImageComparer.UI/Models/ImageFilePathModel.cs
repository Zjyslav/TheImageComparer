using CommunityToolkit.Mvvm.ComponentModel;
using System.IO;
using System.Windows.Media;

namespace TheImageComparer.UI.Models;
public partial class ImageFilePathModel : ObservableObject
{
    public ImageFilePathModel(string filePath)
    {
        FilePath = filePath;
    }

    public string FilePath { get; set; }
    public string FileName
    {
        get
        {
            return Path.GetFileName(FilePath);
        }
    }
    [ObservableProperty]
    private bool _selected = true;
}
