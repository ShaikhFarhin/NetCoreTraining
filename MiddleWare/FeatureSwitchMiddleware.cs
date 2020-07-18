using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreTraining.MiddleWare
{
    public class FeatureSwitchMiddleware
    {
        private readonly RequestDelegate _next;
        public FeatureSwitchMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext,IConfiguration config)
        {
            if (httpContext.Request.Path.Value.Contains("/mail"))
            {
                var mail = config.GetSection("mailSettings");

                var report = mail.GetChildren().Select(x => $"{x.Key} : {x.Value}");

                await httpContext.Response.WriteAsync(string.Join("\n", report));

            }
            else
            {
                await _next(httpContext);
            }
        }
    }
}
