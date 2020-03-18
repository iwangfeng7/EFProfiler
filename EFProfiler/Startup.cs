using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using StackExchange.Profiling;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;
using System.IO;

namespace EFProfiler
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddCors();
            services.AddControllers();

            services.AddSwaggerGen(option =>
            {
                var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "EFProfiler.xml");
                option.IncludeXmlComments(filePath);
                option.SwaggerDoc("v1", info: new OpenApiInfo()
                {
                    Title = "iwangfeng7APi",
                    Version = "v1",
                });
                option.DocumentFilter<InjectMiniProfiler>();
            });

            services.AddMiniProfiler(options =>
            {
                options.PopupRenderPosition = RenderPosition.BottomLeft;
                options.PopupShowTimeWithChildren = true;
            }).AddEntityFramework();

            services.AddMvc(options =>
            {
                // Because the samples have some MyAction and MyActionAsync duplicates
                // See: https://github.com/aspnet/AspNetCore/issues/8998
                options.SuppressAsyncSuffixInActionNames = false;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            #region ¿çÓòÅäÖÃ

            app.UseCors(builder =>
            {
                builder
                    .SetIsOriginAllowed(_ => true)
                    .AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .WithExposedHeaders("X-MiniProfiler-Ids");
            });

            #endregion ¿çÓòÅäÖÃ

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseMiniProfiler()
               .UseStaticFiles()
               .UseRouting();
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            #region Swagger

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.InjectJavascript("/swagger/ui/Customization.js");
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "api v1");
                options.DocExpansion(DocExpansion.Full);
            });

            #endregion Swagger
        }
    }
}