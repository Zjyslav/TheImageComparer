using TheImageComparer.Logic.Properties;

namespace TheImageComparer.Logic.Services;
public class ResourcesService : IResourcesService
{
    public byte[] DatabaseTemplate
    {
        get
        {
            return Resources.database;
        }
    }
}
