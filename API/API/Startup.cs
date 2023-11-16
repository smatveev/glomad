using API.Helpers;
using Glomad.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        //readonly string policy = "GlomadPolicy";
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddCors(options =>
            //{
            //    options.AddPolicy(policy,
            //                      builder =>
            //                      {
            //                          builder.WithOrigins("http://localhost:8080",
            //                                              "http://localhost:5001",
            //                                              "https://glomad.net")
            //                                              .AllowAnyHeader()
            //                                              .AllowAnyMethod();
            //                      });
            //});

            //services.AddControllers();
            //services.AddSwaggerGen(c =>
            //{
            //    c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });
            //});

            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("AppDbContext")));

            services.AddControllersWithViews();
            
            services.AddHttpClient("AmadeusAPI", client => {
                client.BaseAddress = new System.Uri(Configuration.GetValue<string>("AmadeusAPI:BaseUrl"));
            });
            services.AddScoped<AmadeusAPI>();

            services.AddWebOptimizer(pipeline =>
            {
                pipeline.MinifyCssFiles("/css/**/*.css");
                pipeline.AddCssBundle("/css/site.min.css", "/css/**/*.css");

                pipeline.MinifyJsFiles("/js/site.js", "/js/bootstrap.bundle.js", "/js/moment.min.js");
                pipeline.AddJavaScriptBundle("/js/site.min.js", "/js/**/*.js");
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //app.UseSwagger();
                //app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1"));
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseHttpsRedirection();

            app.UseWebOptimizer();

            app.UseStaticFiles();



            // TODO: research
            app.UseRouting();
            //app.UseCors(policy
            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });   
        }
    }
}
