using Redis.Models;
using StackExchange.Redis;
using System.Text.Json;

namespace redis.Data
{
    public class RedisPlatformRepo : IPlatformRepo
    {
        private readonly IConnectionMultiplexer _redis;

        public RedisPlatformRepo(IConnectionMultiplexer redis)
        {
            this._redis = redis;
        }
        public void CreatePlatform(Platform plat)
        {
            if(plat == null)
            {
                throw new ArgumentNullException(nameof(plat));
            }

            var db = _redis.GetDatabase();

            var serialPlat = JsonSerializer.Serialize(plat);

            //db.StringSet(plat.Id, serialPlat);
            //db.SetAdd("PlatformSet", serialPlat);
            db.HashSet("hashplatform", new HashEntry[]
                {new HashEntry(plat.Id, serialPlat)});
        }

        public IEnumerable<Platform?>? GetAllPlatforms()
        {
            var db = _redis.GetDatabase();

            //var completeSet = db.SetMembers("PlatformSet");

            //if(completeSet.Length > 0)
            //{
            //    var obj = Array.ConvertAll(completeSet, val => JsonSerializer.Deserialize<Platform>(val)).ToList();

            //    return obj;
            //}

            var completeHash = db.HashGetAll("hashplatform");

            if (completeHash.Length > 0)
            {
                var obj = Array.ConvertAll(completeHash, val => JsonSerializer.Deserialize<Platform>(val.Value)).ToList();

                return obj;
            }

            return null;
        }

        public Platform? GetPlatformById(string id)
        {
            var db = _redis.GetDatabase();
            //var platform = db.StringGet(id);
            var platform = db.HashGet("hashplatform", id);

            if (!string.IsNullOrEmpty(platform))
            {
                return JsonSerializer.Deserialize<Platform>(platform);
            }

            

            return null;
        }
    }
}
