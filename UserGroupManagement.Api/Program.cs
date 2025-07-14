using AutoMapper;
using UserGroupManagement.Api.Middlewares;
using UserGroupManagement.Common;
using UserGroupManagement.Service;

namespace UserGroupManagement.Api
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
            builder.Services.AddSwaggerGen();

            
            builder.Services.AddUserGroupManagementServices(builder.Configuration);

            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfiles());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            builder.Services.AddSingleton(mapper);

            //builder.Services.AddAutoMapper(typeof(MappingProfiles).Assembly);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            //app.UseMiddleware<Middlewares.GlobalExceptionMiddleware>();
            app.UseGlobalExceptionMiddleware();

            //app.UseExceptionHandler(error =>
            //{
            //    error.Run(async context =>
            //    {
            //        context.Response.StatusCode = 500;
            //        context.Response.ContentType = "application/json";

            //        var exceptionhandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();

            //        if (exceptionhandlerPathFeature?.Error != null) { 
            //            var errorResponse = new {error = exceptionhandlerPathFeature.Error.Message};
            //            await context.Response.WriteAsJsonAsync(errorResponse);
            //        }
            //    });
            //});

            app.MapControllers();

            app.Run();
        }
    }
}
