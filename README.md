# Presenteie

## Requirements
* .NET 5.0+
* MySQL 8.0+

## Database
Run an image of MySQL in Docker: 
```shell
docker run -p 3306:3306 --name presenteie-mysql -e MYSQL_ROOT_PASSWORD=superpassword -e MYSQL_DATABASE=presenteie -e MYSQL_USER=user -e MYSQL_PASSWORD=password -d mysql```

Or set up an instance yourself. Default settings:
* Host: `localhost`
* Port: `3306`
* Database: `presenteie`
* User: `user`
* Password: `password`

You can also change it by editing **appsettings.json**

## Migrations
* You must get the .NET Core CLI tools
    ```shell
    dotnet tool install --global dotnet-ef
    ```
    Or to update the tools, use the `dotnet tool update` command. 


* To create a new migration
    ```shell
    dotnet ef migrations add <migration-name>
    ```

* To apply migrations  
    ```shell
    dotnet ef database update
    ```
  
* Can help
    ```shell
    export PATH="$PATH:$HOME/.dotnet/tools"
    ```
  
### Entity Framework Core reference
* [Getting Started with EF Core](https://docs.microsoft.com/en-us/ef/core/get-started/overview/first-app?tabs=netcore-cli)
* [.NET Core CLI : Includes commands to update, drop, add, remove, and more.](https://docs.microsoft.com/en-us/ef/core/cli/dotnet)
* [Package Manager Console in Visual Studio : Includes commands to update, drop, add, remove, and more.](https://docs.microsoft.com/en-us/ef/core/cli/powershell)

### ASP.NET Core Security reference
* [Use cookie authentication without ASP.NET Core Identity](https://docs.microsoft.com/en-us/aspnet/core/security/authentication/cookie?view=aspnetcore-5.0)
