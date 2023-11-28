using Microsoft.Extensions.Caching.Memory;
using System.Text;

namespace E_ecommerceAssignment.APi.Middlewares
{
    public class PayloadValidationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IMemoryCache _cache;
        public PayloadValidationMiddleware(RequestDelegate next , IMemoryCache cache)
        {
             _next = next;
            _cache = cache;
        }
      

        public async Task InvokeAsync(HttpContext context)
            {
                // Extract payload from the request
                string payload = await GetRequestBody(context.Request);

                // Generate a unique cache key based on requester and payload
                string cacheKey = $"{context.Request.Host.Value}_{context.Request.Path}_{payload}";

                if (_cache.TryGetValue(cacheKey, out _))
                {
                    // Payload has been used within the specified time frame
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    await context.Response.WriteAsync("Duplicate payload within 10 minutes.");
                    return;
                }

                // Cache the payload with a sliding expiration of 10 minutes
                _cache.Set(cacheKey, DateTime.Now, TimeSpan.FromMinutes(10));

                // Call the next middleware in the pipeline
                await _next(context);
            }

            private async Task<string> GetRequestBody(HttpRequest request)
            {
                using var streamReader = new System.IO.StreamReader(request.Body, Encoding.UTF8);
                return await streamReader.ReadToEndAsync();
            }
        }
       
}
