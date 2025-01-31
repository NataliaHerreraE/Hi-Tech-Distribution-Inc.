USE [master]
GO
/****** Object:  Database [HiTechDistributionDB]    Script Date: 2024-04-21 10:38:51 PM ******/
CREATE DATABASE [HiTechDistributionDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'HiTechDistributionDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQL2019EXPRESS\MSSQL\DATA\HiTechDistributionDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'HiTechDistributionDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQL2019EXPRESS\MSSQL\DATA\HiTechDistributionDB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [HiTechDistributionDB] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [HiTechDistributionDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [HiTechDistributionDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [HiTechDistributionDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [HiTechDistributionDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [HiTechDistributionDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [HiTechDistributionDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [HiTechDistributionDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [HiTechDistributionDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [HiTechDistributionDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [HiTechDistributionDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [HiTechDistributionDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [HiTechDistributionDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [HiTechDistributionDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [HiTechDistributionDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [HiTechDistributionDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [HiTechDistributionDB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [HiTechDistributionDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [HiTechDistributionDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [HiTechDistributionDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [HiTechDistributionDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [HiTechDistributionDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [HiTechDistributionDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [HiTechDistributionDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [HiTechDistributionDB] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [HiTechDistributionDB] SET  MULTI_USER 
GO
ALTER DATABASE [HiTechDistributionDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [HiTechDistributionDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [HiTechDistributionDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [HiTechDistributionDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [HiTechDistributionDB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [HiTechDistributionDB] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [HiTechDistributionDB] SET QUERY_STORE = OFF
GO
USE [HiTechDistributionDB]
GO
/****** Object:  Table [dbo].[Authors]    Script Date: 2024-04-21 10:38:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Authors](
	[AuthorID] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[AuthorID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Books]    Script Date: 2024-04-21 10:38:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Books](
	[BookID] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](100) NOT NULL,
	[ISBN] [numeric](13, 0) NOT NULL,
	[UnitPrice] [decimal](10, 2) NOT NULL,
	[PublicationYear] [int] NOT NULL,
	[PublisherID] [int] NOT NULL,
	[QuantityAvailable] [int] NOT NULL,
	[CategoryID] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[BookID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BooksAuthors]    Script Date: 2024-04-21 10:38:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BooksAuthors](
	[BookID] [int] NOT NULL,
	[AuthorID] [int] NOT NULL,
 CONSTRAINT [PK_BooksAuthors] PRIMARY KEY CLUSTERED 
(
	[BookID] ASC,
	[AuthorID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BooksCategories]    Script Date: 2024-04-21 10:38:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BooksCategories](
	[CategoryID] [int] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[CategoryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Customers]    Script Date: 2024-04-21 10:38:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customers](
	[CustomerID] [int] IDENTITY(1,1) NOT NULL,
	[CustomerName] [nvarchar](100) NOT NULL,
	[Street] [nvarchar](100) NOT NULL,
	[City] [nvarchar](100) NOT NULL,
	[PostalCode] [nvarchar](10) NOT NULL,
	[PhoneNumber] [nvarchar](20) NOT NULL,
	[FaxNumber] [nvarchar](20) NULL,
	[CreditLimit] [decimal](10, 2) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[CustomerID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Employees]    Script Date: 2024-04-21 10:38:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employees](
	[EmployeeID] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](100) NOT NULL,
	[JobId] [int] NULL,
	[StatusId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[EmployeeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Jobs]    Script Date: 2024-04-21 10:38:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Jobs](
	[JobId] [int] IDENTITY(1,1) NOT NULL,
	[JobTitle] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[JobId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Orders]    Script Date: 2024-04-21 10:38:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Orders](
	[OrderID] [int] IDENTITY(1,1) NOT NULL,
	[CustomerID] [int] NOT NULL,
	[EmployeeID] [int] NOT NULL,
	[OrderDate] [date] NOT NULL,
	[OrderType] [nvarchar](50) NOT NULL,
	[StatusId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[OrderID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrdersDetails]    Script Date: 2024-04-21 10:38:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrdersDetails](
	[OrderID] [int] NOT NULL,
	[ItemSequencial] [int] NOT NULL,
	[BookID] [int] NULL,
	[Quantity] [int] NOT NULL,
	[CurrentUnitPrice] [decimal](10, 2) NOT NULL,
	[PriceTotal] [decimal](10, 2) NOT NULL,
 CONSTRAINT [PK_OrdersDetails] PRIMARY KEY CLUSTERED 
(
	[OrderID] ASC,
	[ItemSequencial] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Publishers]    Script Date: 2024-04-21 10:38:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Publishers](
	[PublisherID] [int] IDENTITY(1,1) NOT NULL,
	[PublisherName] [nvarchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[PublisherID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Status]    Script Date: 2024-04-21 10:38:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Status](
	[StatusId] [int] IDENTITY(1,1) NOT NULL,
	[State] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Status] PRIMARY KEY CLUSTERED 
(
	[StatusId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserAccounts]    Script Date: 2024-04-21 10:38:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserAccounts](
	[UserId] [int] NOT NULL,
	[Password] [nvarchar](50) NOT NULL,
	[DateCreated] [date] NOT NULL,
	[DateModified] [date] NOT NULL,
	[StatusId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Authors] ON 

INSERT [dbo].[Authors] ([AuthorID], [FirstName], [LastName], [Email]) VALUES (1, N'Andy', N'Hunt', N'ahunt@email.com')
INSERT [dbo].[Authors] ([AuthorID], [FirstName], [LastName], [Email]) VALUES (2, N'Brian', N'Kernighan', N'bkernighan@email.com')
INSERT [dbo].[Authors] ([AuthorID], [FirstName], [LastName], [Email]) VALUES (3, N'Alice', N'Johnson', N'alicejohnson@email.com')
INSERT [dbo].[Authors] ([AuthorID], [FirstName], [LastName], [Email]) VALUES (5, N'Erich', N'Gamma', N'egamma@email.com')
INSERT [dbo].[Authors] ([AuthorID], [FirstName], [LastName], [Email]) VALUES (6, N'Steve', N'McConnell', N'smcconnell@email.com')
SET IDENTITY_INSERT [dbo].[Authors] OFF
GO
SET IDENTITY_INSERT [dbo].[Books] ON 

INSERT [dbo].[Books] ([BookID], [Title], [ISBN], [UnitPrice], [PublicationYear], [PublisherID], [QuantityAvailable], [CategoryID]) VALUES (1, N'Learning C#', CAST(1234567890123 AS Numeric(13, 0)), CAST(79.99 AS Decimal(10, 2)), 2022, 1, 16, 1)
INSERT [dbo].[Books] ([BookID], [Title], [ISBN], [UnitPrice], [PublicationYear], [PublisherID], [QuantityAvailable], [CategoryID]) VALUES (2, N'Data Structures', CAST(1234567890124 AS Numeric(13, 0)), CAST(49.99 AS Decimal(10, 2)), 2021, 1, 24, 2)
INSERT [dbo].[Books] ([BookID], [Title], [ISBN], [UnitPrice], [PublicationYear], [PublisherID], [QuantityAvailable], [CategoryID]) VALUES (3, N'Introduction to Algorithms', CAST(1234567890125 AS Numeric(13, 0)), CAST(39.99 AS Decimal(10, 2)), 2020, 2, 90, 3)
INSERT [dbo].[Books] ([BookID], [Title], [ISBN], [UnitPrice], [PublicationYear], [PublisherID], [QuantityAvailable], [CategoryID]) VALUES (8, N'Introduction to Java', CAST(1234567890126 AS Numeric(13, 0)), CAST(25.99 AS Decimal(10, 2)), 2018, 3, 23, 4)
INSERT [dbo].[Books] ([BookID], [Title], [ISBN], [UnitPrice], [PublicationYear], [PublisherID], [QuantityAvailable], [CategoryID]) VALUES (9, N'Clean Code', CAST(1234567890127 AS Numeric(13, 0)), CAST(46.99 AS Decimal(10, 2)), 2020, 4, 18, 4)
INSERT [dbo].[Books] ([BookID], [Title], [ISBN], [UnitPrice], [PublicationYear], [PublisherID], [QuantityAvailable], [CategoryID]) VALUES (10, N'Code complete', CAST(1234567890128 AS Numeric(13, 0)), CAST(64.99 AS Decimal(10, 2)), 2021, 3, 40, 3)
INSERT [dbo].[Books] ([BookID], [Title], [ISBN], [UnitPrice], [PublicationYear], [PublisherID], [QuantityAvailable], [CategoryID]) VALUES (11, N'Design Patterns', CAST(1234567890129 AS Numeric(13, 0)), CAST(59.99 AS Decimal(10, 2)), 2017, 2, 50, 2)
INSERT [dbo].[Books] ([BookID], [Title], [ISBN], [UnitPrice], [PublicationYear], [PublisherID], [QuantityAvailable], [CategoryID]) VALUES (12, N'Mastering Algorithms with C', CAST(1234567890230 AS Numeric(13, 0)), CAST(39.99 AS Decimal(10, 2)), 1999, 1, 58, 1)
INSERT [dbo].[Books] ([BookID], [Title], [ISBN], [UnitPrice], [PublicationYear], [PublisherID], [QuantityAvailable], [CategoryID]) VALUES (15, N'The Pragmatic Programmer', CAST(369852147159 AS Numeric(13, 0)), CAST(39.99 AS Decimal(10, 2)), 2006, 1, 68, 3)
SET IDENTITY_INSERT [dbo].[Books] OFF
GO
INSERT [dbo].[BooksAuthors] ([BookID], [AuthorID]) VALUES (1, 1)
INSERT [dbo].[BooksAuthors] ([BookID], [AuthorID]) VALUES (1, 2)
INSERT [dbo].[BooksAuthors] ([BookID], [AuthorID]) VALUES (2, 2)
INSERT [dbo].[BooksAuthors] ([BookID], [AuthorID]) VALUES (3, 5)
INSERT [dbo].[BooksAuthors] ([BookID], [AuthorID]) VALUES (8, 3)
INSERT [dbo].[BooksAuthors] ([BookID], [AuthorID]) VALUES (9, 2)
INSERT [dbo].[BooksAuthors] ([BookID], [AuthorID]) VALUES (9, 6)
INSERT [dbo].[BooksAuthors] ([BookID], [AuthorID]) VALUES (10, 2)
INSERT [dbo].[BooksAuthors] ([BookID], [AuthorID]) VALUES (11, 3)
INSERT [dbo].[BooksAuthors] ([BookID], [AuthorID]) VALUES (12, 3)
INSERT [dbo].[BooksAuthors] ([BookID], [AuthorID]) VALUES (12, 5)
INSERT [dbo].[BooksAuthors] ([BookID], [AuthorID]) VALUES (15, 6)
GO
SET IDENTITY_INSERT [dbo].[BooksCategories] ON 

INSERT [dbo].[BooksCategories] ([CategoryID], [Description]) VALUES (1, N'Computer Science')
INSERT [dbo].[BooksCategories] ([CategoryID], [Description]) VALUES (2, N'Information Technology')
INSERT [dbo].[BooksCategories] ([CategoryID], [Description]) VALUES (3, N'Data Science')
INSERT [dbo].[BooksCategories] ([CategoryID], [Description]) VALUES (4, N'Programming')
INSERT [dbo].[BooksCategories] ([CategoryID], [Description]) VALUES (5, N'Algorithms')
SET IDENTITY_INSERT [dbo].[BooksCategories] OFF
GO
SET IDENTITY_INSERT [dbo].[Customers] ON 

INSERT [dbo].[Customers] ([CustomerID], [CustomerName], [Street], [City], [PostalCode], [PhoneNumber], [FaxNumber], [CreditLimit]) VALUES (1, N'Quebec College', N'123 College St', N'Montreal', N'Q1A 2B3', N'123-456-7890', N'123-456-7899', CAST(10000.00 AS Decimal(10, 2)))
INSERT [dbo].[Customers] ([CustomerID], [CustomerName], [Street], [City], [PostalCode], [PhoneNumber], [FaxNumber], [CreditLimit]) VALUES (2, N'Montreal University', N'321 University Ave', N'Montreal', N'Q2B 3C4', N'321-654-0987', N'321-654-0986', CAST(20000.00 AS Decimal(10, 2)))
INSERT [dbo].[Customers] ([CustomerID], [CustomerName], [Street], [City], [PostalCode], [PhoneNumber], [FaxNumber], [CreditLimit]) VALUES (3, N'Laval College', N'123 College St', N'Montreal', N'H3S 1X4', N'(123)456-7890', N'(123)456-7899', CAST(10000.00 AS Decimal(10, 2)))
INSERT [dbo].[Customers] ([CustomerID], [CustomerName], [Street], [City], [PostalCode], [PhoneNumber], [FaxNumber], [CreditLimit]) VALUES (4, N'College LaSalle', N'2000 Saint Catherine St W', N'Montreal', N'H3H 2T2', N'(514)939-2006', N'(514)939-2006', CAST(25000.00 AS Decimal(10, 2)))
INSERT [dbo].[Customers] ([CustomerID], [CustomerName], [Street], [City], [PostalCode], [PhoneNumber], [FaxNumber], [CreditLimit]) VALUES (9, N'McGill University', N'845 Sherbrooke St W', N'Montreal', N'H3A 0G4', N'(123)589-9999', N'(123)589-2569', CAST(30000.00 AS Decimal(10, 2)))
SET IDENTITY_INSERT [dbo].[Customers] OFF
GO
SET IDENTITY_INSERT [dbo].[Employees] ON 

INSERT [dbo].[Employees] ([EmployeeID], [FirstName], [LastName], [Email], [JobId], [StatusId]) VALUES (2, N'Alex', N'Clark', N'alex.clark@gmail.com', 1, 6)
INSERT [dbo].[Employees] ([EmployeeID], [FirstName], [LastName], [Email], [JobId], [StatusId]) VALUES (3, N'Sophia', N'Martinez', N'sophia.martinez@email.com', 1, 7)
INSERT [dbo].[Employees] ([EmployeeID], [FirstName], [LastName], [Email], [JobId], [StatusId]) VALUES (5, N'Anna', N'Hansen', N'anna@gmail.com', 1, 6)
INSERT [dbo].[Employees] ([EmployeeID], [FirstName], [LastName], [Email], [JobId], [StatusId]) VALUES (6, N'Paulo', N'Oliveira', N'paulo@gmail.com', 4, 7)
INSERT [dbo].[Employees] ([EmployeeID], [FirstName], [LastName], [Email], [JobId], [StatusId]) VALUES (7, N'Natalia', N'Herrera', N'natalia.h@gmail.com', 4, 7)
INSERT [dbo].[Employees] ([EmployeeID], [FirstName], [LastName], [Email], [JobId], [StatusId]) VALUES (9, N'Henry', N'Brown', N'hbrown@email.com', 4, 6)
INSERT [dbo].[Employees] ([EmployeeID], [FirstName], [LastName], [Email], [JobId], [StatusId]) VALUES (10, N'Thomas', N'Moore', N'tmoore@email.com', 2, 6)
INSERT [dbo].[Employees] ([EmployeeID], [FirstName], [LastName], [Email], [JobId], [StatusId]) VALUES (11, N'Peter', N'Wang', N'pwang@email.com', 3, 6)
INSERT [dbo].[Employees] ([EmployeeID], [FirstName], [LastName], [Email], [JobId], [StatusId]) VALUES (12, N'Mary', N'Brown', N'mbrown@email.com', 1, 6)
INSERT [dbo].[Employees] ([EmployeeID], [FirstName], [LastName], [Email], [JobId], [StatusId]) VALUES (13, N'Jennifer', N'Bouchard', N'jbouchard@email.com', 1, 6)
INSERT [dbo].[Employees] ([EmployeeID], [FirstName], [LastName], [Email], [JobId], [StatusId]) VALUES (14, N'Nelson', N'Penha', N'npenha@email.com', 4, 7)
INSERT [dbo].[Employees] ([EmployeeID], [FirstName], [LastName], [Email], [JobId], [StatusId]) VALUES (18, N'Hamilton', N'Gomez', N'hgomez@email.com', 4, 7)
SET IDENTITY_INSERT [dbo].[Employees] OFF
GO
SET IDENTITY_INSERT [dbo].[Jobs] ON 

INSERT [dbo].[Jobs] ([JobId], [JobTitle]) VALUES (1, N'Order Clerk')
INSERT [dbo].[Jobs] ([JobId], [JobTitle]) VALUES (2, N'Sales Manager')
INSERT [dbo].[Jobs] ([JobId], [JobTitle]) VALUES (3, N'Inventory Controller')
INSERT [dbo].[Jobs] ([JobId], [JobTitle]) VALUES (4, N'MIS Manager')
SET IDENTITY_INSERT [dbo].[Jobs] OFF
GO
SET IDENTITY_INSERT [dbo].[Orders] ON 

INSERT [dbo].[Orders] ([OrderID], [CustomerID], [EmployeeID], [OrderDate], [OrderType], [StatusId]) VALUES (3, 1, 2, CAST(N'2024-02-01' AS Date), N'Email', 9)
INSERT [dbo].[Orders] ([OrderID], [CustomerID], [EmployeeID], [OrderDate], [OrderType], [StatusId]) VALUES (4, 2, 3, CAST(N'2024-03-10' AS Date), N'Phone', 5)
INSERT [dbo].[Orders] ([OrderID], [CustomerID], [EmployeeID], [OrderDate], [OrderType], [StatusId]) VALUES (5, 9, 13, CAST(N'2024-04-18' AS Date), N'Email', 5)
INSERT [dbo].[Orders] ([OrderID], [CustomerID], [EmployeeID], [OrderDate], [OrderType], [StatusId]) VALUES (6, 3, 7, CAST(N'2024-02-09' AS Date), N'Email', 9)
SET IDENTITY_INSERT [dbo].[Orders] OFF
GO
INSERT [dbo].[OrdersDetails] ([OrderID], [ItemSequencial], [BookID], [Quantity], [CurrentUnitPrice], [PriceTotal]) VALUES (3, 1, 1, 7, CAST(79.99 AS Decimal(10, 2)), CAST(559.93 AS Decimal(10, 2)))
INSERT [dbo].[OrdersDetails] ([OrderID], [ItemSequencial], [BookID], [Quantity], [CurrentUnitPrice], [PriceTotal]) VALUES (3, 2, 2, 1, CAST(49.99 AS Decimal(10, 2)), CAST(49.99 AS Decimal(10, 2)))
INSERT [dbo].[OrdersDetails] ([OrderID], [ItemSequencial], [BookID], [Quantity], [CurrentUnitPrice], [PriceTotal]) VALUES (3, 3, 8, 5, CAST(25.99 AS Decimal(10, 2)), CAST(129.95 AS Decimal(10, 2)))
INSERT [dbo].[OrdersDetails] ([OrderID], [ItemSequencial], [BookID], [Quantity], [CurrentUnitPrice], [PriceTotal]) VALUES (4, 1, 2, 1, CAST(49.99 AS Decimal(10, 2)), CAST(49.99 AS Decimal(10, 2)))
INSERT [dbo].[OrdersDetails] ([OrderID], [ItemSequencial], [BookID], [Quantity], [CurrentUnitPrice], [PriceTotal]) VALUES (4, 2, 12, 1, CAST(39.99 AS Decimal(10, 2)), CAST(39.99 AS Decimal(10, 2)))
GO
SET IDENTITY_INSERT [dbo].[Publishers] ON 

INSERT [dbo].[Publishers] ([PublisherID], [PublisherName]) VALUES (1, N'Premier Press')
INSERT [dbo].[Publishers] ([PublisherID], [PublisherName]) VALUES (2, N'Wrox')
INSERT [dbo].[Publishers] ([PublisherID], [PublisherName]) VALUES (3, N'Murach')
INSERT [dbo].[Publishers] ([PublisherID], [PublisherName]) VALUES (4, N'Prentice Hall')
SET IDENTITY_INSERT [dbo].[Publishers] OFF
GO
SET IDENTITY_INSERT [dbo].[Status] ON 

INSERT [dbo].[Status] ([StatusId], [State]) VALUES (1, N'Active')
INSERT [dbo].[Status] ([StatusId], [State]) VALUES (2, N'InActive')
INSERT [dbo].[Status] ([StatusId], [State]) VALUES (3, N'In Process')
INSERT [dbo].[Status] ([StatusId], [State]) VALUES (4, N'Completed')
INSERT [dbo].[Status] ([StatusId], [State]) VALUES (5, N'Pending')
INSERT [dbo].[Status] ([StatusId], [State]) VALUES (6, N'Full-Time')
INSERT [dbo].[Status] ([StatusId], [State]) VALUES (7, N'Part-Time')
INSERT [dbo].[Status] ([StatusId], [State]) VALUES (8, N'Contracted')
INSERT [dbo].[Status] ([StatusId], [State]) VALUES (9, N'Canceled')
INSERT [dbo].[Status] ([StatusId], [State]) VALUES (10, N'Shipped')
SET IDENTITY_INSERT [dbo].[Status] OFF
GO
INSERT [dbo].[UserAccounts] ([UserId], [Password], [DateCreated], [DateModified], [StatusId]) VALUES (2, N'Pass123!', CAST(N'2024-03-15' AS Date), CAST(N'2024-03-15' AS Date), 1)
INSERT [dbo].[UserAccounts] ([UserId], [Password], [DateCreated], [DateModified], [StatusId]) VALUES (3, N'Senha@2024!', CAST(N'2024-03-15' AS Date), CAST(N'2024-03-22' AS Date), 1)
INSERT [dbo].[UserAccounts] ([UserId], [Password], [DateCreated], [DateModified], [StatusId]) VALUES (5, N'Pass123!', CAST(N'2024-03-23' AS Date), CAST(N'2024-03-23' AS Date), 1)
INSERT [dbo].[UserAccounts] ([UserId], [Password], [DateCreated], [DateModified], [StatusId]) VALUES (6, N'Pass123!', CAST(N'2024-03-23' AS Date), CAST(N'2024-03-23' AS Date), 1)
INSERT [dbo].[UserAccounts] ([UserId], [Password], [DateCreated], [DateModified], [StatusId]) VALUES (7, N'Pass123!', CAST(N'2024-04-10' AS Date), CAST(N'2024-04-10' AS Date), 1)
INSERT [dbo].[UserAccounts] ([UserId], [Password], [DateCreated], [DateModified], [StatusId]) VALUES (9, N'Pass123!', CAST(N'2024-04-10' AS Date), CAST(N'2024-04-10' AS Date), 1)
INSERT [dbo].[UserAccounts] ([UserId], [Password], [DateCreated], [DateModified], [StatusId]) VALUES (10, N'Pass123!', CAST(N'2024-04-10' AS Date), CAST(N'2024-04-10' AS Date), 1)
INSERT [dbo].[UserAccounts] ([UserId], [Password], [DateCreated], [DateModified], [StatusId]) VALUES (11, N'Pass123!', CAST(N'2024-04-10' AS Date), CAST(N'2024-04-10' AS Date), 1)
INSERT [dbo].[UserAccounts] ([UserId], [Password], [DateCreated], [DateModified], [StatusId]) VALUES (12, N'Pass123!', CAST(N'2024-04-10' AS Date), CAST(N'2024-04-10' AS Date), 1)
INSERT [dbo].[UserAccounts] ([UserId], [Password], [DateCreated], [DateModified], [StatusId]) VALUES (13, N'Pass123!', CAST(N'2024-04-10' AS Date), CAST(N'2024-04-10' AS Date), 1)
INSERT [dbo].[UserAccounts] ([UserId], [Password], [DateCreated], [DateModified], [StatusId]) VALUES (14, N'Senha!123', CAST(N'2024-04-20' AS Date), CAST(N'2024-04-20' AS Date), 2)
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Authors_Email]    Script Date: 2024-04-21 10:38:52 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Authors_Email] ON [dbo].[Authors]
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_ISBN]    Script Date: 2024-04-21 10:38:52 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_ISBN] ON [dbo].[Books]
(
	[ISBN] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [CK_OrdersDetails_UniqueBookIDPerOrder]    Script Date: 2024-04-21 10:38:52 PM ******/
ALTER TABLE [dbo].[OrdersDetails] ADD  CONSTRAINT [CK_OrdersDetails_UniqueBookIDPerOrder] UNIQUE NONCLUSTERED 
(
	[OrderID] ASC,
	[BookID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Books]  WITH CHECK ADD  CONSTRAINT [FK_Books_CategoryID] FOREIGN KEY([CategoryID])
REFERENCES [dbo].[BooksCategories] ([CategoryID])
GO
ALTER TABLE [dbo].[Books] CHECK CONSTRAINT [FK_Books_CategoryID]
GO
ALTER TABLE [dbo].[Books]  WITH CHECK ADD  CONSTRAINT [FK_Books_PublisherID] FOREIGN KEY([PublisherID])
REFERENCES [dbo].[Publishers] ([PublisherID])
GO
ALTER TABLE [dbo].[Books] CHECK CONSTRAINT [FK_Books_PublisherID]
GO
ALTER TABLE [dbo].[BooksAuthors]  WITH CHECK ADD  CONSTRAINT [FK_BooksAuthors_AuthorID] FOREIGN KEY([AuthorID])
REFERENCES [dbo].[Authors] ([AuthorID])
GO
ALTER TABLE [dbo].[BooksAuthors] CHECK CONSTRAINT [FK_BooksAuthors_AuthorID]
GO
ALTER TABLE [dbo].[BooksAuthors]  WITH CHECK ADD  CONSTRAINT [FK_BooksAuthors_BookID] FOREIGN KEY([BookID])
REFERENCES [dbo].[Books] ([BookID])
GO
ALTER TABLE [dbo].[BooksAuthors] CHECK CONSTRAINT [FK_BooksAuthors_BookID]
GO
ALTER TABLE [dbo].[Employees]  WITH CHECK ADD  CONSTRAINT [FK_Employees_JobId] FOREIGN KEY([JobId])
REFERENCES [dbo].[Jobs] ([JobId])
GO
ALTER TABLE [dbo].[Employees] CHECK CONSTRAINT [FK_Employees_JobId]
GO
ALTER TABLE [dbo].[Employees]  WITH CHECK ADD  CONSTRAINT [FK_Employees_StatusId] FOREIGN KEY([StatusId])
REFERENCES [dbo].[Status] ([StatusId])
GO
ALTER TABLE [dbo].[Employees] CHECK CONSTRAINT [FK_Employees_StatusId]
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [FK_ORDERS_CustomerID] FOREIGN KEY([CustomerID])
REFERENCES [dbo].[Customers] ([CustomerID])
GO
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [FK_ORDERS_CustomerID]
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [FK_ORDERS_EmployeeID] FOREIGN KEY([EmployeeID])
REFERENCES [dbo].[Employees] ([EmployeeID])
GO
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [FK_ORDERS_EmployeeID]
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [FK_Orders_Status] FOREIGN KEY([StatusId])
REFERENCES [dbo].[Status] ([StatusId])
GO
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [FK_Orders_Status]
GO
ALTER TABLE [dbo].[OrdersDetails]  WITH CHECK ADD  CONSTRAINT [FK_OrdersDetails_BookID] FOREIGN KEY([BookID])
REFERENCES [dbo].[Books] ([BookID])
GO
ALTER TABLE [dbo].[OrdersDetails] CHECK CONSTRAINT [FK_OrdersDetails_BookID]
GO
ALTER TABLE [dbo].[OrdersDetails]  WITH CHECK ADD  CONSTRAINT [FK_OrdersDetails_OrderID] FOREIGN KEY([OrderID])
REFERENCES [dbo].[Orders] ([OrderID])
GO
ALTER TABLE [dbo].[OrdersDetails] CHECK CONSTRAINT [FK_OrdersDetails_OrderID]
GO
ALTER TABLE [dbo].[UserAccounts]  WITH CHECK ADD  CONSTRAINT [FK_UserAccounts_Employees] FOREIGN KEY([UserId])
REFERENCES [dbo].[Employees] ([EmployeeID])
GO
ALTER TABLE [dbo].[UserAccounts] CHECK CONSTRAINT [FK_UserAccounts_Employees]
GO
ALTER TABLE [dbo].[UserAccounts]  WITH CHECK ADD  CONSTRAINT [FK_UserAccounts_StatusId] FOREIGN KEY([StatusId])
REFERENCES [dbo].[Status] ([StatusId])
GO
ALTER TABLE [dbo].[UserAccounts] CHECK CONSTRAINT [FK_UserAccounts_StatusId]
GO
USE [master]
GO
ALTER DATABASE [HiTechDistributionDB] SET  READ_WRITE 
GO
