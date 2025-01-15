
using Microsoft.AspNetCore.Mvc;
using news_angular_app.Server.Services;

namespace news_angular_app.Server.Controllers
{

    [Route("[controller]")]
    [ApiController]
    public class HackerNewsController : ControllerBase
    {
        private readonly INewStoriesService _newStoriesService;

        public HackerNewsController(INewStoriesService newStoriesService)
        {
            _newStoriesService = newStoriesService;
        }

        [HttpGet("storycount")]
        public async Task<ActionResult<List<Story>>> GetNewestStories()
        {
            var stories = await _newStoriesService.GetNewStoriesAsync();
            return Ok(stories);
        }

        [HttpGet("storydetails")]
        public async Task<IActionResult> GetNewestStories100(int page = 1, int pageSize = 20)
        {
            var stories = await _newStoriesService.GetNewestStoriesAsync(page, pageSize);
            return Ok(stories);
        }
    }
}
























































//using Microsoft.AspNetCore.Mvc;
//using System.Net.Http;
//using System.Threading.Tasks;
//using Newtonsoft.Json;
//using Microsoft.Extensions.Caching.Memory;

//namespace news_angular_app.Server.Controllers
//{
//    //[Route("api/[controller]")]
//    //[ApiController]
//    [ApiController]
//    [Route("[controller]")]
//    public class HackerNewsController : ControllerBase
//    {
//        private readonly HttpClient _httpClient;
//        private readonly IMemoryCache _memoryCache;

//        // Inject HttpClient through dependency injection
//        public HackerNewsController(HttpClient httpClient, IMemoryCache memoryCache)
//        {
//            _httpClient = httpClient;
//            _memoryCache = memoryCache;
//        }


//        // Get the new stories from Hacker News
//        [HttpGet("newstories")]
//        public async Task<IActionResult> GetNewStories()
//        {
//            if (!_memoryCache.TryGetValue("NewStories", out int? newStoryIdsCount))
//            {
//                var response = await _httpClient.GetStringAsync("https://hacker-news.firebaseio.com/v0/newstories.json?print=pretty");
//                var newStoryIds = JsonConvert.DeserializeObject<int[]>(response);
//                newStoryIdsCount = newStoryIds?.Length;

//                // Set cache options (optional expiration, priority, etc.)
//                var cacheOptions = new MemoryCacheEntryOptions()
//                    .SetSlidingExpiration(TimeSpan.FromMinutes(5)) // Cache expires after 5 minutes
//                    .SetAbsoluteExpiration(TimeSpan.FromHours(1))  // Cache expires after 1 hour
//                    .SetPriority(CacheItemPriority.High);

//                // Save the top stories in memory cache
//                _memoryCache.Set("NewStories", newStoryIds, cacheOptions);
//            }
//            return Ok(newStoryIdsCount);
//        }

//        // Get a single story by its ID
//        [HttpGet("story/{id}")]
//        public async Task<IActionResult> GetStory(int id)
//        {
//            var response = await _httpClient.GetStringAsync($"https://hacker-news.firebaseio.com/v0/item/{id}.json?print=pretty");
//            var story = JsonConvert.DeserializeObject<HackerNewsStory>(response);

//            return Ok(story);
//        }
//    }

//    // A model class to represent the Hacker News story
//    public class HackerNewsStory
//    {
//        public int Id { get; set; }
//        public string Title { get; set; }
//        public string Url { get; set; }
//        public int? Score { get; set; }
//        public string By { get; set; }
//    }

//    public class JsonData
//    {
//        public int[] Values { get; set; }
//    }
//}
