using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using KMChartsUpdater.DAL.Context;
using KMChartsUpdater.BLL.Interfaces;
using KMChartsUpdater.BLL.Services;
using KMChartsUpdater.DAL;
using Microsoft.Extensions.FileProviders;
using System.IO;
using Microsoft.AspNetCore.Http;
using KMChartsUpdater.BLL.Config;
using KMChartsUpdater.BLL.Infrastructure;

namespace KMChartsUpdater.WebAPI
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
            string connection = Configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<ApplicationContext>(
                options => options
                    .UseLazyLoadingProxies()
                    .UseSqlServer(connection)
            );

            services
                .AddTransient<IChartService, ChartService>()
                .AddTransient<IAudioService, AudioService>()
                .AddTransient<IAlbumService, AlbumService>()
                .AddTransient<ISearchService, SearchService>()
                .AddTransient<ISecurityService, SecurityService>()
                .AddTransient<ILabelService, LabelService>()
                .AddTransient<IPlaylistService, PlaylistService>()
                .AddTransient<IAccountService, AccountService>()
                .AddTransient<ITaskService, TaskService>()

                .AddTransient<UnitOfWork>()
            ;

            var config = Configuration
                .GetSection("MyConfig")
                .Get<MyConfig>();

            ApiFactory.SetConfig(config);

            services.AddCors();
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"Uploads")),
                RequestPath = new PathString("/Uploads")
            });

            app.UseCors(builder => builder.AllowAnyOrigin());

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
