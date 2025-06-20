using APITask_Framework.Models.Request;
using APITask_Framework.Models.Response;
using RestSharp;

namespace APITask_Framework.Endpoints
{
    public class UsersEndpoints
    {
        private readonly ApiClient _client;
        private UserListResponse UserListResponse => new UserListResponse();

        public UsersEndpoints(ApiClient? client = null)
        {
            _client = client ?? new ApiClient();
        }

        public RestResponse<UserListResponse> GetUsers(int page = 1, int perPage = 6)
        {
            var request = new RestRequest("users", Method.Get)
                .AddQueryParameter(UserListResponse.Page.ToString(), page.ToString())
                .AddQueryParameter(UserListResponse.Per_page.ToString(), perPage.ToString());

            return _client.Execute<UserListResponse>(request);
        }
        public RestResponse<SingleUserResponse> GetUserById(int userId)
        {
            var request = new RestRequest($"users/{userId}", Method.Get);
            return _client.Execute<SingleUserResponse>(request);
        }

        public RestResponse<CreateUserResponse> CreateUser(CreateUserRequest newUser)
        {
            var request = new RestRequest("users", Method.Post);
            request.AddJsonBody(newUser);
            return _client.Execute<CreateUserResponse>(request);
        }

        public RestResponse DeleteUser(string userId)
        {
            var request = new RestRequest($"users/{userId}", Method.Delete);
            return _client.Execute(request);
        }


    }
}
