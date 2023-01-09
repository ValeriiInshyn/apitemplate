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
	[UserId] [int] FOREIGN KEY REFERENCES [User](Id),
	[RoleId] [int] FOREIGN KEY REFERENCES [Role](Id)
)

CREATE TABLE [UserInfo]
(
	[Firstname] [varchar](50),
	[LastName] [varchar](50),
	[User] [int] FOREIGN KEY REFERENCES  [User](Id)
	ON DELETE CASCADE
	ON UPDATE CASCADE,

)

```
