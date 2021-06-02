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

### Return to [STEP 1 - Creating DB Environment](STEP1-DBEnvironment.md)