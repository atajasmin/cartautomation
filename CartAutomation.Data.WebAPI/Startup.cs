using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CartAutomation.Business.DataOperation.Abstract;
using CartAutomation.Business.DataOperation.Concrete;
using CartAutomation.Data.DataAccess.Abstract;
using CartAutomation.Data.DataAccess.Concrete.EntityFramework;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace CartAutomation.Data.WebAPI
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
            //Businesse a autfacht ile taşınabilir.
            services.AddSingleton<IProductService,ProductManager>();
            services.AddSingleton<IProductDal,EfProductDal>();
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
