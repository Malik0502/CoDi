# CoDi -- Development & Testing Setup

## 1. Start the Development Database

The repository includes a `docker-compose.yml` file that provisions a
PostgreSQL container.

From the root directory of the project, execute:

``` bash
docker compose up -d
```

This starts a PostgreSQL instance with the following configuration:

-   Host: `localhost`
-   Port: `5432`
-   Database: `CoDi_Develop`
-   Username: `CoDi`
-   Password: `CoDi`

It is important to note that the currently used database configuration is hardcoded in the DbContext located in CoDi.Data as CoDiContext.cs
This will be changed in a future update.

Verify that the container is running:

``` bash
docker ps
```
## 2. Run the Application

Start the application:

``` bash
dotnet run
```

Alternatively, run it via your IDE (Visual Studio, Rider, or VS Code).

The application will now connect to the Docker-hosted PostgreSQL
database.

------------------------------------------------------------------------

## 3. Stopping the Development Environment

To stop and remove the container:

``` bash
docker compose down
```

This preserves the database data.

To completely reset the database (including all stored data):

``` bash
docker compose down -v
```

------------------------------------------------------------------------
## 4. Resetting the Database

To fully reset the development database:

``` bash
docker compose down -v
docker compose up -d
dotnet ef database update
```

------------------------------------------------------------------------
