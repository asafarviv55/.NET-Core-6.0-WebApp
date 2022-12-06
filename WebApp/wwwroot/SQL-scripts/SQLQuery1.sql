USE [master]
GO
/****** Object:  Database [storedb]    Script Date: 12/4/2022 10:19:16 PM ******/
CREATE DATABASE [storedb]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'storedb', FILENAME = N'/var/opt/mssql/data/storedb.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'storedb_log', FILENAME = N'/var/opt/mssql/data/storedb_log.ldf' , SIZE = 73728KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [storedb] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [storedb].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [storedb] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [storedb] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [storedb] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [storedb] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [storedb] SET ARITHABORT OFF 
GO
ALTER DATABASE [storedb] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [storedb] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [storedb] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [storedb] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [storedb] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [storedb] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [storedb] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [storedb] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [storedb] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [storedb] SET  DISABLE_BROKER 
GO
ALTER DATABASE [storedb] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [storedb] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [storedb] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [storedb] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [storedb] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [storedb] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [storedb] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [storedb] SET RECOVERY FULL 
GO
ALTER DATABASE [storedb] SET  MULTI_USER 
GO
ALTER DATABASE [storedb] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [storedb] SET DB_CHAINING OFF 
GO
ALTER DATABASE [storedb] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [storedb] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [storedb] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [storedb] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'storedb', N'ON'
GO
ALTER DATABASE [storedb] SET QUERY_STORE = OFF
GO
USE [storedb]
GO
/****** Object:  UserDefinedTableType [dbo].[IDList]    Script Date: 12/4/2022 10:19:16 PM ******/
CREATE TYPE [dbo].[IDList] AS TABLE(
	[Id] [bigint] NULL
)
GO
/****** Object:  Table [dbo].[imageData]    Script Date: 12/4/2022 10:19:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[imageData](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[product_id] [int] NOT NULL,
	[path] [nvarchar](250) NULL,
 CONSTRAINT [PK_imageData] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[products]    Script Date: 12/4/2022 10:19:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[products](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[code] [int] NULL,
	[name] [nvarchar](50) NULL,
	[description] [nvarchar](50) NULL,
	[sell_date] [date] NULL,
 CONSTRAINT [PK_products] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[imageData]  WITH CHECK ADD  CONSTRAINT [FK_imageData_products1] FOREIGN KEY([product_id])
REFERENCES [dbo].[products] ([id])
GO
ALTER TABLE [dbo].[imageData] CHECK CONSTRAINT [FK_imageData_products1]
GO
/****** Object:  StoredProcedure [dbo].[AddNewProduct]    Script Date: 12/4/2022 10:19:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 

CREATE PROCEDURE [dbo].[AddNewProduct]
 	@code nvarchar(50),
 	@name nvarchar(50),
	@description nvarchar(50),
	@sell_date date,
	@imagePath nvarchar(250)
AS
BEGIN
 
    

	insert into products (code,name,description,sell_date) values( @code,@name,@description,@sell_date );

	declare @id int ;
	set @id = (select id from products where code = @code);

	insert into imageData ([product_id],[path]) values( @id, @imagePath);

END
GO
/****** Object:  StoredProcedure [dbo].[DeleteMultipleProducts]    Script Date: 12/4/2022 10:19:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[DeleteMultipleProducts]
   @IDs IDList READONLY
AS
BEGIN
   SET NOCOUNT ON

   	DELETE FROM imageData where product_id IN (SELECT Id FROM @IDs); 

    DELETE FROM [products] WHERE Id IN (SELECT Id FROM @IDs);

END
GO
/****** Object:  StoredProcedure [dbo].[GetAllProducts]    Script Date: 12/4/2022 10:19:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetAllProducts] 
AS
BEGIN

	

	select p.id ,imgd.path ,  p.code , p.name , p.description ,p.sell_date
	from products p left join imageData imgd
	on p.id = imgd.product_id;


	select count(*) as totalRows 
	from products;


END;
GO
/****** Object:  StoredProcedure [dbo].[GetProductsOrderByColumn]    Script Date: 12/4/2022 10:19:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE   PROCEDURE [dbo].[GetProductsOrderByColumn]
@orderColumn nvarchar(50),
@orderDirection nvarchar(50)

AS
BEGIN

DECLARE @query nVARCHAR(max);
SET @query = 'SELECT p.id,p.code,p.name,p.description,p.sell_date,i.path FROM products p left join imageData i on p.id = i.product_id order by ' + @orderColumn + ' '+ @orderDirection;
EXEC sp_executeSql @query


END
GO
/****** Object:  StoredProcedure [dbo].[Paging]    Script Date: 12/4/2022 10:19:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
CREATE PROCEDURE [dbo].[Paging] 
	 @offset int,
	 @rowsPerPage int
AS
BEGIN
	
	SELECT p.id , p.code , p.description , p.name , p.sell_date , imgd.path
	from products p left join imageData imgd
	on p.id = imgd.product_id
	order by id asc 
	OFFSET @offset ROWS FETCH NEXT @rowsPerPage ROWS ONLY;
 
END
GO
/****** Object:  StoredProcedure [dbo].[RecreateProducts]    Script Date: 12/4/2022 10:19:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[RecreateProducts]
as
begin
 

/****** Object:  Table [dbo].[imageData]    Script Date: 12/4/2022 10:38:04 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[imageData]') AND type in (N'U'))
DROP TABLE  IF  EXISTS [dbo].[imageData];
drop table  IF  EXISTS [dbo].[products];

 
CREATE TABLE [dbo].[products](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[code] [int] NULL,
	[name] [nvarchar](50) NULL,
	[description] [nvarchar](50) NULL,
	[sell_date] [date] NULL,
 CONSTRAINT [PK_products] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]


CREATE TABLE [dbo].[imageData](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[product_id] [int] NOT NULL,
	[path] [nvarchar](250) NULL,
 CONSTRAINT [PK_imageData] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
 

ALTER TABLE [dbo].[imageData]  WITH CHECK ADD  CONSTRAINT [FK_imageData_products1] FOREIGN KEY([product_id])
REFERENCES [dbo].[products] ([id])
 
ALTER TABLE [dbo].[imageData] CHECK CONSTRAINT [FK_imageData_products1]
 

 end
 
GO
/****** Object:  StoredProcedure [dbo].[SavePathOfFile]    Script Date: 12/4/2022 10:19:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE   PROCEDURE [dbo].[SavePathOfFile]
		@path nvarchar(250),
		@productID int
AS
BEGIN
	 
		insert into [dbo].[imageData] ( [product_id] , [path] ) values( @productID , @path );

END
GO
/****** Object:  StoredProcedure [dbo].[SearchByNameOrCode]    Script Date: 12/4/2022 10:19:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE   PROCEDURE [dbo].[SearchByNameOrCode]
 	@str nvarchar(50) 
AS
BEGIN

	select p.id , p.code , p.name , p.description , p.sell_date , i.path
	from products p left join imageData i
	on p.id = i.product_id
	where name like @str+'%' or code like @str+'%';

END
GO
/****** Object:  StoredProcedure [dbo].[UpdateProduct]    Script Date: 12/4/2022 10:19:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
CREATE PROCEDURE [dbo].[UpdateProduct] 
	@id int,
	@code nvarchar(50),
 	@name nvarchar(50),
	@description nvarchar(100),
	@sell_date date,
	@imagePath nvarchar(250)
AS
BEGIN
	 
 	update products 
		set code =  @code,
		name = @name,
		description = @description,
		sell_date = @sell_date
	where id = @id;

	update imageData
	set path = @imagePath
	where product_id = @id;
END
GO
USE [master]
GO
ALTER DATABASE [storedb] SET  READ_WRITE 
GO
