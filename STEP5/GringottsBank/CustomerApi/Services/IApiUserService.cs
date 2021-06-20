using CustomerApi.Models;

namespace CustomerApi.Services
{
    public interface IApiUserService
    {
        public ApiUser CheckUserCredentials(string username, string password);
    }
}
