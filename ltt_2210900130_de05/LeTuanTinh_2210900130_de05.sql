USE [letuantinh_2210900130_de05]
GO
/****** Object:  Table [dbo].[lttTask]    Script Date: 24-Jul-24 8:23:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[lttTask](
	[lttTaskId] [nchar](10) NOT NULL,
	[lttTaskName] [nvarchar](50) NULL,
	[lttTaskLevel] [int] NULL,
	[lttStartDate] [date] NULL,
	[lttTaskStatus] [bit] NULL,
 CONSTRAINT [PK_lttTask] PRIMARY KEY CLUSTERED 
(
	[lttTaskId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
