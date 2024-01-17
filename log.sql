USE [master]
GO
CREATE DATABASE [Больница]
GO
USE [Больница]
GO
CREATE TABLE [dbo].[Авторизация](
	[Логин] [nvarchar](50) NOT NULL,
	[ХэшПароль] [nvarchar](50) NOT NULL,
	[КодРегистора] [nchar](10) NULL
) ON [PRIMARY]
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

CREATE TABLE [dbo].[Диагноз](
	[Код] [int] NOT NULL,
	[Наименование] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Диагноз] PRIMARY KEY CLUSTERED 
(
	[Код] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[Пациент](
	[НомерКарты] [int] NOT NULL,
	[ФИО] [nvarchar](100) NOT NULL,
	[ДатаРождения] [date] NOT NULL,
	[Адрес] [nvarchar](100) NOT NULL,
	[Пол] [bit] NOT NULL,
	[КодСкидки] [int] NULL,
	[Талон] [int] NOT NULL,
 CONSTRAINT [PK_Пациент] PRIMARY KEY CLUSTERED 
(
	[НомерКарты] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[Прием](
	[Код] [int] NOT NULL,
	[КодВрача] [int] NOT NULL,
	[КодПациента] [int] NOT NULL,
	[ДатаВизита] [date] NOT NULL,
	[КодСтоимости] [int] NOT NULL,
	[Цель] [nvarchar](50) NOT NULL,
	[ДатаДиагноза] [int] NOT NULL,
 CONSTRAINT [PK_Прием] PRIMARY KEY CLUSTERED 
(
	[Код] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[ПриемДиагноз](
	[Код] [int] NOT NULL,
	[КодПриема] [int] NOT NULL,
	[КодДиагноза] [int] NOT NULL
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[Скидка](
	[Код] [int] NOT NULL,
	[Процент] [int] NOT NULL,
 CONSTRAINT [PK_Скидка] PRIMARY KEY CLUSTERED 
(
	[Код] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
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

CREATE TABLE [dbo].[Талон](
	[Код] [int] NOT NULL,
	[КодПациента] [int] NOT NULL,
	[НомерТалона] [int] NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Пациент]  WITH CHECK ADD  CONSTRAINT [FK_Пациент_Скидка] FOREIGN KEY([КодСкидки])
REFERENCES [dbo].[Скидка] ([Код])
GO
ALTER TABLE [dbo].[Пациент] CHECK CONSTRAINT [FK_Пациент_Скидка]
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
USE [master]
GO
ALTER DATABASE [Больница] SET  READ_WRITE 
GO
