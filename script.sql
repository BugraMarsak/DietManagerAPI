USE [master]
GO
/****** Object:  Database [DietManager]    Script Date: 12.01.2023 18:31:00 ******/
CREATE DATABASE [DietManager]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'DietManager', FILENAME = N'C:\Users\uykuc\AppData\Local\Microsoft\Microsoft SQL Server Local DB\Instances\MSSQLLocalDB\DietManager.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'DietManager_log', FILENAME = N'C:\Users\uykuc\AppData\Local\Microsoft\Microsoft SQL Server Local DB\Instances\MSSQLLocalDB\DietManager.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [DietManager] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [DietManager].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [DietManager] SET ANSI_NULL_DEFAULT ON 
GO
ALTER DATABASE [DietManager] SET ANSI_NULLS ON 
GO
ALTER DATABASE [DietManager] SET ANSI_PADDING ON 
GO
ALTER DATABASE [DietManager] SET ANSI_WARNINGS ON 
GO
ALTER DATABASE [DietManager] SET ARITHABORT ON 
GO
ALTER DATABASE [DietManager] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [DietManager] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [DietManager] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [DietManager] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [DietManager] SET CURSOR_DEFAULT  LOCAL 
GO
ALTER DATABASE [DietManager] SET CONCAT_NULL_YIELDS_NULL ON 
GO
ALTER DATABASE [DietManager] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [DietManager] SET QUOTED_IDENTIFIER ON 
GO
ALTER DATABASE [DietManager] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [DietManager] SET  DISABLE_BROKER 
GO
ALTER DATABASE [DietManager] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [DietManager] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [DietManager] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [DietManager] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [DietManager] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [DietManager] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [DietManager] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [DietManager] SET RECOVERY FULL 
GO
ALTER DATABASE [DietManager] SET  MULTI_USER 
GO
ALTER DATABASE [DietManager] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [DietManager] SET DB_CHAINING OFF 
GO
ALTER DATABASE [DietManager] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [DietManager] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [DietManager] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [DietManager] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [DietManager] SET QUERY_STORE = OFF
GO
USE [DietManager]
GO
/****** Object:  Table [dbo].[Allergen]    Script Date: 12.01.2023 18:31:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Allergen](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AllergenName] [nvarchar](500) NOT NULL,
 CONSTRAINT [PK_Allergen] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Appointment]    Script Date: 12.01.2023 18:31:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Appointment](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DietianId] [int] NOT NULL,
	[ClientId] [int] NOT NULL,
	[AppointmentDate] [datetime2](7) NOT NULL,
	[AppointmentType] [nvarchar](400) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ClientAllergies]    Script Date: 12.01.2023 18:31:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ClientAllergies](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ClientId] [int] NOT NULL,
	[AllergiesList] [text] NOT NULL,
 CONSTRAINT [PK_ClientAllergies] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ClientDefaultData]    Script Date: 12.01.2023 18:31:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ClientDefaultData](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Age] [int] NOT NULL,
	[Gender] [nvarchar](500) NOT NULL,
	[Height] [float] NOT NULL,
	[ClientId] [int] NOT NULL,
 CONSTRAINT [PK_ClientDefaultData] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ClientDietList]    Script Date: 12.01.2023 18:31:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ClientDietList](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ClientId] [int] NOT NULL,
	[DietitianId] [int] NOT NULL,
	[DietInfo] [nvarchar](500) NOT NULL,
	[DietDate] [datetime2](7) NOT NULL,
	[Note] [nvarchar](500) NULL,
	[Session] [int] NOT NULL,
 CONSTRAINT [PK_ClientDietList] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DietianClients]    Script Date: 12.01.2023 18:31:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DietianClients](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DietianId] [int] NOT NULL,
	[ClientId] [int] NOT NULL,
 CONSTRAINT [PK_DietianClients] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FoodList]    Script Date: 12.01.2023 18:31:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FoodList](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FoodName] [nvarchar](200) NOT NULL,
	[Calories] [int] NOT NULL,
	[Allergen] [nvarchar](250) NOT NULL,
 CONSTRAINT [PK_FoodList] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MeasurementResults]    Script Date: 12.01.2023 18:31:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MeasurementResults](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ClientId] [int] NOT NULL,
	[MeasurementTime] [datetime2](7) NOT NULL,
	[Weight] [float] NOT NULL,
	[TargetWeight] [float] NOT NULL,
	[BKI] [float] NOT NULL,
	[BodyFatRatio] [float] NOT NULL,
	[IKA] [float] NOT NULL,
	[Protein] [float] NOT NULL,
	[Mineral] [float] NOT NULL,
	[TotalBodyWater] [float] NOT NULL,
	[BodyFatWeight] [float] NOT NULL,
	[VisceralFatLevel] [float] NOT NULL,
	[TargetVisceralFatLevel] [float] NOT NULL,
 CONSTRAINT [PK_MeasurementResults] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Messages]    Script Date: 12.01.2023 18:31:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Messages](
	[MessageId] [int] IDENTITY(1,1) NOT NULL,
	[SenderId] [int] NOT NULL,
	[ReceiverId] [int] NOT NULL,
	[SendTime] [datetime] NOT NULL,
	[Messages] [varchar](max) NOT NULL,
	[IsreciverRead] [bit] NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Notes]    Script Date: 12.01.2023 18:31:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Notes](
	[NotesId] [int] IDENTITY(1,1) NOT NULL,
	[NotesName] [nvarchar](250) NOT NULL,
	[NoteTime] [datetime2](7) NULL,
	[Note] [varchar](max) NOT NULL,
	[DietianId] [int] NOT NULL,
	[ClientId] [int] NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OperationClaims]    Script Date: 12.01.2023 18:31:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OperationClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](250) NOT NULL,
 CONSTRAINT [PK_OperationClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserOperationClaims]    Script Date: 12.01.2023 18:31:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserOperationClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[OperationClaimId] [int] NOT NULL,
 CONSTRAINT [PK_UserOperationClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 12.01.2023 18:31:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [varchar](50) NOT NULL,
	[LastName] [varchar](50) NOT NULL,
	[Email] [varchar](50) NOT NULL,
	[PasswordHash] [varbinary](500) NOT NULL,
	[PasswordSalt] [varbinary](500) NOT NULL,
	[Status] [bit] NOT NULL,
	[PhoneNumber] [varchar](50) NOT NULL,
	[Photo] [varchar](500) NOT NULL,
	[Address] [varchar](500) NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
USE [master]
GO
ALTER DATABASE [DietManager] SET  READ_WRITE 
GO
