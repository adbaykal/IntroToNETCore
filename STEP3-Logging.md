# Logging

In previous section we learned that .NET Core has a default Dependency Injection (DI) container. One of the framework dependencies is `Microsoft.Extensions.Logging` library which provides logging interfaces. 

To activate logging we need to inject `ILogger` interface to the class where the logger is needed. In our case we will be logging the number of customers on the list on `ListCustomers()` method.

Let's start by injecting `ILogger` interface to `CustomerController`.

```csharp
using Microsoft.Extensions.Logging;
...

namespace CustomerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IntToNetCoreContext _context;
        private readonly ILogger<CustomerController> _logger;

        public CustomerController(IntToNetCoreContext context, ILogger<CustomerController> logger)
        {
            _context = context;
            _logger = logger;
        }
        ...
    }
}
```

After injecting the logger interface we can log by calling the neccessary level's log method like `LogInformation` or `LogError`. So let's change our `ListCustomer` method like below.

```csharp
[HttpGet]
public List<string> ListCustomers() {

    List<string> customerList = _context.Customer.Select(x => x.Name).ToList();

    //Log customer count
    _logger.LogInformation($"There are {customerList.Count} customers in the bank. ");

    return customerList;
}
```

Now let's try to call our method to see if logging is done or not. After executing out method from swagger or calling below `curl` command;

```bash
curl -X 'GET' \
  'https://localhost:<YOURPORT>/api/Customer' \
  -H 'accept: text/plain'
```
you can see that there are only 2 customers in our Gringotts Bank.

```log
CustomerApi.Controllers.CustomerController: Information: There are 2 customers in the bank. 
```

## Logging Request and Response with a custom Middleware

As you can remember, our head goblin of Gringotts Bank needs to see all request and response logs. We can log one by one on every method of every Controller like on previous example but this causes a lot of duplicated code. 

So we need a structure which intercepts all incoming requests, logs it, then executes the required method and logs the response. We can do this by writing a custom Middleware.

The custom middleware component is like any other .NET class with `Invoke()` or `InvokeAsync()` method. However, in order to execute next middleware in a sequence, it should have `RequestDelegate` type parameter in the constructor. An example middleware is like below;

```csharp
public class RequestResponseLoggingMiddleware
{
    private readonly RequestDelegate _next;
    
    public RequestResponseLoggingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        ...
        await _next(context);
        ...
    }

    
}

```

After writing the custom middleware class, we can write an extension for this middleware in order to use it in `Startup.cs`.

```csharp
public static class RequestResponseLoggingMiddlewareExtensions
{
    public static IApplicationBuilder UseRequestResponseLogging(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<RequestResponseLoggingMiddleware>();
    }
}
```
by using this extension we can add the middleware by writing `app.UseRequestResponseLogging();` in `Startup.cs`.


For the complete 

### Return to [STEP 2 - Creating ASP.NET Core project](STEP2-CreatingASPNETCoreProject.md)