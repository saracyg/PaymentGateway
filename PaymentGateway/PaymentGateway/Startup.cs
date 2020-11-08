using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MongoDB.Driver;
using PaymentGateway.ConfigOptions;
using PaymentGateway.Database;
using PaymentGateway.Payment;

namespace PaymentGateway
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
            services.AddHttpClient();
            services.AddScoped<IPaymentProcessor, PaymentProcessor>();
            services.AddScoped<IPaymentRepository, PaymentRepository>();
            services.AddScoped<IPaymentApiClient, PaymentApiClient>();
            services.AddScoped<ICardNumberMaskingService, CardNumberMaskingService>();
            services.AddOptions<DatabaseConfigOptions>()
                .Bind(Configuration.GetSection(DatabaseConfigOptions.DatabaseConfig));
            services.AddOptions<PaymentApiOptions>()
                .Bind(Configuration.GetSection(PaymentApiOptions.PaymentApi));
            services.AddSingleton<IMongoClient>(s => new MongoClient(Configuration.GetConnectionString("MongoDb")));
            services.AddScoped<IDatabaseContext, DatabaseContext>();
            services.AddAutoMapper(typeof(Startup));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
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
        }
    }
}
