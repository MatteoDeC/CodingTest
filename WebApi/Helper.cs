using System.Net;
using System.Reflection;
using Serilog;

namespace WebApi
{
    public static class Helper
    {
        /// <summary>
        /// Performs a generic (async) Get request
        /// </summary>
        /// <param name="uri"></param>
        /// <returns>json response</returns>
        public static async Task<string> GetAsync(string uri)
        {
            string content = string.Empty;
            try
            {
                // Initializes HttpClient to perform the request
                using (HttpClient client = new HttpClient())
                {
                    // Sends request
                    var result = await client.GetAsync(uri);

                    // Checks the status code
                    result.EnsureSuccessStatusCode();

                    // Reads content
                    content = await result.Content.ReadAsStringAsync();
                }
            }
            catch (Exception ex)
            {
                // Writes log
                Log.Error(ex.Message);

                // Returns empty string in case of error
                content = string.Empty;
            }
            
            return content;
        }
    }

    public class ResponseObject
    {
        // Message
        public string Message { get; set; }

        // Objects list
        public List<Todo> Todos { get; set; }

        // Total objects available
        public int Total { get; set; } = 0;

        // Status
        public Status Status { get; set; }

    }


    // Status
    public enum Status
    {
        Success = 0,
        Error = 1,
    }
}
