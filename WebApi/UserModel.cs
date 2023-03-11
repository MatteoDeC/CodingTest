using Newtonsoft.Json;
using Serilog;

namespace WebApi
{
    public class User
    {
        #region Fields
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("address")]
        public Address Address { get; set; }

        [JsonProperty("phone")]
        public string Phone { get; set; }

        [JsonProperty("website")]
        public string Website { get; set; }

        [JsonProperty("company")]
        public Company Company { get; set; }
        #endregion

        #region Methods
        public static async Task<TaskResponseObject> GetTasksByUser(int userId, int limit, int offset)
        {
            // Retrieve all Todos
            TaskResponseObject response = await Todo.RetrieveTodos();

            // Filter by userId and update total count
            response.Todos = response.Todos.Where(todo => todo.UserId == userId).ToList();
            response.Total = response.Todos.Count;

            // Pagination
            response.Todos = response.Todos.Skip(offset).Take(limit).ToList();

            // Return final result
            return response;
        }
        public static async Task<UserResponseObject> GetUsers(int max)
        {
            List<User> users = new List<User>();
            UserResponseObject response = new UserResponseObject()
            {
                Message = "Success",
                Status = Status.Success
            };

            try
            {
                // Retrieve all users in json
                string usersListJson = await Helper.GetAsync("https://jsonplaceholder.typicode.com/users");

                if (!string.IsNullOrEmpty(usersListJson))
                {
                    // Deserialize users to a list
                    users = JsonConvert.DeserializeObject<List<User>>(usersListJson);

                    // Get a specific number of users if requested
                    if(max > 0)
                        users = users.Take(max).ToList();

                    // Convert list to a lighter dictionary with only required info
                    response.Users = users.ToDictionary(user => user.Id, user => user.Username);
                }
                else
                    throw new Exception("Error while retrieving Users from jsonplaceholder");
            }
            catch (Exception ex)
            {
                // Writes log
                Log.Error(ex.Message);

                response.Message = ex.Message;
                response.Status = Status.Error;
            }

            // Return final result
            return response;
        }
        #endregion
    }

    public class Address
    {
        #region Fields
        [JsonProperty("street")]
        public string Street { get; set; }

        [JsonProperty("suite")]
        public string Suite { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("zipcode")]
        public string Zipcode { get; set; }

        [JsonProperty("geo")]
        public Geo Geo { get; set; }
        #endregion
    }

    public class Geo
    {
        #region Fields
        [JsonProperty("lat")]
        public string Lat { get; set; }

        [JsonProperty("lng")]
        public string Lng { get; set; }
        #endregion
    }

    public class Company
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("catchPhrase")]
        public string CatchPhrase { get; set; }

        [JsonProperty("bs")]
        public string Bs { get; set; }
    }
}
