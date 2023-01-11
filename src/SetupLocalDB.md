# Introducing to setup local database
> This is a description to set up default layout for local database, but you always can use your own one.

## Installing / Getting started

Connect to local database using root link:

```shell
    (localdb)\MSSQLLocalDB
```

Then create database using next template(for data and logs db):

```shell
USE master;
GO
CREATE DATABASE ApiTemplate
ON
( NAME = ApiTemplate_data,  
    FILENAME = 'C:\Database\ApiTemplate_data.mdf') 
LOG ON
( NAME = ApiTemplate_log,  
    FILENAME = 'C:\Database\ApiTemplate_log.ldf');
GO
```


### Initial Configuration

Now you are able to configure your own database and add tables with different configuration.
Down below is the template to create default table layout with user, role, group


```shell
CREATE TABLE [Role]
(
	[Id] [int] IDENTITY(1,1) PRIMARY KEY CLUSTERED,
	[Name] [varchar](50) NOT NULL
)

CREATE TABLE [Group]
(
	[Id] [int] IDENTITY(1,1) PRIMARY KEY CLUSTERED,
	[Name] [varchar](50) NOT NULL
)

CREATE TABLE [User]
(
	[Id] [int] IDENTITY(1,1) PRIMARY KEY CLUSTERED, --Could be used 'bigint' to set get more users, but it would be converted to long in C# and it is also 8 bytys except 4 bytes in default int
	[UserName] [varchar](50) NOT NULL,
	[Password] [varchar](50) CHECK(LEN([password]) >= 8) NOT NULL,
	[Email] [varchar](50) NOT NULL,
	[GroupId] [int] FOREIGN KEY REFERENCES [Group](Id),
	[Role] [int] FOREIGN KEY REFERENCES [Role](Id),
	[IsDeleted] [bit],
	
)

CREATE TABLE [UserToRole]
(
	[UserId] [int] FOREIGN KEY REFERENCES [User](Id) NOT NULL,
	[RoleId] [int] FOREIGN KEY REFERENCES [Role](Id) NOT NULL,
	PRIMARY KEY([UserId], [RoleId])
)

CREATE TABLE [UserInfo]
(
	[Firstname] [varchar](50),
	[LastName] [varchar](50),
	[User] [int] PRIMARY KEY FOREIGN KEY REFERENCES  [User](Id)
	ON DELETE CASCADE
	ON UPDATE CASCADE,

)
```

### Scaffold database
You should change connection string, especially change 'Server root' and 'Initial catalog value' to your database name. 
Also, you can add username and password if your SQL Server wants authentication. 
Then just copy and put it on terminal.
```shell
dotnet ef dbcontext scaffold --project Server.Infrastructure\Server.Infrastructure.csproj --startup-project Server.Presentation\Server.Presentation.csproj --configuration Debug "Server=(localdb)\MSSQLLocalDB;Initial Catalog=ApiTemplate;Trusted_Connection=true;MultipleActiveResultSets=true;TrustServerCertificate=True" "Microsoft.EntityFrameworkCore.SqlServer" --data-annotations --context AppDbContext --context-dir Database --context-namespace  Server.Infrastructure.Database --namespace Server.Domain.Scaffolded  --output-dir ..\Server.Domain\Scaffolded --no-onconfiguring --force
```
