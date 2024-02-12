namespace TheImageComparer.UI.StartupHelpers;
public interface IAbstractFactory<T>
{
    T Create();
}