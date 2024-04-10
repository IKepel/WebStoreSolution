using Microsoft.AspNetCore.Mvc.Versioning;
using StoreRESTAPI.Modules;

namespace StoreRESTAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddApiVersioning(t =>
            {
                t.ApiVersionReader = new UrlSegmentApiVersionReader();
                t.ReportApiVersions = true;
            });
            builder.Services.AddSwaggerGen();
            builder.Services.AddCore(builder.Configuration);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("v1/swagger.json", "Web Store API V1");
                    c.OAuthAppName("Web Store API");
                });
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
