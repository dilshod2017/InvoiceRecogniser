/****** Object:  Table [dbo].[Fields]    Script Date: 01/04/2021 4:28:25 pm ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Fields](
	[FieldsId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Accuracy] [float] NOT NULL,
	[Label] [nvarchar](50) NOT NULL,
	[Type] [nvarchar](10) not null default('string'),
	[Text] [nvarchar](max) not null
 CONSTRAINT [PK_FieldsId] PRIMARY KEY CLUSTERED 
(
	[FieldsId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[Name] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[Label] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

--/****** Object:  Table [dbo].[Status]    Script Date: 01/04/2021 4:31:45 pm ******/
--SET ANSI_NULLS ON
--GO

--SET QUOTED_IDENTIFIER ON
--GO

--CREATE TABLE [dbo].[Status](
--	[StatusId] [int] IDENTITY(1,1) NOT NULL,
--	[StatusLabel] [nvarchar](10) NOT NULL,
-- CONSTRAINT [PK_StatusId] PRIMARY KEY CLUSTERED 
--(
--	[StatusId] ASC
--)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
--UNIQUE NONCLUSTERED 
--(
--	[StatusLabel] ASC
--)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
--) ON [PRIMARY]
--GO

/****** Object:  Table [dbo].[Model]    Script Date: 01/04/2021 4:32:04 pm ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Model](
	[ModelId] [int] IDENTITY(1,1) NOT NULL,
	[RawModelId] [nvarchar](50) NOT NULL,
	[UserId] [int] NOT NULL,
	[CreatedDateTime] [datetime2](7) NOT NULL,
	[FieldsId] [int] NOT NULL,
	[Status] [int] NOT NULL,
 CONSTRAINT [PK_ModelId] PRIMARY KEY CLUSTERED 
(
	[ModelId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[RawModelId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Model] ADD  DEFAULT ((1)) FOR [UserId]
GO

ALTER TABLE [dbo].[Model] ADD  DEFAULT (sysutcdatetime()) FOR [CreatedDateTime]
GO

ALTER TABLE [dbo].[Model]  WITH CHECK ADD  CONSTRAINT [FK_Fields] FOREIGN KEY([FieldsId])
REFERENCES [dbo].[Fields] ([FieldsId])
GO

ALTER TABLE [dbo].[Model] CHECK CONSTRAINT [FK_Fields]
GO


create table dbo.ModelField(
	ModelFieldId int not null identity(1,1),
	FieldId int not null,
	ModelId int not null,
	constraint [PK_ModelField] primary key clustered(ModelFieldId),
	constraint [FK_ModelField_Field] foreign key(FieldId) references dbo.Fields(FieldsId),
	constraint [FK_ModelField_Model] foreign key(ModelFieldId) references dbo.Model(ModelId),
)

CREATE UNIQUE NONCLUSTERED INDEX ModelFieldIndex ON dbo.ModelField
(
	ModelFieldId Asc,
	FieldId Asc,
	ModelId asc
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF
	, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF
	, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]