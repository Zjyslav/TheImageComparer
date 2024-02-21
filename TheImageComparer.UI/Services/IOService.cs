using Microsoft.Win32;

namespace TheImageComparer.UI.Services;

public class IOService : IIOService
{
    public string? GetFilePathWithDialog(string filter)
    {
        OpenFileDialog openFileDialog = new()
        {
            Filter = filter,
            RestoreDirectory = true
        };
        if (openFileDialog.ShowDialog() == true)
            return openFileDialog.FileName;
        return null;
    }

    public string? GetSaveFilePathWithDialog(string filter, string extension, string defaultName)
    {
        SaveFileDialog saveFileDialog = new()
        {
            Filter = filter,
            RestoreDirectory = true,
            AddExtension = true,
            DefaultExt = extension,
            FileName = defaultName
        };
        if (saveFileDialog.ShowDialog() == true)
            return saveFileDialog.FileName;
        return null;
    }

    public string? GetFolderPathWithDialog()
    {
        OpenFolderDialog openFolderDialog = new();
        if(openFolderDialog.ShowDialog() == true)
            return openFolderDialog.FolderName;
        return null;
    }
}