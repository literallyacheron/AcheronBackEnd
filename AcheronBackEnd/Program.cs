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
            var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";

            
            var url = $"http://0.0.0.0:{port}";
            builder.WebHost.UseUrls(url);

            var app = builder.Build();

            
            app.MapGet("/", () => "Hello from C# on Google Cloud Run!");

            app.MapPost("/api/data", async (HttpContext context) =>
            {
                
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
