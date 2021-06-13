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

### Return to [STEP 2 - Creating ASP.NET Core project](STEP2-CreatingASPNETCoreProject.md)