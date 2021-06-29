# Creating DB Enviroment

First we are going to install MariaDB 

```bash
docker pull mariadb
```

```bash
docker run -p 127.0.0.1:3306:3306  --name mariafornetcore -e MARIADB_ROOT_PASSWORD=mariafornetcore -d mariadb:latest
```

Now lets create a db from mysql CLI. First you need to login to CLI of the container and enter the following command:

```bash
mysql -p
```

You can enter the password you previosly enter to the `docker run` command

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
docker run --name myadmin -d --link mariafornetcore:db -p 8080:80 phpmyadmin
```

After running these commands successfully you can navigate to PHPMyAdmin via `http://localhost:8080`


## Creating Workshop Tables

After all the setup is done, we can create tables which we will be using on our API.

We will be creating below tables:

- APIUser - This table will contain API user data to manage access to API. For simplicity every user in this table will have full access to all of the methods. 

- Customer - his table will contain customer data whick we will be serving mainly from the API.

You can access create scripts from file called [TableCreate.sql](TableCreate.sql)

### Continue on [STEP 2 - Creating ASP.NET Core project](STEP2-CreatingASPNETCoreProject.md)
