CREATE TABLE [dbo].[StorageArticle]
(
	[Id] uniqueidentifier  NOT NULL PRIMARY KEY,
	[Name] VARCHAR(50),
	[Description] VARCHAR(150),
	[Storage_Id] uniqueidentifier,
	constraint Storage_Store foreign key (Storage_Id) references Storage(Id)
)
