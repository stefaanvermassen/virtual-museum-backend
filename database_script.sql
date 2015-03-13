USE [master]
GO
/****** Object:  Database [VirtualMuseum_PROD]    Script Date: 3/13/2015 6:19:05 PM ******/
CREATE DATABASE [VirtualMuseum_PROD]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'VirtualMuseum', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\VirtualMuseum.mdf' , SIZE = 3264KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'VirtualMuseum_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\VirtualMuseum_log.ldf' , SIZE = 832KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [VirtualMuseum_PROD] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [VirtualMuseum_PROD].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [VirtualMuseum_PROD] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [VirtualMuseum_PROD] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [VirtualMuseum_PROD] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [VirtualMuseum_PROD] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [VirtualMuseum_PROD] SET ARITHABORT OFF 
GO
ALTER DATABASE [VirtualMuseum_PROD] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [VirtualMuseum_PROD] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [VirtualMuseum_PROD] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [VirtualMuseum_PROD] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [VirtualMuseum_PROD] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [VirtualMuseum_PROD] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [VirtualMuseum_PROD] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [VirtualMuseum_PROD] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [VirtualMuseum_PROD] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [VirtualMuseum_PROD] SET  ENABLE_BROKER 
GO
ALTER DATABASE [VirtualMuseum_PROD] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [VirtualMuseum_PROD] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [VirtualMuseum_PROD] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [VirtualMuseum_PROD] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [VirtualMuseum_PROD] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [VirtualMuseum_PROD] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [VirtualMuseum_PROD] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [VirtualMuseum_PROD] SET RECOVERY FULL 
GO
ALTER DATABASE [VirtualMuseum_PROD] SET  MULTI_USER 
GO
ALTER DATABASE [VirtualMuseum_PROD] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [VirtualMuseum_PROD] SET DB_CHAINING OFF 
GO
ALTER DATABASE [VirtualMuseum_PROD] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [VirtualMuseum_PROD] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [VirtualMuseum_PROD] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'VirtualMuseum_PROD', N'ON'
GO
USE [VirtualMuseum_PROD]
GO
/****** Object:  Table [dbo].[__MigrationHistory]    Script Date: 3/13/2015 6:19:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[__MigrationHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ContextKey] [nvarchar](300) NOT NULL,
	[Model] [varbinary](max) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK_dbo.__MigrationHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC,
	[ContextKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Artists]    Script Date: 3/13/2015 6:19:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Artists](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](128) NOT NULL,
	[ModiBy] [nvarchar](128) NOT NULL,
	[ModiDate] [datetime] NOT NULL,
 CONSTRAINT [PK_dbo.Artists] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ArtistsXUsers]    Script Date: 3/13/2015 6:19:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ArtistsXUsers](
	[UID] [nvarchar](128) NOT NULL,
	[ArtistID] [int] NOT NULL,
 CONSTRAINT [PK_dbo.ArtistsXUsers] PRIMARY KEY CLUSTERED 
(
	[UID] ASC,
	[ArtistID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ArtworkKeys]    Script Date: 3/13/2015 6:19:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ArtworkKeys](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](100) NOT NULL,
	[ModiBy] [nvarchar](128) NOT NULL,
	[ModiDate] [datetime] NOT NULL,
 CONSTRAINT [PK_dbo.ArtworkKeys] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ArtworkMetadata]    Script Date: 3/13/2015 6:19:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ArtworkMetadata](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ArtworkID] [int] NOT NULL,
	[KeyID] [int] NOT NULL,
	[Value] [nvarchar](max) NOT NULL,
	[ModiBy] [nvarchar](128) NOT NULL,
	[ModiDate] [datetime] NOT NULL,
 CONSTRAINT [PK_dbo.ArtworkMetadata] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ArtworkRepresentations]    Script Date: 3/13/2015 6:19:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ArtworkRepresentations](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ArtworkID] [int] NOT NULL,
	[Size] [int] NOT NULL,
	[DataGUID] [uniqueidentifier] NOT NULL,
	[Data] [varbinary](max) NULL,
 CONSTRAINT [PK_dbo.ArtworkRepresentations] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Artworks]    Script Date: 3/13/2015 6:19:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Artworks](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](50) NOT NULL,
	[ArtistID] [int] NOT NULL,
	[ModiBy] [nvarchar](128) NOT NULL,
	[ModiDate] [datetime] NOT NULL,
 CONSTRAINT [PK_dbo.Artworks] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 3/13/2015 6:19:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoles](
	[Id] [nvarchar](128) NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 3/13/2015 6:19:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 3/13/2015 6:19:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserLogins](
	[LoginProvider] [nvarchar](128) NOT NULL,
	[ProviderKey] [nvarchar](128) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC,
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 3/13/2015 6:19:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserRoles](
	[UserId] [nvarchar](128) NOT NULL,
	[RoleId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 3/13/2015 6:19:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUsers](
	[Id] [nvarchar](128) NOT NULL,
	[Email] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEndDateUtc] [datetime] NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
	[UserName] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [RoleNameIndex]    Script Date: 3/13/2015 6:19:05 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [RoleNameIndex] ON [dbo].[AspNetRoles]
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_UserId]    Script Date: 3/13/2015 6:19:05 PM ******/
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[AspNetUserClaims]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_UserId]    Script Date: 3/13/2015 6:19:05 PM ******/
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[AspNetUserLogins]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_RoleId]    Script Date: 3/13/2015 6:19:05 PM ******/
CREATE NONCLUSTERED INDEX [IX_RoleId] ON [dbo].[AspNetUserRoles]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_UserId]    Script Date: 3/13/2015 6:19:05 PM ******/
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[AspNetUserRoles]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UserNameIndex]    Script Date: 3/13/2015 6:19:05 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [UserNameIndex] ON [dbo].[AspNetUsers]
(
	[UserName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Artists]  WITH CHECK ADD  CONSTRAINT [AspNetUser_Artist] FOREIGN KEY([ModiBy])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[Artists] CHECK CONSTRAINT [AspNetUser_Artist]
GO
ALTER TABLE [dbo].[ArtistsXUsers]  WITH CHECK ADD  CONSTRAINT [AspNetUser_ArtistsXUser] FOREIGN KEY([UID])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[ArtistsXUsers] CHECK CONSTRAINT [AspNetUser_ArtistsXUser]
GO
ALTER TABLE [dbo].[ArtworkKeys]  WITH CHECK ADD  CONSTRAINT [AspNetUser_ArtworkKey] FOREIGN KEY([ModiBy])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[ArtworkKeys] CHECK CONSTRAINT [AspNetUser_ArtworkKey]
GO
ALTER TABLE [dbo].[ArtworkMetadata]  WITH CHECK ADD  CONSTRAINT [ArtworkKey_ArtworkMetadata] FOREIGN KEY([KeyID])
REFERENCES [dbo].[ArtworkKeys] ([ID])
GO
ALTER TABLE [dbo].[ArtworkMetadata] CHECK CONSTRAINT [ArtworkKey_ArtworkMetadata]
GO
ALTER TABLE [dbo].[ArtworkMetadata]  WITH CHECK ADD  CONSTRAINT [ArtworkMetadata_ArtworkMetadata] FOREIGN KEY([ID])
REFERENCES [dbo].[ArtworkMetadata] ([ID])
GO
ALTER TABLE [dbo].[ArtworkMetadata] CHECK CONSTRAINT [ArtworkMetadata_ArtworkMetadata]
GO
ALTER TABLE [dbo].[ArtworkMetadata]  WITH CHECK ADD  CONSTRAINT [AspNetUser_ArtworkMetadata] FOREIGN KEY([ModiBy])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[ArtworkMetadata] CHECK CONSTRAINT [AspNetUser_ArtworkMetadata]
GO
ALTER TABLE [dbo].[ArtworkRepresentations]  WITH CHECK ADD  CONSTRAINT [Artwork_ArtworkRepresentation] FOREIGN KEY([ArtworkID])
REFERENCES [dbo].[Artworks] ([ID])
GO
ALTER TABLE [dbo].[ArtworkRepresentations] CHECK CONSTRAINT [Artwork_ArtworkRepresentation]
GO
ALTER TABLE [dbo].[Artworks]  WITH CHECK ADD  CONSTRAINT [Artist_Artwork] FOREIGN KEY([ArtistID])
REFERENCES [dbo].[Artists] ([ID])
GO
ALTER TABLE [dbo].[Artworks] CHECK CONSTRAINT [Artist_Artwork]
GO
ALTER TABLE [dbo].[Artworks]  WITH CHECK ADD  CONSTRAINT [AspNetUser_Artwork] FOREIGN KEY([ModiBy])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[Artworks] CHECK CONSTRAINT [AspNetUser_Artwork]
GO
ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId]
GO
USE [master]
GO
ALTER DATABASE [VirtualMuseum_PROD] SET  READ_WRITE 
GO
