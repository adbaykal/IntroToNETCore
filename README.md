# IntroToNETCore
This tutorial is created for introducing .NET Core enviroment to developers that know .NET Framework applications. This tutorial does not teach C# syntax and object oriented principles but intented to show basic .NET Core cencepts.

> This tutorial uses ASP.NET Core 3.1 version.

## Prerequisites
Before getting into the steps of tutorial, you need to setup some core software that we will use during the tutorial.

Please install these software if you don't have in your computer.

* [Visual Studio Community 2019](https://visualstudio.microsoft.com/vs/community/) - Don't forget to install .NET core components during setup.

* [Docker Desktop for Windows](https://hub.docker.com/editions/community/docker-ce-desktop-windows)

## Presentation

### [You can find the presentation file from here.](https://docs.google.com/presentation/d/e/2PACX-1vQ9VJOmxUXZ5LZtvD_A5NuFbyVvs5fGUJKUj5OXt1Rwgc8_xLWJcHdT724eEpZhFaxU6huiyIWxy0gd/pub?start=false&loop=false&delayms=3000)

## Workshop Project
We will be creating a microservice API for Customer Operations of Gringotts Wizarding Bank

### Requirements:
* As an unregistered user i could not query the API, the endpoint should return “401 Unauthorized”
* As a registered API user i can create customer by posting data to api with my username and password
* As a registered API user i can query customers from api with my username and password
* As the Head Goblin of the bank i need to see all query logs
* As a Bank IT Admin i can run the app on Docker so that i can deploy it to any Kubernetes environment


## Workshop Steps

### [STEP 1 - Creating DB Environment](STEP1-DBEnvironment.md)

### [STEP 2 - Creating ASP.NET Core project](STEP2-CreatingASPNETCoreProject.md)

### [STEP 3 - Logging](STEP3-Logging.md)

### [STEP 4 - Authentication & Authorization](STEP4-Auth.md)

### [STEP 5 - Deploying to Docker](STEP5-DeployingToDocker.md)