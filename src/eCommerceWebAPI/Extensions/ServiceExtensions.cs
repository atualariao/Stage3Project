using eCommerceWebAPI.Configurations;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using eCommerceWebAPI.Handlers;
using Microsoft.AspNetCore.Authentication;

namespace eCommerceWebAPI.Extensions
{
    public static class ServiceExtensions
    {
        //Cors Policy
        public static void ConfigureCors(this IServiceCollection services) =>
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                    builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });

        //IIS Integration
        public static void ConfigureIISIntegration(this IServiceCollection services) =>
            services.Configure<IISOptions>(options =>
            {
                //IIS options here
            });

        //AutoMapper
        public static void ConfigureAutoMapper(this IServiceCollection services) =>
            services.AddAutoMapper(typeof(AutoMapperInitializer).Assembly);

        //Swagger API Versioning
        public static void ConfigureSwaggerVersioning(this IServiceCollection services) =>
            services.AddApiVersioning(options =>
            {
                // reporting api versions will return the headers "api-supported-versions" and "api-deprecated-versions"
                options.ReportApiVersions = true;
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = ApiVersion.Default;
                options.ApiVersionReader = new UrlSegmentApiVersionReader();
            });
        public static void ConfigureSwaggerVersioningExplorer(this IServiceCollection services) =>
            services.AddVersionedApiExplorer(setup =>
            {
                // add the versioned api explorer, which also adds IApiVersionDescriptionProvider service
                // note: the specified format code will format the version as "'v'major[.minor][-status]"
                setup.GroupNameFormat = "'v'VVV";

                // note: this option is only necessary when versioning by url segment. the SubstitutionFormat
                // can also be used to control the format of the API version in route templates
                setup.SubstituteApiVersionInUrl = true;
            });

        //Swagger Documentation
        public static void ConfigureSwaggerDocumentation(this IServiceCollection services) =>
            services.AddSwaggerGen(c =>
            {
                //Swagger Documentation
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "WebApi E-commerce",
                    Description = "A simple example for swagger api information",
                    TermsOfService = new Uri("https://example.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "Adriane Troy U. Alariao",
                        Email = "atualariao.itd.pps@gmail.com",
                        Url = new Uri("https://example.com"),
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Use under OpenApiLicense",
                        Url = new Uri("https://example.com/license"),
                    }
                });

                //Swagger Annotation
                c.EnableAnnotations();

                //Swagger security
                //c.AddSecurityDefinition("basic", new OpenApiSecurityScheme
                //{
                //    Name = "Authorization",
                //    Type = SecuritySchemeType.Http,
                //    Scheme = "basic",
                //    In = ParameterLocation.Header,
                //    Description = "Basic Authorization header using the Bearer scheme."
                //});

                //Swagger Basic Authorization
                //c.AddSecurityRequirement(new OpenApiSecurityRequirement
                //{
                //    {
                //        new OpenApiSecurityScheme
                //        {
                //            Reference = new OpenApiReference
                //            {
                //                Type = ReferenceType.SecurityScheme,
                //                Id = "basic"
                //            }
                //        },
                //        new string[] {}
                //    }
                //});
            });

        //Basic Authentication
        public static void ConfigureBasicAuth(this IServiceCollection services) =>
            services.AddAuthentication("BasicAuthentication")
            .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);
    }
}