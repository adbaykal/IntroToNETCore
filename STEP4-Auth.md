# Authorization and Authentication
Before entering the Authentication part first we need to create a business service that controls the `ApiUser` table for given username and password. 

So let's create an API User on the table by executing below SQL Query in PHPMyadmin.

```sql
INSERT INTO `ApiUser`(`Username`, `Password`, `CreatedTime`, `ModifiedTime`) VALUES ('adbaykal','adbpass',CURRENT_TIME,CURRENT_TIME)
```

After this let's start writing our `ApiUserService`. Add a `Services` folder in the solution and add an `IApiUserService`interface like below.

```csharp
namespace CustomerApi.Services
{
    public interface IApiUserService
    {
        public ApiUser CheckUserCredentials(string username, string password);
    }
}
```

Now lets implement this interface as `ApiUserService`.

```csharp
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

```

After this step we need to inject this service to our DI Container. For this, we need to add below code to `Startup.cs`.

```csharp
services.AddScoped<IApiUserService, ApiUserService>();
```
---
In slides, you can see that a custom Auth Scheme needs a Handler and Options classes in order to act as a middleware. An example of a Authentication service is like below:
```csharp
services.AddAuthentication("BasicAuthentication")
                .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);
```

In this example a custom `BasicAuthenticationHandler` class is used in order to handle the incoming request's authentication process. Let's create a class called `BasicAuthenticationHandler` in the `Middleware` folder and paste below code in it.

```csharp
public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    private readonly IApiUserService _apiUserService;

    public BasicAuthenticationHandler(
        IOptionsMonitor<AuthenticationSchemeOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder,
        ISystemClock clock,
        IApiUserService apiUserService)
        : base(options, logger, encoder, clock)
    {
        _apiUserService = apiUserService;
    }

    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        // skip authentication if endpoint has [AllowAnonymous] attribute
        var endpoint = Context.GetEndpoint();
        if (endpoint?.Metadata?.GetMetadata<IAllowAnonymous>() != null)
            return AuthenticateResult.NoResult();

        if (!Request.Headers.ContainsKey("Authorization"))
            return AuthenticateResult.Fail("Missing Authorization Header");

        ApiUser user = null;
        try
        {
            var authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
            var credentialBytes = Convert.FromBase64String(authHeader.Parameter);
            var credentials = Encoding.UTF8.GetString(credentialBytes).Split(new[] { ':' }, 2);
            var username = credentials[0];
            var password = credentials[1];
            user = _apiUserService.CheckUserCredentials(username, password);
        }
        catch
        {
            return AuthenticateResult.Fail("Invalid Authorization Header");
        }

        if (user == null)
            return AuthenticateResult.Fail("Invalid Username or Password");

        var claims = new[] {
        new Claim(ClaimTypes.NameIdentifier, user.Username.ToString()),
        new Claim(ClaimTypes.Name, user.Username),
    };
        var identity = new ClaimsIdentity(claims, Scheme.Name);
        var principal = new ClaimsPrincipal(identity);
        var ticket = new AuthenticationTicket(principal, Scheme.Name);

        return AuthenticateResult.Success(ticket);
    }
}
```

An auth handler needs to implement `HandleAuthenticateAsync` method in order to control if the request is authenticated or not. The above method extracts the `username` and `password` from header and checks if the username and password is true. If it is true it creates a `AuthenticationTicket` with user `Claim`.

Now let's add the auth service with the given handler to DI and enable the Authentication middleware in `Startup.cs` like below;

```csharp
public void ConfigureServices(IServiceCollection services)
        {
           ...
            services.AddAuthentication("BasicAuthentication")
                .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);
            ...
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            ...
            app.UseAuthentication();
            app.UseAuthorization();
            ...
        }
```

At this point we can start protecting our endpoints by adding `[Authorize]` attribute to actions or controllers. So let's add it to `CustomerController`.

```csharp
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class CustomerController : ControllerBase
{
    ...
}
```

Run the solution and find out if you can call the `ListCustomers` method from Swagger UI. You'll find that all responses return `401-Unauthorized`.

For enabling Swagger UI so that you can enter username and password, you need to change `AddSwaggerGen` methos as below;

```csharp
services.AddSwaggerGen(c =>
    {

        c.AddSecurityDefinition("http", new OpenApiSecurityScheme
        {
            Type = SecuritySchemeType.Http,
            Description = "Basic",
            Name = "Authorization",
            In = ParameterLocation.Header,
            Scheme = "basic"
        });
        c.AddSecurityRequirement( new OpenApiSecurityRequirement()
        {
            { 
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type=ReferenceType.SecurityScheme,
                        Id="http"
                    },
                    Scheme = "basic",
                    Name = "basic",
                    In = ParameterLocation.Header
                }, 
                new List<string>()
            }
        });
    });
```

By adding this code you will find out the Authorize button appears on Swagger UI and you can enter username password for the requests.

### Return to [STEP 3 - Logging](STEP3-Logging.md)