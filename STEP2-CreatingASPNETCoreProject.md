# Creating ASP.NET Core Project

We will follow these steps in workshop:

1. Create a Web Api Project
2. Delete default `Weatherforcast` class and controller
3. Create `CustomerController`
4. Create a dummy `ListCustomers` method

    ```csharp
    [HttpGet]
    public List<string> ListCustomers() {
        return null;
    }
    ```

5. Add `Swashbuckle.AspNetCore` package for Swagger support 

    After installing the package we need to inject Swagger services by inserting below code in `ConfigureServices` method of `Startup.cs`
    ```csharp
    services.AddSwaggerGen();
    ```

    And then we need add below code to `Configure` method of `Startup.cs` to enable Swagger

    ```csharp
    // Enable middleware to serve generated Swagger as a JSON endpoint.
    app.UseSwagger();

    // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
    // specifying the Swagger JSON endpoint.
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Gringotts API V1");
    });
    ```

6. Finally if we try to go `http://localhost:`YOURPORT`/swagger` you will see that SwaggerUI interface shows with our freshly created `ListCustomers` method.

> Don't forget to change YOURPORT with the port your project is running.


## Adding DB Context

We will be adding DB Connection to our application via Entity Framework.

1. First of all we need add `EntityFrameworkCore` package from Package Management Console by entering the below command.

    ```bash
    Install-Package Microsoft.EntityFrameworkCore -Version 3.1.15
    ```

2. Then we need to add a Nuget package for EntityFrameworkCore MySQL Driver called `Pomelo.EntityFrameworkCore.MySql` 

    ```bash
    Install-Package Pomelo.EntityFrameworkCore.MySql -Version 3.2.5
    ```

3. And we need to install package so that we can create models from existing db tables.

    ```bash
    Install-Package Microsoft.EntityFrameworkCore.Tools -Version 3.1.15
    ```

4. After installing all the packages, we can create our model classes from existing tables. This is done by `Scaffold-DbContext` command of the `Microsoft.EntityFrameworkCore.Tools` library. 

    ```bash
    Scaffold-DbContext "server=localhost;port=3306;database=IntToNetCore;user=root;password=mariaforgt" Pomelo.EntityFrameworkCore.MySql -OutputDir Models
    ```
    > Be carefull to enter connection string of the db correctly. Also you can specify the output directory of model classes by giving the path to `-OutputDir` parameter of the command. In our example model classes will be generated to `Models` directory.

    You can see that a `DBContext` class and `DataSet` classes of the tables are created in the given folder.

5. So we have created the object for data management. Now we need to setup our connection string. For that, we need to insert below section to `appsettings.json`file of our project.

    ```json
    ...
    "ConnectionStrings": {
        "DefaultConnection": "server=localhost;port=3306;database=IntToNetCore;user=root;password=mariaforgt"
    },
    ...
    ```
6. In `IntroToNetCoreContext.cs` class you will see below code in the `OnConfiguring` method. We will be injecting the context with our predefined connection string on `Startup`, so delete this code block.

    ```csharp
    if (!optionsBuilder.IsConfigured)
                {
    #warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                    optionsBuilder.UseMySql("server=localhost;port=3306;database=IntToNetCore;user=root;password=mariaforgt", x => x.ServerVersion("10.5.10-mariadb"));
                }
    ```

7. In the final step we will be injecting the DBContext object to our container so that we can access it where ever we want. For this we need to setup the neccessary service like below;

    ```csharp
    using CustomerApi.Models;
    using Microsoft.EntityFrameworkCore;
    ...
    public void ConfigureServices(IServiceCollection services)
        {
           ...

            services.AddDbContext<IntToNetCoreContext>(
                options => options.UseMySql(Configuration.GetConnectionString("DefaultConnection"))
                );
        }
    ...
    ```

8. After setting up the service we can use the context on our Controller like below;

    ```csharp
    public class CustomerController : ControllerBase
    {
        private readonly IntToNetCoreContext _context;
        public CustomerController(IntToNetCoreContext context)
        {
            _context = context;
        }

        [HttpGet]
        public List<string> ListCustomers() {
            return _context.Customer.Select(x=>x.Name).ToList() ;
        }
    }
    ...
    ```

### Return to [STEP 1 - Creating DB Environment](STEP1-DBEnvironment.md)