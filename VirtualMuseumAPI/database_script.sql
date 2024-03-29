USE [VirtualMuseum_DEV]
GO
/****** Object:  Table [dbo].[Artists]    Script Date: 4/24/2015 12:07:00 AM ******/
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
/****** Object:  Table [dbo].[ArtistsXUsers]    Script Date: 4/24/2015 12:07:00 AM ******/
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
/****** Object:  Table [dbo].[ArtworkFilters]    Script Date: 4/24/2015 12:07:00 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ArtworkFilters](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ArtworkKeyID] [int] NOT NULL,
	[Value] [nvarchar](max) NOT NULL,
	[ModiBy] [nvarchar](128) NOT NULL,
	[ModiDate] [datetime] NOT NULL,
 CONSTRAINT [PK_ArtworkFilters] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ArtworkFiltersXUsers]    Script Date: 4/24/2015 12:07:00 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ArtworkFiltersXUsers](
	[UID] [nvarchar](128) NOT NULL,
	[ArtworkFilterID] [int] NOT NULL,
 CONSTRAINT [PK_ArtworkFiltersXUsers] PRIMARY KEY CLUSTERED 
(
	[UID] ASC,
	[ArtworkFilterID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ArtworkKeys]    Script Date: 4/24/2015 12:07:00 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ArtworkKeys](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_dbo.ArtworkKeys] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ArtworkMetadata]    Script Date: 4/24/2015 12:07:00 AM ******/
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
/****** Object:  Table [dbo].[ArtworkRepresentations]    Script Date: 4/24/2015 12:07:00 AM ******/
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
/****** Object:  Table [dbo].[Artworks]    Script Date: 4/24/2015 12:07:00 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Artworks](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](50) NOT NULL,
	[ArtistID] [int] NOT NULL,
	[Collected] [int] NOT NULL,
	[ModiBy] [nvarchar](128) NOT NULL,
	[ModiDate] [datetime] NOT NULL,
 CONSTRAINT [PK_dbo.Artworks] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ArtworksXUsers]    Script Date: 4/24/2015 12:07:00 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ArtworksXUsers](
	[UID] [nvarchar](128) NOT NULL,
	[ArtworkID] [int] NOT NULL,
 CONSTRAINT [PK_ArtworksXUsers] PRIMARY KEY CLUSTERED 
(
	[UID] ASC,
	[ArtworkID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ConfigValues]    Script Date: 4/24/2015 12:07:00 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ConfigValues](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Setting] [varchar](50) NOT NULL,
	[Value] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_ConfigValues] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [UQ__ConfigVa__81E7DFFD22E0B640] UNIQUE NONCLUSTERED 
(
	[Setting] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CreditActions]    Script Date: 4/24/2015 12:07:00 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CreditActions](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[Credits] [int] NOT NULL,
 CONSTRAINT [PK_CreditActions] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CreditsXUsers]    Script Date: 4/24/2015 12:07:00 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CreditsXUsers](
	[UID] [nvarchar](128) NOT NULL,
	[Credits] [int] NOT NULL,
	[ModiDate] [datetime] NOT NULL,
 CONSTRAINT [PK_CreditsXUsers] PRIMARY KEY CLUSTERED 
(
	[UID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[MuseumKeys]    Script Date: 4/24/2015 12:07:00 AM ******/
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
/****** Object:  Table [dbo].[MuseumMetadata]    Script Date: 4/24/2015 12:07:00 AM ******/
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
/****** Object:  Table [dbo].[MuseumRatings]    Script Date: 4/24/2015 12:07:00 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MuseumRatings](
	[UID] [nvarchar](128) NOT NULL,
	[MuseumID] [int] NOT NULL,
	[Rating] [int] NOT NULL,
	[ModiDate] [datetime] NOT NULL,
 CONSTRAINT [PK_MuseumRatings] PRIMARY KEY CLUSTERED 
(
	[UID] ASC,
	[MuseumID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Museums]    Script Date: 4/24/2015 12:07:00 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Museums](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Data] [varbinary](max) NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[OwnerID] [nvarchar](128) NOT NULL,
	[Status] [int] NOT NULL,
	[PrivacyLevelID] [int] NOT NULL,
	[Visited] [int] NOT NULL,
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
/****** Object:  Table [dbo].[MuseumsXArtworks]    Script Date: 4/24/2015 12:07:00 AM ******/
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
/****** Object:  Table [dbo].[MuseumUserVisits]    Script Date: 4/24/2015 12:07:00 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MuseumUserVisits](
	[MuseumID] [int] NOT NULL,
	[UID] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_MuseumUserVisits] PRIMARY KEY CLUSTERED 
(
	[MuseumID] ASC,
	[UID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PrivacyLevels]    Script Date: 4/24/2015 12:07:00 AM ******/
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
ALTER TABLE [dbo].[ArtworkFilters]  WITH CHECK ADD  CONSTRAINT [ArtworkFilters_ArtworkKeyID_FK] FOREIGN KEY([ArtworkKeyID])
REFERENCES [dbo].[ArtworkKeys] ([ID])
GO
ALTER TABLE [dbo].[ArtworkFilters] CHECK CONSTRAINT [ArtworkFilters_ArtworkKeyID_FK]
GO
ALTER TABLE [dbo].[ArtworkFilters]  WITH CHECK ADD  CONSTRAINT [ArtworkFilters_ModiBy_FK] FOREIGN KEY([ModiBy])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[ArtworkFilters] CHECK CONSTRAINT [ArtworkFilters_ModiBy_FK]
GO
ALTER TABLE [dbo].[ArtworkFiltersXUsers]  WITH CHECK ADD  CONSTRAINT [ArtworkFiltersXUsers_ArtworkFilterID_FK] FOREIGN KEY([ArtworkFilterID])
REFERENCES [dbo].[ArtworkFilters] ([ID])
GO
ALTER TABLE [dbo].[ArtworkFiltersXUsers] CHECK CONSTRAINT [ArtworkFiltersXUsers_ArtworkFilterID_FK]
GO
ALTER TABLE [dbo].[ArtworkFiltersXUsers]  WITH CHECK ADD  CONSTRAINT [ArtworkFiltersXUsers_UID_FK] FOREIGN KEY([UID])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[ArtworkFiltersXUsers] CHECK CONSTRAINT [ArtworkFiltersXUsers_UID_FK]
GO
ALTER TABLE [dbo].[ArtworkMetadata]  WITH CHECK ADD  CONSTRAINT [ArtworkKey_ArtworkMetadata] FOREIGN KEY([KeyID])
REFERENCES [dbo].[ArtworkKeys] ([ID])
GO
ALTER TABLE [dbo].[ArtworkMetadata] CHECK CONSTRAINT [ArtworkKey_ArtworkMetadata]
GO
ALTER TABLE [dbo].[ArtworkMetadata]  WITH CHECK ADD  CONSTRAINT [ArtworkMetadata_ArtworkID_FK] FOREIGN KEY([ArtworkID])
REFERENCES [dbo].[Artworks] ([ID])
GO
ALTER TABLE [dbo].[ArtworkMetadata] CHECK CONSTRAINT [ArtworkMetadata_ArtworkID_FK]
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
ALTER TABLE [dbo].[ArtworksXUsers]  WITH CHECK ADD  CONSTRAINT [ArtworksXUsers_ArtworkID_FK] FOREIGN KEY([ArtworkID])
REFERENCES [dbo].[Artworks] ([ID])
GO
ALTER TABLE [dbo].[ArtworksXUsers] CHECK CONSTRAINT [ArtworksXUsers_ArtworkID_FK]
GO
ALTER TABLE [dbo].[ArtworksXUsers]  WITH CHECK ADD  CONSTRAINT [ArtworksXUsers_UID_FK] FOREIGN KEY([UID])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[ArtworksXUsers] CHECK CONSTRAINT [ArtworksXUsers_UID_FK]
GO
ALTER TABLE [dbo].[CreditsXUsers]  WITH CHECK ADD  CONSTRAINT [CreditsXUsers_UID_FK] FOREIGN KEY([UID])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[CreditsXUsers] CHECK CONSTRAINT [CreditsXUsers_UID_FK]
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
ALTER TABLE [dbo].[MuseumUserVisits]  WITH CHECK ADD  CONSTRAINT [MuseumUserVisits_MuseumID_FK] FOREIGN KEY([MuseumID])
REFERENCES [dbo].[Museums] ([ID])
GO
ALTER TABLE [dbo].[MuseumUserVisits] CHECK CONSTRAINT [MuseumUserVisits_MuseumID_FK]
GO
ALTER TABLE [dbo].[MuseumUserVisits]  WITH CHECK ADD  CONSTRAINT [MuseumUserVisits_UID_FK] FOREIGN KEY([UID])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[MuseumUserVisits] CHECK CONSTRAINT [MuseumUserVisits_UID_FK]
GO
