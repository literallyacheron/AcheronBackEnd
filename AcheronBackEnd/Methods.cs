using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Http;
using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading;

namespace AcheronBackEnd
{
    internal class Methods
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("int rep back4app deploy successful! Yay!");
            var payload = new { message = "visit ", timestamp = DateTime.UtcNow };
            string url = "https://files.catbox.moe/2ac0az.cyphdep";
            var services = new ServiceCollection();
            services.AddHttpClient();
            var serviceProvider = services.BuildServiceProvider();
            var clientFactory = serviceProvider.GetRequiredService<IHttpClientFactory>();
            bool exists = await url.ExistsAsync(clientFactory);
            switch (exists)
            {
                case true:
                    Console.WriteLine("shared-test-dep online");
                    break;
                case false:
                    Console.WriteLine("shared-test-dep error");
                    break;
            }
            await url.WriteJsonAsync(clientFactory, payload);
            string result = await url.ReadAsync(clientFactory);
            Console.WriteLine(result);
        }
    }
    public static class HttpClientExtensions
    {
        public static async Task<bool> ExistsAsync(this string url, IHttpClientFactory clientFactory)
        {
            try
            {
                var client = clientFactory.CreateClient();
                using var response = await client.GetAsync(url, HttpCompletionOption.ResponseHeadersRead);
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }

        public static async Task<string> ReadAsync(this string url, IHttpClientFactory clientFactory)
        {
            try
            {
                var client = clientFactory.CreateClient();

                // Use a variable name other than 'response' to avoid confusion
                using var httpResponse = await client.GetAsync(url);

                if (httpResponse.IsSuccessStatusCode)
                {
                    // Explicitly read the body content stream as a string
                    string fileContent = await httpResponse.Content.ReadAsStringAsync();
                    return fileContent;
                }

                return $"Error: {(int)httpResponse.StatusCode} {httpResponse.ReasonPhrase}";
            }
            catch (Exception ex)
            {
                return $"Exception: {ex.Message}";
            }
        }
        public static async Task<bool> WriteJsonAsync<T>(this string url, IHttpClientFactory clientFactory, T data)
        {
            try
            {
                var client = clientFactory.CreateClient();

                string jsonString = JsonSerializer.Serialize(data);
                var content = new StringContent(jsonString, Encoding.UTF8, "application/json");

                using var response = await client.PostAsync(url, content);
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }
    }
}
    


