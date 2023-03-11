using Newtonsoft.Json;
using Serilog;

namespace WebApi
{
    public class Todo
    {
        #region Fields
        /// <summary>
        /// Todo UserId
        /// </summary>
        [JsonProperty("userId")]
        public uint UserId { get; set; }

        /// <summary>
        /// Todo Id
        /// </summary>
        [JsonProperty("id")]
        public uint Id { get; set; }

        /// <summary>
        /// Todo Title
        /// </summary>
        [JsonProperty("title")]
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// Completed
        /// </summary>
        [JsonProperty("completed")]
        public bool Completed { get; set; }
        #endregion

        /// <summary>
        /// Retrieves and reorganizes all tasks
        /// </summary>
        /// <param name="limit"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public static async Task<TaskResponseObject> GetAllTasks(int limit, int offset)
        {
            // Retrieve all tasks
            TaskResponseObject response = await RetrieveTodos();

            // Paginate
            response.Todos = response.Todos.Skip(offset).Take(limit).ToList();

            // Return final result
            return response;
        }

        /// <summary>
        /// Retrieves all Todos from jsonplaceholder.typicode.com
        /// </summary>
        /// <returns>List of Todo items</returns>
        public static async Task<TaskResponseObject> RetrieveTodos()
        {
            TaskResponseObject response = new TaskResponseObject() { 
                Message = "Success",
                Status = Status.Success
            };

            try
            {
                // Retrieves all todos in json
                string todoListJson = await Helper.GetAsync("https://jsonplaceholder.typicode.com/todos");

                if (!string.IsNullOrEmpty(todoListJson))
                {
                    // Deserializes todos to a list
                    response.Todos = JsonConvert.DeserializeObject<List<Todo>>(todoListJson);
                }
                else
                    throw new Exception("Error while retrieving Tasks from jsonplaceholder");

                // Updates total count
                if(response.Todos != null)
                    response.Total = response.Todos.Count;
            }
            catch (Exception ex)
            {
                // Writes log
                Log.Error(ex.Message);

                response.Message= ex.Message;
                response.Status = Status.Error;
            }

            return response;
        }
    }
}
