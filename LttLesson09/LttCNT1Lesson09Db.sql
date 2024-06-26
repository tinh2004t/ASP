USE [LttK22CNTLesson09Db]
GO
/****** Object:  Table [dbo].[LttKhoa]    Script Date: 17-Jun-24 10:03:35 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LttKhoa](
	[LttMaKH] [nchar](10) NOT NULL,
	[LttTenKH] [nvarchar](50) NULL,
	[LttTrangThai] [bit] NULL,
 CONSTRAINT [PK_LttKhoa] PRIMARY KEY CLUSTERED 
(
	[LttMaKH] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LttSinhVien]    Script Date: 17-Jun-24 10:03:35 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LttSinhVien](
	[LttMaSV] [nvarchar](50) NOT NULL,
	[LttHoSV] [nvarchar](50) NULL,
	[LttTenSV] [nvarchar](50) NULL,
	[LttNgaySinh] [date] NULL,
	[LttPhai] [bit] NULL,
	[LttPhone] [nchar](10) NULL,
	[LttEmail] [nvarchar](50) NULL,
	[LttMaKH] [nchar](10) NULL,
 CONSTRAINT [PK_LttSinhVien] PRIMARY KEY CLUSTERED 
(
	[LttMaSV] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[LttKhoa] ([LttMaKH], [LttTenKH], [LttTrangThai]) VALUES (N'CNTT3     ', N'CNT3', 0)
INSERT [dbo].[LttKhoa] ([LttMaKH], [LttTenKH], [LttTrangThai]) VALUES (N'K22CNT1   ', N'K22CNT1', 1)
GO
INSERT [dbo].[LttSinhVien] ([LttMaSV], [LttHoSV], [LttTenSV], [LttNgaySinh], [LttPhai], [LttPhone], [LttEmail], [LttMaKH]) VALUES (N'2210900130', N'Lê', N'Tuấn Tình', CAST(N'2004-04-24' AS Date), 1, N'0123453223', N'tinh@gmail.com', N'K22CNT1   ')
GO
ALTER TABLE [dbo].[LttSinhVien]  WITH CHECK ADD  CONSTRAINT [FK_LttSinhVien_LttKhoa1] FOREIGN KEY([LttMaKH])
REFERENCES [dbo].[LttKhoa] ([LttMaKH])
GO
ALTER TABLE [dbo].[LttSinhVien] CHECK CONSTRAINT [FK_LttSinhVien_LttKhoa1]
GO
