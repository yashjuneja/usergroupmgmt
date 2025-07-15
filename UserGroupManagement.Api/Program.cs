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

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowBlazorClient",
                    policy => policy
                                //.WithOrigins("https://localhost:7220")
                                .WithOrigins("http://localhost:5137")
                                .AllowAnyHeader()
                                .AllowAnyMethod());
            });

            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new Common.AutoMapper());
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

            app.UseCors("AllowBlazorClient");

            app.MapControllers();

            app.Run();
        }
    }
}
