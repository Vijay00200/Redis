using Redis.Models;

namespace redis.Data
{
    public interface IPlatformRepo
    {
        void CreatePlatform(Platform plat);
        
        Platform? GetPlatformById(string id);

        IEnumerable<Platform> GetAllPlatforms();
    }
}
