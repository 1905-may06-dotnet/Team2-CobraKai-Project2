    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.HttpsPolicy;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;

namespace Project.Client
    {
        public class Startup
        {
            public Startup(IConfiguration configuration)
            {
                Configuration = configuration;
            }

		    // This method gets called by the runtime. Use this method to add services to the container.
		    readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

		    public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		    public void ConfigureServices(IServiceCollection services)
		    {

			    services.AddCors(options =>
			    {
				    options.AddPolicy(MyAllowSpecificOrigins,
				    	builder => builder.AllowAnyOrigin()
				    	.AllowAnyMethod()
					    .AllowAnyHeader()
					    .AllowAnyOrigin()
					    .WithHeaders("Accept", "Content-Type", "Origin", "X-My-Header")
					    .WithExposedHeaders("Content-Disposition", "Content-Length"));

                    options.AddPolicy("Mypolicy", builder =>
                    {
                        builder.WithOrigins("*.azurewebsites.net");
                    });
                });

			    services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

			    services.AddDbContext<Project.Data.Entities.CobraKaiDbContext>(optionsAction =>
			    optionsAction.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

			    services.AddScoped<Project.Domain.IRepository, Project.Data.Repository>();
		    }

		    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
              {
                  if (env.IsDevelopment())
                   {
                      app.UseDeveloperExceptionPage();
                      //app.UseMaintainCorsHeaders();
                    }
                    else
                    {
                        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                       app.UseHsts();
                    }

                app.UseCors(MyAllowSpecificOrigins);

                app.UseHttpsRedirection();
                app.UseMvc();
              }
        }
    }
