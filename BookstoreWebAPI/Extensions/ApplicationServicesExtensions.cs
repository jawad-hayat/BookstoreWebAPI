using BookstoreWebAPI.Errors;
using Core.Interfaces;
using Infrastructure.Data;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using StackExchange.Redis;
using Swashbuckle.AspNetCore.Filters;

namespace BookstoreWebAPI.Extensions
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    In = ParameterLocation.Header
                });
                options.OperationFilter<SecurityRequirementsOperationFilter>();
            });
            services.AddDbContext<ApplicationDbContext>(option => option.UseSqlServer(
                config.GetConnectionString("DefaultConnection")
                ));
            services.AddSingleton<IConnectionMultiplexer>(c =>
            {
                var options = ConfigurationOptions.Parse(config.GetConnectionString("Redis"));
                return ConnectionMultiplexer.Connect(options);
            });
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddSingleton<IResponseCacheService, ResponseCacheService>();
            services.AddScoped<IBasketRepository, BasketRepository>();
            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = actionContext =>
                {
                    var errors = actionContext.ModelState
                    .Where(e => e.Value.Errors.Count > 0)
                    .SelectMany(x => x.Value.Errors)
                    .Select(x => x.ErrorMessage).ToArray();

                    var errorResponse = new ApiValidationErrorResponse
                    {
                        Errors = errors
                    };

                    return new BadRequestObjectResult(errorResponse);
                };
            });
            services.AddCors(options => {
                options.AddPolicy("CorsPolicy", policy =>
                {
                    policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
                });
            });

            return services;







            //services.AddSingleton<IResponseCacheService, ResponseCacheService>();

            //services.AddScoped<ITokenService, TokenService>();

            //services.AddScoped<IOrderService, OrderService>();

            //services.AddScoped<IPaymentService, PaymentService>();

            //services.AddScoped<IUnitOfWork, UnitOfWork>();

            //services.AddScoped<IProductRepository, ProductRepository>();

            //services.AddScoped<IBasketRepository, BasketRepository>();

            //services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            //services.Configure<ApiBehaviorOptions>(options =>
            //{
            //    options.InvalidModelStateResponseFactory = actionContext =>
            //    {
            //        var errors = actionContext.ModelState
            //            .Where(e => e.Value.Errors.Count > 0)
            //            .SelectMany(x => x.Value.Errors)
            //            .Select(x => x.ErrorMessage).ToArray();

            //        var errorResponse = new ApiValidationErrorResponse
            //        {
            //            Errors = errors
            //        };

            //        return new BadRequestObjectResult(errorResponse);
            //    };
            //});

            //return services;
        }
    }
}
