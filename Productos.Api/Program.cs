
using Microsoft.EntityFrameworkCore;
using Products.Api.Middleware;
using Products.Business.Contracts;
using Products.Business.Handlers;
using Products.Business.Services;
using Products.BusinessModels.Commands;
using Products.BusinessModels.Product;
using Products.BusinessModels.Queries;
using Products.Data;
using Products.Data.Contracts;
using Products.Data.Repositories;

namespace Products.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Config from appsettings.json
            var configuration = new ConfigurationBuilder()
                .SetBasePath(builder.Environment.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            // Add services to the container.

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddMemoryCache();

            // Configure services
            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped<ICacheService, CacheService>();
            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddScoped<IExternalDiscountService, ExternalDiscountService>();

            // Configure command handlers
            builder.Services.AddScoped<ICommandHandler<CreateProductCommand>, CreateProductCommandHandler>();
            builder.Services.AddScoped<ICommandHandler<UpdateProductCommand>, UpdateProductCommandHandler>();

            // Configure query handlers
            builder.Services.AddScoped<IQueryHandler<GetProductByIdQuery, ProductModel>, GetProductByIdQueryHandler>();

            // Add HttpClient for external services
            builder.Services.AddHttpClient<IExternalDiscountService, ExternalDiscountService>(client =>
            {
                client.BaseAddress = new Uri("http://www.randomnumberapi.com/");
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();
            app.UseMiddleware<RequestLoggingMiddleware>();

            app.UseRouting();


            app.MapControllers();

            app.Run();
        }
    }
}
