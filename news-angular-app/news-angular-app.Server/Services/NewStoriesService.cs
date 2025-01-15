using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
namespace news_angular_app.Server.Services
{


    public interface INewStoriesService
    {
        Task<List<int>> GetNewStoriesAsync();
        Task<List<Story>> GetNewestStoriesAsync(int page = 1, int pageSize = 20);
    }

    public class NewStoriesService : INewStoriesService
    {
        private readonly HttpClient _httpClient;
        private readonly IMemoryCache _memoryCache;

        public NewStoriesService(HttpClient httpClient, IMemoryCache memoryCache)
        {
            _httpClient = httpClient;
            _memoryCache = memoryCache;
        }

        public async Task<List<int>> GetNewStoriesAsync()
        {
            const string cacheKey = "newestStories";
            if (_memoryCache.TryGetValue(cacheKey, out List<int> cachedstoryIds))
            {
                return cachedstoryIds;
            }

            //if (!_memoryCache.TryGetValue(cacheKey, out List<int> storyIds))
            //{
                var response = await _httpClient.GetStringAsync("https://hacker-news.firebaseio.com/v0/newstories.json?print=pretty");
                var  storyIds = JsonConvert.DeserializeObject<List<int>>(response);  // ?? new List<int>(); 
              

                //stories = new List<Story>();

                //for (int i = 0; i < 10 && i < storyIds.Count; i++)
                //{
                //    var storyResponse = await _httpClient.GetStringAsync($"https://hacker-news.firebaseio.com/v0/item/{storyIds[i]}.json?print=pretty");
                //    var story = JsonConvert.DeserializeObject<Story>(storyResponse);
                //    stories.Add(story);
                //}

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(5));
                _memoryCache.Set(cacheKey, storyIds, cacheEntryOptions);
            //}


            return storyIds;
        }

        public async Task<List<Story>> GetNewestStoriesAsync(int page = 1, int pageSize = 20)
        {
            // Cache Key to store stories
            var cacheKey = $"newest_stories_page_{page}";
            if (_memoryCache.TryGetValue(cacheKey, out List<Story> cachedStories))
            {
                return cachedStories;
            }

            // Fetch stories from Hacker News API
            var url = $"https://hacker-news.firebaseio.com/v0/newstories.json?print=pretty";
            var response = await _httpClient.GetStringAsync(url);
            var storyIds = JsonConvert.DeserializeObject<List<int>>(response);

            var stories = new List<Story>();
           // for (int i = (page - 1) * pageSize; i < page * pageSize && i < storyIds.Count; i++)
              for (int i =0; i < storyIds.Count; i++)
                {
                var storyId = storyIds[i];
                var storyUrl = $"https://hacker-news.firebaseio.com/v0/item/{storyId}.json?print=pretty";
                var storyResponse = await _httpClient.GetStringAsync(storyUrl);
                var story = JsonConvert.DeserializeObject<Story>(storyResponse);
                stories.Add(story);
            }

            // Cache the result
            _memoryCache.Set(cacheKey, stories, TimeSpan.FromMinutes(10));

            return stories;
        }

    }

    public class Story
    {
        public string Title { get; set; }
        public string Url { get; set; }
    }
   
}
