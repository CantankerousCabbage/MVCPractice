# MVC Practice

## Setup

### SQL server ORM
```shell
    dotnet add package Microsoft.EntityFrameworkCore.SqlServer --version 9.0.0
```

### EF Core Tools local. Manage Migrations
```shell
    dotnet add package Microsoft.EntityFrameworkCore.Tools --version 9.0.0
```

### EF Core Migrations
Create migrations. Autogenerates table creation code based on DBContext and defined models.

```shell
    dotnet ef migrations add "InitialMigration" --output-dir Data/Migrations
```

Run migration code that has been generated.
```shell
    dotnet ef database update
```

### Generator
Generates a view

Install local dependancy
```shell
    dotnet add package Microsoft.VisualStudio.Web.CodeGeneration.Design
```

- ViewName: Name of the view (e.g., Details, Edit, etc.).</br>
- ViewTemplate: Template for the view (e.g., Create, Edit, Details, Delete, or Empty).</br>
- ModelName: The name of the model class associated with the view.</br>
- DbContextName: The name of the database context class.</br>
- ControllerName: The name of the controller (used to organize the views). </br>

Generate view

***Template:***
```shell
dotnet aspnet-codegenerator view ViewName ViewTemplate -m ModelName -dc DbContextName -outDir Views/ControllerName
```

```shell
    dotnet aspnet-codegenerator view Add Details -m Tag -dc MvcDbContext -outDir Views/AdminTags
```
