using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using NoteAPI.Contexts.Number;
using NoteAPI.Services.Number;
using NoteAPI.Contexts.Blog;
using NoteAPI.Services.Blog;

namespace NoteAPI
{
    public class Startup
    {
        private readonly string _MSSQLConnection = "";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            _MSSQLConnection = Configuration["ConnectionStrings:DefaultConnection"];
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            //Config Culture
            services.Configure<RequestLocalizationOptions>(option => option.DefaultRequestCulture = new RequestCulture("en-us"));

            //Config for return data to same model
            services.AddMvc().AddJsonOptions(option =>
            {
                option.SerializerSettings.ContractResolver = new DefaultContractResolver();
                option.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });

            //Add NumberService
            services.AddDbContext<NumberContext>(option => option.UseSqlServer(_MSSQLConnection));
            services.AddScoped<INumberContext, NumberContext>();
            services.AddScoped<INumberService, NumberService>();

            //Add BlogService
            services.AddDbContext<BlogContext>(option => option.UseSqlServer(_MSSQLConnection));
            services.AddScoped<IBlogContext, BlogContext>();
            services.AddScoped<IBlogService, BlogService>();


            // Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Note API", Version = "v1" });

                // Set the comments path for the Swagger JSON and UI.
                var basePath = AppContext.BaseDirectory;
                var xmlPath = Path.Combine(basePath, "NoteAPI.xml");
                c.IncludeXmlComments(xmlPath);
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

            }
            else
            {
                app.UseHsts();
            }

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
