using CustomerApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerApi.Services
{
    public class ApiUserService : IApiUserService
    {
        private readonly IntToNetCoreContext _context;

        public ApiUserService(IntToNetCoreContext context)
        {
            _context = context;
        }

        public ApiUser CheckUserCredentials(string username, string password)
        {
            if(string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                throw new ArgumentNullException("Username or password cannot be empty");
            }

            ApiUser user = _context.ApiUser.SingleOrDefault(x => x.Username.Equals(username));

            if(user == null || !user.Password.Equals(password))
            {
                throw new KeyNotFoundException("User not found");
            }

            return user;

        }
    }
}
