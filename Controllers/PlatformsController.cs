using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using redis.Data;
using Redis.Models;

namespace redis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlatformsController : ControllerBase
    {
        private readonly IPlatformRepo _platformRepo;

        public PlatformsController(IPlatformRepo platformRepo)
        {
            this._platformRepo = platformRepo;
        }

        [HttpGet("{id}", Name = "GetPlatformById")]
        public ActionResult<Platform> GetPlatformById(string id)
        {
            var platform = _platformRepo.GetPlatformById(id);

            if (platform != null)
            {
                return Ok(platform);
            }

            return NotFound();
        }

        [HttpPost]
        public ActionResult<Platform> CreatePlatform([FromBody] Platform platform)
        {
            _platformRepo.CreatePlatform(platform);

            return CreatedAtRoute(nameof(GetPlatformById), new { Id = platform.Id }, platform);
        }

        [HttpGet(Name = "GetAllPlatforms")]
        public ActionResult<IEnumerable<Platform>> GetAllPlatforms()
        {
            return Ok(_platformRepo.GetAllPlatforms());
        }
    }
}
