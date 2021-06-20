# Deploying to Docker

So we have completed all requirements and decided to release our software. If you remember our Bank IT admin needs to run the app on Docker platform so that he can publish the container to any cloud vendor.

It is so easy to run your application on Docker. Just Right-click on your CustomerApi project and then go to `Add`->`Docker Support`

![IntoductionToNetCore-STEP5-1](/STEP5/STEP5-1.png)

Visual Studio asks you whether you want a Linux are a Windows machine. Choose `Linux` and VS will create a `Dockerfile` in your solution and try to build it.

After this process you will see that you can run your project on Docker rather than IIS Express.

![IntoductionToNetCore-STEP5-2](/STEP5/STEP5-2.png)

If you dive into the Dockerfile you'll see that it creates a base image and publish our solution into that image. 

```docker
FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["CustomerApi/CustomerApi.csproj", "CustomerApi/"]
RUN dotnet restore "CustomerApi/CustomerApi.csproj"
COPY . .
WORKDIR "/src/CustomerApi"
RUN dotnet build "CustomerApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CustomerApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CustomerApi.dll"]
```

You can read all about VS Docker support in https://docs.microsoft.com/en-us/visualstudio/containers/container-build?view=vs-2019 

### Next Step is [STEP 4 - Authentication & Authorization](STEP4-Auth.md)