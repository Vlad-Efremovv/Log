USE [master]
GO
/****** Object:  Database [Больница]    Script Date: 19.01.2024 3:13:50 ******/
CREATE DATABASE [Больница]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Больница', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\Больница.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Больница_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\Больница_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [Больница] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Больница].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Больница] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Больница] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Больница] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Больница] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Больница] SET ARITHABORT OFF 
GO
ALTER DATABASE [Больница] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [Больница] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Больница] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Больница] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Больница] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Больница] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Больница] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Больница] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Больница] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Больница] SET  ENABLE_BROKER 
GO
ALTER DATABASE [Больница] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Больница] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Больница] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Больница] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Больница] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Больница] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Больница] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Больница] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Больница] SET  MULTI_USER 
GO
ALTER DATABASE [Больница] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Больница] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Больница] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Больница] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Больница] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Больница] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [Больница] SET QUERY_STORE = OFF
GO
USE [Больница]
GO
/****** Object:  Table [dbo].[Авторизация]    Script Date: 19.01.2024 3:13:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Авторизация](
	[Логин] [nvarchar](50) NOT NULL,
	[ХэшПароль] [nvarchar](100) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Врачь]    Script Date: 19.01.2024 3:13:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Врачь](
	[Код] [int] NOT NULL,
	[ФИО] [nvarchar](100) NOT NULL,
	[Специальность] [nvarchar](50) NOT NULL,
	[Категория] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Врачь] PRIMARY KEY CLUSTERED 
(
	[Код] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Диагноз]    Script Date: 19.01.2024 3:13:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Диагноз](
	[Код] [int] NOT NULL,
	[Наименование] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Диагноз] PRIMARY KEY CLUSTERED 
(
	[Код] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Пациент]    Script Date: 19.01.2024 3:13:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Пациент](
	[НомерКарты] [int] NOT NULL,
	[ФИО] [nvarchar](100) NOT NULL,
	[ДатаРождения] [date] NOT NULL,
	[Пол] [bit] NOT NULL,
	[Талон] [int] NOT NULL,
 CONSTRAINT [PK_Пациент] PRIMARY KEY CLUSTERED 
(
	[НомерКарты] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Прием]    Script Date: 19.01.2024 3:13:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Прием](
	[Код] [int] NOT NULL,
	[КодВрача] [int] NOT NULL,
	[КодПациента] [int] NOT NULL,
	[ДатаВизита] [date] NOT NULL,
	[КодСтоимости] [int] NOT NULL,
	[Цель] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Прием] PRIMARY KEY CLUSTERED 
(
	[Код] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ПриемДиагноз]    Script Date: 19.01.2024 3:13:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ПриемДиагноз](
	[Код] [int] NOT NULL,
	[КодПриема] [int] NOT NULL,
	[КодДиагноза] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Стоимость]    Script Date: 19.01.2024 3:13:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Стоимость](
	[Код] [int] NOT NULL,
	[Сумма] [money] NOT NULL,
 CONSTRAINT [PK_Стоимость] PRIMARY KEY CLUSTERED 
(
	[Код] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Талон]    Script Date: 19.01.2024 3:13:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Талон](
	[Код] [int] NOT NULL,
	[КодПациента] [int] NOT NULL,
	[НомерТалона] [int] NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Прием]  WITH CHECK ADD  CONSTRAINT [FK_Прием_Врачь] FOREIGN KEY([КодВрача])
REFERENCES [dbo].[Врачь] ([Код])
GO
ALTER TABLE [dbo].[Прием] CHECK CONSTRAINT [FK_Прием_Врачь]
GO
ALTER TABLE [dbo].[Прием]  WITH CHECK ADD  CONSTRAINT [FK_Прием_Пациент] FOREIGN KEY([КодПациента])
REFERENCES [dbo].[Пациент] ([НомерКарты])
GO
ALTER TABLE [dbo].[Прием] CHECK CONSTRAINT [FK_Прием_Пациент]
GO
ALTER TABLE [dbo].[Прием]  WITH CHECK ADD  CONSTRAINT [FK_Прием_Стоимость] FOREIGN KEY([КодСтоимости])
REFERENCES [dbo].[Стоимость] ([Код])
GO
ALTER TABLE [dbo].[Прием] CHECK CONSTRAINT [FK_Прием_Стоимость]
GO
ALTER TABLE [dbo].[ПриемДиагноз]  WITH CHECK ADD  CONSTRAINT [FK_ПриемДиагноз_Диагноз] FOREIGN KEY([КодДиагноза])
REFERENCES [dbo].[Диагноз] ([Код])
GO
ALTER TABLE [dbo].[ПриемДиагноз] CHECK CONSTRAINT [FK_ПриемДиагноз_Диагноз]
GO
ALTER TABLE [dbo].[ПриемДиагноз]  WITH CHECK ADD  CONSTRAINT [FK_ПриемДиагноз_Прием] FOREIGN KEY([КодПриема])
REFERENCES [dbo].[Прием] ([Код])
GO
ALTER TABLE [dbo].[ПриемДиагноз] CHECK CONSTRAINT [FK_ПриемДиагноз_Прием]
GO
ALTER TABLE [dbo].[Талон]  WITH CHECK ADD  CONSTRAINT [FK_Талон_Пациент] FOREIGN KEY([КодПациента])
REFERENCES [dbo].[Пациент] ([НомерКарты])
GO
ALTER TABLE [dbo].[Талон] CHECK CONSTRAINT [FK_Талон_Пациент]
GO

 INSERT INTO [dbo].[Авторизация]([Логин],[ХэшПароль])
VALUES(N'adm',N'b09c600fddc573f117449b3723f23d64'),(N'user',N'ee11cbb19052e40b07aac0ca060c23ee');  
GO
-- Добавление данных в таблицу "Врачь"
INSERT INTO [dbo].[Врачь] ([Код], [ФИО], [Специальность], [Категория])
VALUES (1, N'Иванов Иван Иванович', N'Терапевт', 'Первая')

-- Добавление данных в таблицу "Диагноз"
INSERT INTO [dbo].[Диагноз] ([Код], [Наименование])
VALUES (1, N'Грипп')

-- Добавление данных в таблицу "Стоимость"
INSERT INTO [dbo].[Стоимость] ([Код], [Сумма])
VALUES (1, 1000)

-- Добавление данных в таблицу "Пациент"
INSERT INTO [dbo].[Пациент] ([НомерКарты], [ФИО], [ДатаРождения], [Пол], [Талон])
VALUES (1111, N'Петрова Анна Петровна', '1990-05-15', 0, 12345)

-- Добавление данных в таблицу "Прием"
INSERT INTO [dbo].[Прием] ([Код], [КодВрача], [КодПациента], [ДатаВизита], [КодСтоимости], [Цель])
VALUES (1, 1, 1111, '2024-01-19', 1, N'Прием у терапевта')


-- Добавление данных в таблицу "ПриемДиагноз"
INSERT INTO [dbo].[ПриемДиагноз] ([Код], [КодПриема], [КодДиагноза])
VALUES (1, 1, 1)