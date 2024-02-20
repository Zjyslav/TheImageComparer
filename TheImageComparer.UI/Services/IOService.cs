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

    public string? GetSaveFilePathWithDialog(string filter)
    {
        SaveFileDialog saveFileDialog = new()
        {
            Filter = filter,
            RestoreDirectory = true
        };
        if (saveFileDialog.ShowDialog() == true)
            return saveFileDialog.FileName;
        return null;
    }
}