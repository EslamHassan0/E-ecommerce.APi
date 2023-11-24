using System.Diagnostics;

namespace E_ecommerceAssignment.APi.Middlewares
{
    public class ProfilingMiddleweares
    {
        private readonly ILogger _logger;
        private readonly RequestDelegate _next;
        public ProfilingMiddleweares(RequestDelegate next, ILogger<ProfilingMiddleweares> logger)
        {
            _next = next;
            _logger = logger;
        }
        /*
         معلومات عن ال Request 
        الي رايح والي جي من اهم ال  class in Asp.Net Core 
         */
        public async Task Invoke(HttpContext context)
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();
           await _next(context);
            stopWatch.Stop();
            _logger.LogInformation($"Request`{context.Request.Path}`took `{stopWatch.ElapsedMilliseconds}ms` to execute");
        }
    }
}
