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