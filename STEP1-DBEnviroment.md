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

