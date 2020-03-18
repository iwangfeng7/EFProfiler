using Microsoft.AspNetCore.Http;
using Microsoft.OpenApi.Models;
using StackExchange.Profiling;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace EFProfiler
{
    public class InjectMiniProfiler : IDocumentFilter
    {
        private readonly IHttpContextAccessor _httpContext;

        public InjectMiniProfiler(IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext;
        }

        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            swaggerDoc.Info.Contact = new OpenApiContact()
            {
                Name = MiniProfiler.Current.RenderIncludes(_httpContext.HttpContext).ToString(),
            };
        }
    }
}