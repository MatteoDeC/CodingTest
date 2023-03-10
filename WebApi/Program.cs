using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Set app builder
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddAuthorization();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Configure logger
            Log.Logger = new LoggerConfiguration()
            .WriteTo.File(@"Logs\log-" + DateTime.Today.ToString("dd-MM-yyyy") + ".txt", fileSizeLimitBytes: 10 * 1000 * 1000)
            .CreateLogger();
            builder.Host.UseSerilog();

            WebApplication app = builder.Build();

            // Configure the app
            if (app.Environment.IsDevelopment())
            {
                // Enable Swagger
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseHttpLogging();
            app.UseHttpsRedirection();
            app.UseSerilogRequestLogging();

            // Set APIs
            app.MapGet("/getAllTasks", GetAllTasks);
            app.MapGet("/getTaskByUser", GetTasksByUser);

            // Run app
            app.Run();



            /// <summary>
            /// Retrieves all tasks, paginated
            /// </summary>
            async Task<ResponseObject> GetAllTasks(HttpContext httpContext, [FromQuery] int limit = 10, int offset = 0)
            {
                ResponseObject response = await Todo.GetAllTasks(limit, offset);
                return response;
            }

            /// <summary>
            /// Retrieves all tasks filtered by user, paginated
            /// </summary>
            async Task<ResponseObject> GetTasksByUser(HttpContext httpContext, [FromQuery] int userId, int limit = 10, int offset = 0)
            {
                ResponseObject response = await User.GetTasksByUser(userId, limit, offset);
                return response;
            }
        }
    }
}