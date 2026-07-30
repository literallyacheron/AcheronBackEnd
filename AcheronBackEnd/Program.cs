using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using System;

namespace AcheronBackEnd
{
    internal class Program
    {
        bool active;
        async Task Main(string[] args)
        {
            Console.WriteLine("Deployment successfull! Yay!");
            if (File.Exists("\\shared-test-dep.cyphdep"))
            {
                active = true;
                await waitForInput();
            }
            var builder = WebApplication.CreateBuilder(args);

            // 1. Retrieve the PORT environment variable provided by Cloud Run (defaults to 8080)
            var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";

            // 2. Bind the app to listen on all network interfaces (0.0.0.0) on that port
            var url = $"http://0.0.0.0:{port}";
            builder.WebHost.UseUrls(url);

            var app = builder.Build();

            // 3. Define your HTTP endpoints
            app.MapGet("/", () => "Hello from C# on Google Cloud Run!");

            app.MapPost("/api/data", async (HttpContext context) =>
            {
                // Handle incoming HTTP POST requests here
                return Results.Ok(new { message = "Data received successfully!" });
            });

            app.Run();

        }

        async Task waitForInput()
        {
            switch (active)
            {

            }
        }
    }
}