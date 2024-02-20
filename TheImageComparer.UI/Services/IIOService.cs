namespace TheImageComparer.UI.Services;

public interface IIOService
{
    string? GetFilePathWithDialog(string filter);
    string? GetSaveFilePathWithDialog(string filter, string extension, string defaultName);
}