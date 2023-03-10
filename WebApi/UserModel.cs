using Newtonsoft.Json;

namespace WebApi
{
    public class User
    {
        #region Fields
        [JsonProperty("id")]
        public long Id { get; set; }

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
        public static async Task<ResponseObject> GetTasksByUser(int userId, int limit, int offset)
        {
            // Retrieve all Todos
            ResponseObject response = await Todo.RetrieveTodos();

            // Filter by userId and update total count
            response.Todos = response.Todos.Where(todo => todo.UserId == userId).ToList();
            response.Total = response.Todos.Count;

            // Pagination
            response.Todos = response.Todos.Skip(offset).Take(limit).ToList();

            // Set message
            if (response.Status == 0)
                response.Message = "Success";

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
