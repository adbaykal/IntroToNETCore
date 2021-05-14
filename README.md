# IntroToNETCore
This tutorial is created for introducing .NET Core enviroment to developers that know .NET Framework applications. This tutorial does not teach C# syntax and object oriented principles but intented to show basic .NET Core cencepts.

> This tutorial uses ASP.NET Core 3.1 version.

## Prerequisites
Before getting into the steps of tutorial, you need to setup some core software that we will use during the tutorial.

Please install these software if you don't have in your computer.

* [Visual Studio Community 2019](https://visualstudio.microsoft.com/vs/community/) - Don't forget to install .NET core components during setup.

* [Docker Desktop for Windows](https://hub.docker.com/editions/community/docker-ce-desktop-windows)

## Presentation

You can find the presentation file from [here.](https://docs.google.com/presentation/d/e/2PACX-1vQ9VJOmxUXZ5LZtvD_A5NuFbyVvs5fGUJKUj5OXt1Rwgc8_xLWJcHdT724eEpZhFaxU6huiyIWxy0gd/pub?start=false&loop=false&delayms=3000)


## Creating DB Enviroment

First we are going to install MariaDB 

```bash
docker pull mariadb
```

```bash
docker run -p 127.0.0.1:3306:3306  --name mariaforgt -e MARIADB_ROOT_PASSWORD=mariaforgt -d mariadb:tag
```

Now lets create a db from mysql CLI. First you need to login to CLI of the container and enter the following command:

```bash
mysql -p
```

You can enter the password you previosly enter to the `docker run` commend

Now let's create our database;
```sql
CREATE DATABASE IntToNetCore;
```

You can continue managing MariaDB from CLI but not anyone feels comfordable using CLI for all db operations. If you want to install a GUI, you can install PHPMyAdmin with below commands;

To install phpmysql docker image; 
```bash
docker pull phpmyadmin/phpmyadmin
```
```bash
docker run --name myadmin -d --link mariaforgt:db -p 8080:80 phpmyadmin
```

After running these commands successfully you can navigate to PHPMyAdmin via `http://localhost:8080`

