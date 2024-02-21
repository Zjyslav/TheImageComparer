using System.IO;
using System.Windows.Media;

namespace TheImageComparer.UI.Models;
public class ImageFilePathModel
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
}
