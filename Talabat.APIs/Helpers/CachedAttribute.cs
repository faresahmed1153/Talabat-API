using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text;
using Talabat.Core.Services;

namespace Talabat.APIs.Helpers
{
    public class CachedAttribute : Attribute, IAsyncActionFilter
    {
        private readonly int _timeToLiveInSeconds;
        public CachedAttribute(int timeToLiveInSeconds)
        {
            _timeToLiveInSeconds = timeToLiveInSeconds;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var cachedService = context.HttpContext.RequestServices.GetRequiredService<IResponseCacheService>();

            var cacheKey = GenerateCacheKeyFromRequest(context.HttpContext.Request);

            var cachedResponse = await cachedService.getCachedResponseAsync(cacheKey);

            if (!string.IsNullOrEmpty(cachedResponse))
            {
                var contentResult = new ContentResult()
                {
                    Content = cachedResponse,
                    ContentType = "application/json",
                    StatusCode = 200,
                };

                context.Result = contentResult;
                return;
            }

            var executedEndPointContext = await next();

            if (executedEndPointContext.Result is OkObjectResult okObjectResult)
            {
                await cachedService.CacheResponseAsync(cacheKey, okObjectResult.Value, TimeSpan.FromSeconds(_timeToLiveInSeconds));
            }

        }

        private string GenerateCacheKeyFromRequest(HttpRequest httpRequest)
        {
            // {{url}}/api/Products?pageIndex=1&pageSize=5&sort=name

            var keyBuilder = new StringBuilder();

            keyBuilder.Append(httpRequest.Path); // /api/Products

            foreach (var (key, value) in httpRequest.Query.OrderBy(x=>x.Key))
            {
                keyBuilder.Append($"| {key}-{value}");

                // /api/Products|pageIndex=1
                // /api/Products|pageIndex=1|pageSize=5
                // /api/Products|pageIndex=1|pageSize=5|sort=name
            }

            return keyBuilder.ToString();
        }
    }
}
