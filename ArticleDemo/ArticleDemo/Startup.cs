using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ArticleDemo.Models.Dal;
using Microsoft.EntityFrameworkCore;
using ArticleDemo.Service;
using ArticleDemo.Models.repository.Article;
using ArticleDemo.Models.repository.Category;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Newtonsoft.Json;
using System.Net.Mime;
using Microsoft.AspNetCore.Http;

namespace ArticleDemo
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
            services.AddControllers();
            services.AddHealthChecks();

            var ConnectionStrings = Configuration.GetSection("ConnectionStrings");
            services.Configure<DBList>(ConnectionStrings);
            //Entity Framework Core
            services.AddDbContext<ArticleDBContext>(options =>
            {
                options.UseSqlServer(ConnectionStrings["Article"]);
            });

            #region Service和Repo DI
            services.AddSingleton<ArticleService>();
            services.AddSingleton<CategoryService>();
            services.AddScoped<ArticleRepo>(); //用singleton的話可能在多人連線時會吃到同樣的資料
            services.AddScoped<CategoryRepo>();
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ArticleDBContext dBContext)
        {
            #region 站台檢核

            app.UseHealthChecks("/chk", new HealthCheckOptions
            {
                ResponseWriter = async (context, report) =>
                {
                    var result = JsonConvert.SerializeObject(
                        new
                        {
                            status = report.Status.ToString(),
                            environment = env.EnvironmentName,
                            name = "Article"

                        });
                    context.Response.ContentType = MediaTypeNames.Application.Json;
                    await context.Response.WriteAsync(result);
                }
            });
            #endregion
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            dBContext.Database.EnsureCreated();
        }
    }
}
