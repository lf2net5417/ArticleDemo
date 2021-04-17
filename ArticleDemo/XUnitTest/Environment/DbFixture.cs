using ArticleDemo;
using ArticleDemo.Models.repository.Article;
using ArticleDemo.Models.repository.Category;
using ArticleDemo.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

namespace XUnitTest.Environment
{
    public class DbFixture
    {
        public static void initDbFixture()
        {
            _MyHost = Host.CreateDefaultBuilder(null)
            .ConfigureAppConfiguration((hostContext, config) =>
            {
                config.SetBasePath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Environment"))
                      .AddJsonFile($"appsettings.json", optional: true);

                config.AddEnvironmentVariables();
            }).ConfigureServices((hostContext, services) =>
            {
                services.AddHealthChecks();
                services.AddHttpContextAccessor();

                var connectionString = hostContext.Configuration.GetSection("ConnectionStrings");

                connectionString["Article"] = connectionString["Article"].Replace("%CONTENTROOTPATH%", AppDomain.CurrentDomain.BaseDirectory);

                services.Configure<DBList>(connectionString);

                #region Service和Reposiotry DI
                services.AddSingleton<ArticleService>();
                services.AddSingleton<CategoryService>();
                services.AddSingleton<ArticleRepo>();
                services.AddSingleton<CategoryRepo>();
                #endregion

                //Entity Framework Core
                services.AddDbContext<UnitTestDBContext>(options =>
                {
                    options.UseSqlServer(hostContext.Configuration.GetConnectionString("Article"));
                });
            })
            .ConfigureWebHostDefaults(webBuilder =>
            {

            })
            .Build();

            var dbContext = _MyHost.Services.GetService<UnitTestDBContext>();
            dbContext.Database.EnsureDeleted();

            dbContext.Database.EnsureCreated();
        }

        private static long _lockFlag = 0; // 0 - free

        private static IHost _MyHost { get; set; }

        public static T GetRequiredService<T>()
        {
            if (Interlocked.CompareExchange(ref _lockFlag, 1, 0) == 0 && _MyHost == null)
            {
                try
                {
                    initDbFixture();
                }
                catch (Exception ex)
                {
                    throw;
                }
                finally
                {
                    Interlocked.Decrement(ref _lockFlag);
                }
            }
            else
            {
                var waitLoop = 10;
                while (Interlocked.Read(ref _lockFlag) > 0 && waitLoop > 0)
                {
                    System.Threading.Thread.Sleep(1000);
                    waitLoop--;
                }
            }

            return _MyHost.Services.GetRequiredService<T>();
        }
    }
}
