using Microsoft.AspNetCore.Mvc;
using NewsAPI;
using NewsAPI.Constants;
using NewsAPI.Models;
using NewsApp.Models;


namespace NewsApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NewsController : ControllerBase
    {
        private readonly ILogger _logger;

        public NewsController(ILogger<NewsController> logger)
        {
            _logger = logger;
            _logger.LogInformation(message: "Constructor for News API called");
        }

        [HttpGet("{key}")]
        public News[] GetNews(string key)
        {
            List<News> news = new List<News>();
            
            var newsApiClient = new NewsApiClient("2692946e58c148c6b22212b5e53743c7");
            var articlesResponse  = newsApiClient.GetEverything(new EverythingRequest
            {
                Q = key,
                SortBy = SortBys.Popularity,
                Language = Languages.EN,
             
            });

            if (articlesResponse.Status == Statuses.Ok)
            {
                foreach(var article in articlesResponse.Articles)
                {
                    news.Add(new News { Title=article.Title , Author=article.Author,Description=article.Description,URL=article.Url,UrlToImage=article.UrlToImage,Content=article.Content});
                }            
            }
            _logger.LogInformation(message: " Data fetched successfully");
            return news.ToArray();
           
        }
       
    }
}
