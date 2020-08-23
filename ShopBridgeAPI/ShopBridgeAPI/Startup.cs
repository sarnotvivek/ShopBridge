using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ShopBridge.DAL.Model;
using ShopBridge.Repository.IRepository;
using ShopBridge.Repository.Repository;
using ShopBridge.Service.IService;
using ShopBridge.Service.Service;

namespace ShopBridgeAPI
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
            services.AddCors(o => o.AddPolicy("ShopBridgeCorsPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));
            services.AddMvc();
            services.AddDbContext<ShopBridgeDBContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"), sqlServerOptionsAction: sqlServerOptions =>
            {
                sqlServerOptions.EnableRetryOnFailure(
                  maxRetryCount: 3,
                  maxRetryDelay: TimeSpan.FromSeconds(30),
                  errorNumbersToAdd: null);
            }));

            #region Dependency Injections

            #region Repository
            services.AddScoped<IItemRepository, ItemRepository>();

            #endregion Repository
            #region Service
            services.AddScoped<IItemService, ItemService>();
            #endregion Service

            #endregion Dependency Injections
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("ShopBridgeCorsPolicy");
            app.UseMvc();

        }
    }
}
