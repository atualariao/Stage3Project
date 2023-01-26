using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using eCommerceWebAPI.Configurations;
using eCommerceWebAPI.Data;
using eCommerceWebAPI.Handlers;
using eCommerceWebAPI.Middleware;
using Serilog;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

//Environment Variable Strings
string msSQLConnectionString = Environment.GetEnvironmentVariable("MSSQL_CONNECTION_STRING") ?? "";


//Autofac DIs
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory())
    .ConfigureContainer<ContainerBuilder>(builder =>
    {
        builder.RegisterModule(new AutofacDependencyInjection());
    });

//Serilog Logger
var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

//Add services to the container.
//Add Controllers
builder.Services.AddControllers();

//AutoMapper
builder.Services.AddAutoMapper(typeof(AutoMapperInitializer).Assembly);

//API Versioning
builder.Services.AddApiVersioning(options =>
{
    // reporting api versions will return the headers "api-supported-versions" and "api-deprecated-versions"
    options.ReportApiVersions = true;
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = ApiVersion.Default;
    options.ApiVersionReader = new UrlSegmentApiVersionReader();
});

builder.Services.AddVersionedApiExplorer(setup =>
{
    // add the versioned api explorer, which also adds IApiVersionDescriptionProvider service
    // note: the specified format code will format the version as "'v'major[.minor][-status]"
    setup.GroupNameFormat = "'v'VVV";

    // note: this option is only necessary when versioning by url segment. the SubstitutionFormat
    // can also be used to control the format of the API version in route templates
    setup.SubstituteApiVersionInUrl = true;
});

//AppDataContext DI
builder.Services.AddDbContextFactory<AppDataContext>(options =>
           options.UseSqlServer(msSQLConnectionString),
           ServiceLifetime.Scoped);

//Auth
//builder.Services.AddAuthentication();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => {
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

builder.Services.AddAuthentication("BasicAuthentication")
    .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger( c => { c.RouteTemplate = "swagger/{documentName}/swagger.json"; });
    app.UseSwaggerUI(options =>
    {
        var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

        // build a swagger endpoint for each discovered API version
        foreach (ApiVersionDescription description in provider.ApiVersionDescriptions)
        {
            options.SwaggerEndpoint(
                    $"/swagger/{description.GroupName}/swagger.json",
                    description.ApiVersion.ToString()
                );
        }
    });
}

app.UseHttpsRedirection();
//Use Middleware
app.UseUserAuth();

//Use Authentication
//app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program
{
    //Expose the Program class for use in integration test project with WebAppFactory<T>
}

