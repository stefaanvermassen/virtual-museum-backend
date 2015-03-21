USE [VirtualMuseum_DEV]
GO
/****** Object:  Table [dbo].[Artists]    Script Date: 3/21/2015 5:52:16 PM ******/
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
/****** Object:  Table [dbo].[ArtistsXUsers]    Script Date: 3/21/2015 5:52:16 PM ******/
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
/****** Object:  Table [dbo].[ArtworkKeys]    Script Date: 3/21/2015 5:52:16 PM ******/
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
/****** Object:  Table [dbo].[ArtworkMetadata]    Script Date: 3/21/2015 5:52:16 PM ******/
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
/****** Object:  Table [dbo].[ArtworkRepresentations]    Script Date: 3/21/2015 5:52:16 PM ******/
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
/****** Object:  Table [dbo].[Artworks]    Script Date: 3/21/2015 5:52:16 PM ******/
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
/****** Object:  Table [dbo].[MuseumKeys]    Script Date: 3/21/2015 5:52:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MuseumKeys](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[ModiBy] [nvarchar](128) NOT NULL,
	[ModiDate] [datetime] NOT NULL,
 CONSTRAINT [PK_MuseumKeys] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[MuseumMetadata]    Script Date: 3/21/2015 5:52:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MuseumMetadata](
	[ID] [int] NOT NULL,
	[MuseumID] [int] NOT NULL,
	[KeyID] [int] NOT NULL,
	[Value] [nvarchar](max) NOT NULL,
	[ModiBy] [nvarchar](128) NOT NULL,
	[ModiDate] [datetime] NOT NULL,
 CONSTRAINT [PK_MuseumMetadata] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Museums]    Script Date: 3/21/2015 5:52:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Museums](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Data] [varbinary](max) NULL,
	[Description] [nvarchar](max) NOT NULL,
	[OwnerID] [nvarchar](128) NOT NULL,
	[Status] [int] NOT NULL,
	[PrivacyLevelID] [int] NOT NULL,
	[ModiBy] [nvarchar](128) NOT NULL,
	[ModiDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Museums] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[MuseumsXArtworks]    Script Date: 3/21/2015 5:52:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MuseumsXArtworks](
	[MuseumID] [int] NOT NULL,
	[ArtworkID] [int] NOT NULL,
 CONSTRAINT [PK_MuseumsXArtworks] PRIMARY KEY CLUSTERED 
(
	[MuseumID] ASC,
	[ArtworkID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PrivacyLevels]    Script Date: 3/21/2015 5:52:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PrivacyLevels](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[ModiBy] [nvarchar](128) NOT NULL,
	[ModiDate] [datetime] NOT NULL,
 CONSTRAINT [PK_PrivacyLevels] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
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
ALTER TABLE [dbo].[MuseumKeys]  WITH CHECK ADD  CONSTRAINT [MuseumKeys_ModiBy_FK] FOREIGN KEY([ModiBy])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[MuseumKeys] CHECK CONSTRAINT [MuseumKeys_ModiBy_FK]
GO
ALTER TABLE [dbo].[MuseumMetadata]  WITH CHECK ADD  CONSTRAINT [MuseumMetadata_KeyID_FK] FOREIGN KEY([KeyID])
REFERENCES [dbo].[MuseumKeys] ([ID])
GO
ALTER TABLE [dbo].[MuseumMetadata] CHECK CONSTRAINT [MuseumMetadata_KeyID_FK]
GO
ALTER TABLE [dbo].[MuseumMetadata]  WITH CHECK ADD  CONSTRAINT [MuseumMetadata_ModiBy_FK] FOREIGN KEY([ModiBy])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[MuseumMetadata] CHECK CONSTRAINT [MuseumMetadata_ModiBy_FK]
GO
ALTER TABLE [dbo].[MuseumMetadata]  WITH CHECK ADD  CONSTRAINT [MuseumMetadata_MuseumID_FK] FOREIGN KEY([MuseumID])
REFERENCES [dbo].[Museums] ([ID])
GO
ALTER TABLE [dbo].[MuseumMetadata] CHECK CONSTRAINT [MuseumMetadata_MuseumID_FK]
GO
ALTER TABLE [dbo].[Museums]  WITH CHECK ADD  CONSTRAINT [Museums_ModiBy_FK] FOREIGN KEY([ModiBy])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[Museums] CHECK CONSTRAINT [Museums_ModiBy_FK]
GO
ALTER TABLE [dbo].[Museums]  WITH CHECK ADD  CONSTRAINT [Museums_OwnerID_FK] FOREIGN KEY([OwnerID])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[Museums] CHECK CONSTRAINT [Museums_OwnerID_FK]
GO
ALTER TABLE [dbo].[Museums]  WITH CHECK ADD  CONSTRAINT [Museums_PrivacyLevelID_FK] FOREIGN KEY([PrivacyLevelID])
REFERENCES [dbo].[PrivacyLevels] ([ID])
GO
ALTER TABLE [dbo].[Museums] CHECK CONSTRAINT [Museums_PrivacyLevelID_FK]
GO
ALTER TABLE [dbo].[MuseumsXArtworks]  WITH CHECK ADD  CONSTRAINT [MuseumsXArtworks_ArtworkID_FK] FOREIGN KEY([ArtworkID])
REFERENCES [dbo].[Artworks] ([ID])
GO
ALTER TABLE [dbo].[MuseumsXArtworks] CHECK CONSTRAINT [MuseumsXArtworks_ArtworkID_FK]
GO
ALTER TABLE [dbo].[MuseumsXArtworks]  WITH CHECK ADD  CONSTRAINT [MuseumsXArtworks_MuseumID_FK] FOREIGN KEY([MuseumID])
REFERENCES [dbo].[Museums] ([ID])
GO
ALTER TABLE [dbo].[MuseumsXArtworks] CHECK CONSTRAINT [MuseumsXArtworks_MuseumID_FK]
GO
