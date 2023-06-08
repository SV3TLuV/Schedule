CREATE DATABASE [Schedule]
GO
USE [Schedule]
GO
/****** Object:  Table [dbo].[Classrooms]    Script Date: 06.06.2023 10:11:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Classrooms](
	[ClassroomId] [int] IDENTITY(1,1) NOT NULL,
	[Cabinet] [nvarchar](10) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Classrooms] PRIMARY KEY CLUSTERED 
(
	[ClassroomId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Courses]    Script Date: 06.06.2023 10:11:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Courses](
	[CourseId] [int] NOT NULL,
 CONSTRAINT [PK_Courses] PRIMARY KEY CLUSTERED 
(
	[CourseId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Dates]    Script Date: 06.06.2023 10:11:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Dates](
	[DateId] [int] IDENTITY(1,1) NOT NULL,
	[Value] [date] NOT NULL,
	[Term] [int] NOT NULL,
	[DayId] [int] NOT NULL,
	[WeekTypeId] [int] NOT NULL,
	[IsStudy] [bit] NOT NULL,
 CONSTRAINT [PK_Dates] PRIMARY KEY CLUSTERED 
(
	[DateId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Days]    Script Date: 06.06.2023 10:11:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Days](
	[DayId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](20) NOT NULL,
	[IsStudy] [bit] NOT NULL,
 CONSTRAINT [PK_Days] PRIMARY KEY CLUSTERED 
(
	[DayId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Disciplines]    Script Date: 06.06.2023 10:11:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Disciplines](
	[DisciplineId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Code] [nvarchar](20) NOT NULL,
	[TotalHours] [int] NOT NULL,
	[TermId] [int] NOT NULL,
	[SpecialityId] [int] NOT NULL,
	[DisciplineTypeId] [int] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Disciplines] PRIMARY KEY CLUSTERED 
(
	[DisciplineId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DisciplineType]    Script Date: 06.06.2023 10:11:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DisciplineType](
	[DisciplineTypeId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](30) NOT NULL,
 CONSTRAINT [PK_DisciplineType] PRIMARY KEY CLUSTERED 
(
	[DisciplineTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GroupGroups]    Script Date: 06.06.2023 10:11:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GroupGroups](
	[GroupId] [int] NOT NULL,
	[GroupId2] [int] NOT NULL,
 CONSTRAINT [PK_GroupGroups] PRIMARY KEY CLUSTERED 
(
	[GroupId] ASC,
	[GroupId2] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Groups]    Script Date: 06.06.2023 10:11:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Groups](
	[GroupId] [int] IDENTITY(1,1) NOT NULL,
	[Number] [varchar](2) NOT NULL,
	[SpecialityId] [int] NOT NULL,
	[TermId] [int] NOT NULL,
	[EnrollmentYear] [int] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Groups] PRIMARY KEY CLUSTERED 
(
	[GroupId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GroupTransfers]    Script Date: 06.06.2023 10:11:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GroupTransfers](
	[GroupId] [int] NOT NULL,
	[NextTermId] [int] NOT NULL,
	[IsTransferred] [bit] NOT NULL,
	[TransferDate] [date] NOT NULL,
 CONSTRAINT [PK_GroupTransfers] PRIMARY KEY CLUSTERED 
(
	[GroupId] ASC,
	[NextTermId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Lessons]    Script Date: 06.06.2023 10:11:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Lessons](
	[LessonId] [int] IDENTITY(1,1) NOT NULL,
	[Number] [int] NOT NULL,
	[Subgroup] [int] NULL,
	[TimeId] [int] NULL,
	[TimetableId] [int] NOT NULL,
	[DisciplineId] [int] NULL,
	[IsChanged] [bit] NOT NULL,
 CONSTRAINT [PK_Pairs] PRIMARY KEY CLUSTERED 
(
	[LessonId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LessonTeacherClassrooms]    Script Date: 06.06.2023 10:11:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LessonTeacherClassrooms](
	[LessonId] [int] NOT NULL,
	[TeacherId] [int] NOT NULL,
	[ClassroomId] [int] NULL,
 CONSTRAINT [PK_PairTeachers] PRIMARY KEY CLUSTERED 
(
	[LessonId] ASC,
	[TeacherId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LessonTemplates]    Script Date: 06.06.2023 10:11:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LessonTemplates](
	[LessonTemplateId] [int] IDENTITY(1,1) NOT NULL,
	[Number] [int] NOT NULL,
	[Subgroup] [int] NULL,
	[TimeId] [int] NULL,
	[TemplateId] [int] NOT NULL,
	[DisciplineId] [int] NULL,
 CONSTRAINT [PK_LessonTemplates] PRIMARY KEY CLUSTERED 
(
	[LessonTemplateId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LessonTemplateTeacherClassrooms]    Script Date: 06.06.2023 10:11:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LessonTemplateTeacherClassrooms](
	[LessonTemplateId] [int] NOT NULL,
	[TeacherId] [int] NOT NULL,
	[ClassroomId] [int] NULL,
 CONSTRAINT [PK_LessonTemplateTeacherClassrooms] PRIMARY KEY CLUSTERED 
(
	[LessonTemplateId] ASC,
	[TeacherId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Logs]    Script Date: 06.06.2023 10:11:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Logs](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Message] [nvarchar](max) NULL,
	[MessageTemplate] [nvarchar](max) NULL,
	[Level] [nvarchar](max) NULL,
	[TimeStamp] [datetime] NULL,
	[Exception] [nvarchar](max) NULL,
	[Properties] [nvarchar](max) NULL,
 CONSTRAINT [PK_Logs] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 06.06.2023 10:11:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[RoleId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](30) NOT NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Sessions]    Script Date: 06.06.2023 10:11:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sessions](
	[SessionId] [uniqueidentifier] NOT NULL,
	[RefreshToken] [nvarchar](512) NOT NULL,
	[UserId] [int] NOT NULL,
 CONSTRAINT [PK_Sessions] PRIMARY KEY CLUSTERED 
(
	[SessionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Specialities]    Script Date: 06.06.2023 10:11:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Specialities](
	[SpecialityId] [int] IDENTITY(1,1) NOT NULL,
	[Code] [nvarchar](20) NOT NULL,
	[Name] [nvarchar](20) NOT NULL,
	[MaxTermId] [int] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Specialities] PRIMARY KEY CLUSTERED 
(
	[SpecialityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Teachers]    Script Date: 06.06.2023 10:11:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Teachers](
	[TeacherId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](40) NOT NULL,
	[Surname] [nvarchar](40) NOT NULL,
	[MiddleName] [nvarchar](40) NOT NULL,
	[Email] [nvarchar](200) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Teachers] PRIMARY KEY CLUSTERED 
(
	[TeacherId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Templates]    Script Date: 06.06.2023 10:11:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Templates](
	[TemplateId] [int] IDENTITY(1,1) NOT NULL,
	[GroupId] [int] NOT NULL,
	[TermId] [int] NOT NULL,
	[DayId] [int] NOT NULL,
	[WeekTypeId] [int] NOT NULL,
 CONSTRAINT [PK_Templates] PRIMARY KEY CLUSTERED 
(
	[TemplateId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Terms]    Script Date: 06.06.2023 10:11:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Terms](
	[TermId] [int] IDENTITY(1,1) NOT NULL,
	[CourseId] [int] NOT NULL,
	[CourseTerm] [int] NOT NULL,
 CONSTRAINT [PK_Terms] PRIMARY KEY CLUSTERED 
(
	[TermId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Times]    Script Date: 06.06.2023 10:11:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Times](
	[TimeId] [int] IDENTITY(1,1) NOT NULL,
	[Start] [time](7) NOT NULL,
	[End] [time](7) NOT NULL,
	[Duration] [int] NOT NULL,
	[LessonNumber] [int] NOT NULL,
	[TypeId] [int] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Times] PRIMARY KEY CLUSTERED 
(
	[TimeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Timetables]    Script Date: 06.06.2023 10:11:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Timetables](
	[TimetableId] [int] IDENTITY(1,1) NOT NULL,
	[GroupId] [int] NOT NULL,
	[DateId] [int] NOT NULL,
 CONSTRAINT [PK_Timetables] PRIMARY KEY CLUSTERED 
(
	[TimetableId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TimeTypes]    Script Date: 06.06.2023 10:11:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TimeTypes](
	[TimeTypeId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_TimeTypes] PRIMARY KEY CLUSTERED 
(
	[TimeTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 06.06.2023 10:11:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[Login] [nvarchar](50) NOT NULL,
	[PasswordHash] [nvarchar](512) NOT NULL,
	[RoleId] [int] NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WeekTypes]    Script Date: 06.06.2023 10:11:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WeekTypes](
	[WeekTypeId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](20) NOT NULL,
 CONSTRAINT [PK_WeekTypes] PRIMARY KEY CLUSTERED 
(
	[WeekTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Classrooms] ON 

INSERT [dbo].[Classrooms] ([ClassroomId], [Cabinet], [IsDeleted]) VALUES (1, N'0108', 0)
INSERT [dbo].[Classrooms] ([ClassroomId], [Cabinet], [IsDeleted]) VALUES (2, N'0109', 0)
INSERT [dbo].[Classrooms] ([ClassroomId], [Cabinet], [IsDeleted]) VALUES (3, N'0110', 0)
INSERT [dbo].[Classrooms] ([ClassroomId], [Cabinet], [IsDeleted]) VALUES (4, N'0111', 0)
INSERT [dbo].[Classrooms] ([ClassroomId], [Cabinet], [IsDeleted]) VALUES (5, N'0114', 0)
INSERT [dbo].[Classrooms] ([ClassroomId], [Cabinet], [IsDeleted]) VALUES (6, N'0115', 0)
INSERT [dbo].[Classrooms] ([ClassroomId], [Cabinet], [IsDeleted]) VALUES (7, N'0200', 0)
INSERT [dbo].[Classrooms] ([ClassroomId], [Cabinet], [IsDeleted]) VALUES (8, N'0201', 0)
INSERT [dbo].[Classrooms] ([ClassroomId], [Cabinet], [IsDeleted]) VALUES (9, N'0201а', 0)
INSERT [dbo].[Classrooms] ([ClassroomId], [Cabinet], [IsDeleted]) VALUES (10, N'0202', 0)
INSERT [dbo].[Classrooms] ([ClassroomId], [Cabinet], [IsDeleted]) VALUES (11, N'0204', 0)
INSERT [dbo].[Classrooms] ([ClassroomId], [Cabinet], [IsDeleted]) VALUES (12, N'0205', 0)
INSERT [dbo].[Classrooms] ([ClassroomId], [Cabinet], [IsDeleted]) VALUES (13, N'0207', 0)
INSERT [dbo].[Classrooms] ([ClassroomId], [Cabinet], [IsDeleted]) VALUES (14, N'0209', 0)
INSERT [dbo].[Classrooms] ([ClassroomId], [Cabinet], [IsDeleted]) VALUES (15, N'0209а', 0)
INSERT [dbo].[Classrooms] ([ClassroomId], [Cabinet], [IsDeleted]) VALUES (16, N'0300', 0)
INSERT [dbo].[Classrooms] ([ClassroomId], [Cabinet], [IsDeleted]) VALUES (17, N'0301', 0)
INSERT [dbo].[Classrooms] ([ClassroomId], [Cabinet], [IsDeleted]) VALUES (18, N'0302', 0)
INSERT [dbo].[Classrooms] ([ClassroomId], [Cabinet], [IsDeleted]) VALUES (19, N'0303', 0)
INSERT [dbo].[Classrooms] ([ClassroomId], [Cabinet], [IsDeleted]) VALUES (20, N'0305', 0)
INSERT [dbo].[Classrooms] ([ClassroomId], [Cabinet], [IsDeleted]) VALUES (21, N'0306', 0)
INSERT [dbo].[Classrooms] ([ClassroomId], [Cabinet], [IsDeleted]) VALUES (22, N'0307', 0)
INSERT [dbo].[Classrooms] ([ClassroomId], [Cabinet], [IsDeleted]) VALUES (23, N'0308', 0)
INSERT [dbo].[Classrooms] ([ClassroomId], [Cabinet], [IsDeleted]) VALUES (24, N'0309', 0)
INSERT [dbo].[Classrooms] ([ClassroomId], [Cabinet], [IsDeleted]) VALUES (25, N'104', 0)
INSERT [dbo].[Classrooms] ([ClassroomId], [Cabinet], [IsDeleted]) VALUES (26, N'105', 0)
INSERT [dbo].[Classrooms] ([ClassroomId], [Cabinet], [IsDeleted]) VALUES (27, N'215', 0)
INSERT [dbo].[Classrooms] ([ClassroomId], [Cabinet], [IsDeleted]) VALUES (28, N'219', 0)
INSERT [dbo].[Classrooms] ([ClassroomId], [Cabinet], [IsDeleted]) VALUES (29, N'220', 0)
INSERT [dbo].[Classrooms] ([ClassroomId], [Cabinet], [IsDeleted]) VALUES (30, N'221', 0)
INSERT [dbo].[Classrooms] ([ClassroomId], [Cabinet], [IsDeleted]) VALUES (31, N'222', 0)
INSERT [dbo].[Classrooms] ([ClassroomId], [Cabinet], [IsDeleted]) VALUES (32, N'226', 0)
INSERT [dbo].[Classrooms] ([ClassroomId], [Cabinet], [IsDeleted]) VALUES (33, N'228', 0)
INSERT [dbo].[Classrooms] ([ClassroomId], [Cabinet], [IsDeleted]) VALUES (34, N'230', 0)
INSERT [dbo].[Classrooms] ([ClassroomId], [Cabinet], [IsDeleted]) VALUES (35, N'300', 0)
INSERT [dbo].[Classrooms] ([ClassroomId], [Cabinet], [IsDeleted]) VALUES (36, N'301', 0)
INSERT [dbo].[Classrooms] ([ClassroomId], [Cabinet], [IsDeleted]) VALUES (37, N'303', 0)
INSERT [dbo].[Classrooms] ([ClassroomId], [Cabinet], [IsDeleted]) VALUES (38, N'304', 0)
INSERT [dbo].[Classrooms] ([ClassroomId], [Cabinet], [IsDeleted]) VALUES (39, N'305', 0)
INSERT [dbo].[Classrooms] ([ClassroomId], [Cabinet], [IsDeleted]) VALUES (40, N'306', 0)
INSERT [dbo].[Classrooms] ([ClassroomId], [Cabinet], [IsDeleted]) VALUES (41, N'306а', 0)
INSERT [dbo].[Classrooms] ([ClassroomId], [Cabinet], [IsDeleted]) VALUES (42, N'307', 0)
INSERT [dbo].[Classrooms] ([ClassroomId], [Cabinet], [IsDeleted]) VALUES (43, N'308', 0)
INSERT [dbo].[Classrooms] ([ClassroomId], [Cabinet], [IsDeleted]) VALUES (44, N'309', 0)
INSERT [dbo].[Classrooms] ([ClassroomId], [Cabinet], [IsDeleted]) VALUES (45, N'311', 0)
INSERT [dbo].[Classrooms] ([ClassroomId], [Cabinet], [IsDeleted]) VALUES (46, N'312', 0)
INSERT [dbo].[Classrooms] ([ClassroomId], [Cabinet], [IsDeleted]) VALUES (47, N'314', 0)
INSERT [dbo].[Classrooms] ([ClassroomId], [Cabinet], [IsDeleted]) VALUES (48, N'315', 0)
INSERT [dbo].[Classrooms] ([ClassroomId], [Cabinet], [IsDeleted]) VALUES (49, N'317', 0)
INSERT [dbo].[Classrooms] ([ClassroomId], [Cabinet], [IsDeleted]) VALUES (50, N'401', 0)
INSERT [dbo].[Classrooms] ([ClassroomId], [Cabinet], [IsDeleted]) VALUES (51, N'402', 0)
INSERT [dbo].[Classrooms] ([ClassroomId], [Cabinet], [IsDeleted]) VALUES (52, N'403', 0)
INSERT [dbo].[Classrooms] ([ClassroomId], [Cabinet], [IsDeleted]) VALUES (53, N'404', 0)
INSERT [dbo].[Classrooms] ([ClassroomId], [Cabinet], [IsDeleted]) VALUES (54, N'404а', 0)
INSERT [dbo].[Classrooms] ([ClassroomId], [Cabinet], [IsDeleted]) VALUES (55, N'405', 0)
INSERT [dbo].[Classrooms] ([ClassroomId], [Cabinet], [IsDeleted]) VALUES (56, N'406', 0)
INSERT [dbo].[Classrooms] ([ClassroomId], [Cabinet], [IsDeleted]) VALUES (57, N'407', 0)
INSERT [dbo].[Classrooms] ([ClassroomId], [Cabinet], [IsDeleted]) VALUES (58, N'408', 0)
INSERT [dbo].[Classrooms] ([ClassroomId], [Cabinet], [IsDeleted]) VALUES (59, N'409', 0)
INSERT [dbo].[Classrooms] ([ClassroomId], [Cabinet], [IsDeleted]) VALUES (60, N'411', 0)
INSERT [dbo].[Classrooms] ([ClassroomId], [Cabinet], [IsDeleted]) VALUES (61, N'411а', 0)
INSERT [dbo].[Classrooms] ([ClassroomId], [Cabinet], [IsDeleted]) VALUES (62, N'413', 0)
INSERT [dbo].[Classrooms] ([ClassroomId], [Cabinet], [IsDeleted]) VALUES (63, N'414', 0)
INSERT [dbo].[Classrooms] ([ClassroomId], [Cabinet], [IsDeleted]) VALUES (64, N'416', 0)
INSERT [dbo].[Classrooms] ([ClassroomId], [Cabinet], [IsDeleted]) VALUES (65, N'417', 0)
INSERT [dbo].[Classrooms] ([ClassroomId], [Cabinet], [IsDeleted]) VALUES (66, N'418', 0)
SET IDENTITY_INSERT [dbo].[Classrooms] OFF
GO
INSERT [dbo].[Courses] ([CourseId]) VALUES (1)
INSERT [dbo].[Courses] ([CourseId]) VALUES (2)
INSERT [dbo].[Courses] ([CourseId]) VALUES (3)
INSERT [dbo].[Courses] ([CourseId]) VALUES (4)
INSERT [dbo].[Courses] ([CourseId]) VALUES (5)
GO
SET IDENTITY_INSERT [dbo].[Days] ON 

INSERT [dbo].[Days] ([DayId], [Name], [IsStudy]) VALUES (1, N'Понедельник', 1)
INSERT [dbo].[Days] ([DayId], [Name], [IsStudy]) VALUES (2, N'Вторник', 1)
INSERT [dbo].[Days] ([DayId], [Name], [IsStudy]) VALUES (3, N'Среда', 1)
INSERT [dbo].[Days] ([DayId], [Name], [IsStudy]) VALUES (4, N'Четверг', 1)
INSERT [dbo].[Days] ([DayId], [Name], [IsStudy]) VALUES (5, N'Пятница', 1)
INSERT [dbo].[Days] ([DayId], [Name], [IsStudy]) VALUES (6, N'Суббота', 1)
INSERT [dbo].[Days] ([DayId], [Name], [IsStudy]) VALUES (7, N'Воскресенье', 0)
SET IDENTITY_INSERT [dbo].[Days] OFF
GO
SET IDENTITY_INSERT [dbo].[DisciplineType] ON 

INSERT [dbo].[DisciplineType] ([DisciplineTypeId], [Name]) VALUES (3, N'Внекласная деятельность')
INSERT [dbo].[DisciplineType] ([DisciplineTypeId], [Name]) VALUES (1, N'Дисциплина')
INSERT [dbo].[DisciplineType] ([DisciplineTypeId], [Name]) VALUES (2, N'Практика')
SET IDENTITY_INSERT [dbo].[DisciplineType] OFF
GO
SET IDENTITY_INSERT [dbo].[Roles] ON 

INSERT [dbo].[Roles] ([RoleId], [Name]) VALUES (1, N'Admin')
INSERT [dbo].[Roles] ([RoleId], [Name]) VALUES (2, N'Editor')
SET IDENTITY_INSERT [dbo].[Roles] OFF
GO
SET IDENTITY_INSERT [dbo].[Terms] ON 

INSERT [dbo].[Terms] ([TermId], [CourseId], [CourseTerm]) VALUES (1, 1, 1)
INSERT [dbo].[Terms] ([TermId], [CourseId], [CourseTerm]) VALUES (2, 1, 2)
INSERT [dbo].[Terms] ([TermId], [CourseId], [CourseTerm]) VALUES (3, 2, 1)
INSERT [dbo].[Terms] ([TermId], [CourseId], [CourseTerm]) VALUES (4, 2, 2)
INSERT [dbo].[Terms] ([TermId], [CourseId], [CourseTerm]) VALUES (5, 3, 1)
INSERT [dbo].[Terms] ([TermId], [CourseId], [CourseTerm]) VALUES (6, 3, 2)
INSERT [dbo].[Terms] ([TermId], [CourseId], [CourseTerm]) VALUES (7, 4, 1)
INSERT [dbo].[Terms] ([TermId], [CourseId], [CourseTerm]) VALUES (8, 4, 2)
INSERT [dbo].[Terms] ([TermId], [CourseId], [CourseTerm]) VALUES (9, 5, 1)
INSERT [dbo].[Terms] ([TermId], [CourseId], [CourseTerm]) VALUES (10, 5, 2)
SET IDENTITY_INSERT [dbo].[Terms] OFF
GO
SET IDENTITY_INSERT [dbo].[Times] ON 

INSERT [dbo].[Times] ([TimeId], [Start], [End], [Duration], [LessonNumber], [TypeId], [IsDeleted]) VALUES (1, CAST(N'08:30:00' AS Time), CAST(N'10:10:00' AS Time), 2, 1, 1, 0)
INSERT [dbo].[Times] ([TimeId], [Start], [End], [Duration], [LessonNumber], [TypeId], [IsDeleted]) VALUES (2, CAST(N'10:20:00' AS Time), CAST(N'12:00:00' AS Time), 2, 2, 1, 0)
INSERT [dbo].[Times] ([TimeId], [Start], [End], [Duration], [LessonNumber], [TypeId], [IsDeleted]) VALUES (3, CAST(N'12:40:00' AS Time), CAST(N'14:20:00' AS Time), 2, 3, 1, 0)
INSERT [dbo].[Times] ([TimeId], [Start], [End], [Duration], [LessonNumber], [TypeId], [IsDeleted]) VALUES (4, CAST(N'14:30:00' AS Time), CAST(N'16:10:00' AS Time), 2, 4, 1, 0)
INSERT [dbo].[Times] ([TimeId], [Start], [End], [Duration], [LessonNumber], [TypeId], [IsDeleted]) VALUES (5, CAST(N'16:20:00' AS Time), CAST(N'18:00:00' AS Time), 2, 5, 1, 0)
INSERT [dbo].[Times] ([TimeId], [Start], [End], [Duration], [LessonNumber], [TypeId], [IsDeleted]) VALUES (6, CAST(N'08:30:00' AS Time), CAST(N'09:45:00' AS Time), 2, 1, 2, 0)
INSERT [dbo].[Times] ([TimeId], [Start], [End], [Duration], [LessonNumber], [TypeId], [IsDeleted]) VALUES (7, CAST(N'09:55:00' AS Time), CAST(N'11:10:00' AS Time), 2, 2, 2, 0)
INSERT [dbo].[Times] ([TimeId], [Start], [End], [Duration], [LessonNumber], [TypeId], [IsDeleted]) VALUES (8, CAST(N'11:40:00' AS Time), CAST(N'12:55:00' AS Time), 2, 3, 2, 0)
INSERT [dbo].[Times] ([TimeId], [Start], [End], [Duration], [LessonNumber], [TypeId], [IsDeleted]) VALUES (9, CAST(N'13:05:00' AS Time), CAST(N'14:20:00' AS Time), 2, 4, 2, 0)
INSERT [dbo].[Times] ([TimeId], [Start], [End], [Duration], [LessonNumber], [TypeId], [IsDeleted]) VALUES (10, CAST(N'14:30:00' AS Time), CAST(N'15:45:00' AS Time), 2, 5, 2, 0)
INSERT [dbo].[Times] ([TimeId], [Start], [End], [Duration], [LessonNumber], [TypeId], [IsDeleted]) VALUES (11, CAST(N'08:30:00' AS Time), CAST(N'09:15:00' AS Time), 1, 0, 3, 0)
INSERT [dbo].[Times] ([TimeId], [Start], [End], [Duration], [LessonNumber], [TypeId], [IsDeleted]) VALUES (12, CAST(N'09:20:00' AS Time), CAST(N'11:00:00' AS Time), 2, 1, 3, 0)
INSERT [dbo].[Times] ([TimeId], [Start], [End], [Duration], [LessonNumber], [TypeId], [IsDeleted]) VALUES (13, CAST(N'11:10:00' AS Time), CAST(N'12:50:00' AS Time), 2, 2, 3, 0)
INSERT [dbo].[Times] ([TimeId], [Start], [End], [Duration], [LessonNumber], [TypeId], [IsDeleted]) VALUES (14, CAST(N'13:30:00' AS Time), CAST(N'15:10:00' AS Time), 2, 3, 3, 0)
INSERT [dbo].[Times] ([TimeId], [Start], [End], [Duration], [LessonNumber], [TypeId], [IsDeleted]) VALUES (15, CAST(N'15:20:00' AS Time), CAST(N'17:00:00' AS Time), 2, 4, 3, 0)
INSERT [dbo].[Times] ([TimeId], [Start], [End], [Duration], [LessonNumber], [TypeId], [IsDeleted]) VALUES (16, CAST(N'17:10:00' AS Time), CAST(N'18:50:00' AS Time), 2, 5, 3, 0)
SET IDENTITY_INSERT [dbo].[Times] OFF
GO
SET IDENTITY_INSERT [dbo].[TimeTypes] ON 

INSERT [dbo].[TimeTypes] ([TimeTypeId], [Name], [IsDeleted]) VALUES (1, N'Стандартное', 0)
INSERT [dbo].[TimeTypes] ([TimeTypeId], [Name], [IsDeleted]) VALUES (2, N'Сокращенное', 0)
INSERT [dbo].[TimeTypes] ([TimeTypeId], [Name], [IsDeleted]) VALUES (3, N'Понедельник', 0)
SET IDENTITY_INSERT [dbo].[TimeTypes] OFF
GO
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([UserId], [Login], [PasswordHash], [RoleId]) VALUES (1, N'Admin', N'$2a$11$/AKGJmbjT9.J/pdMmIk7S.VItgYYrknXhoPAUsTRIUqzIUXVw25zq', 1)
INSERT [dbo].[Users] ([UserId], [Login], [PasswordHash], [RoleId]) VALUES (2, N'Editor', N'$2a$11$qtS1HuNq4Q/9/gnERQJunu9U0wEYvtxbN2Z8senRvOLUF1gn/OV3i', 2)
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
SET IDENTITY_INSERT [dbo].[WeekTypes] ON 

INSERT [dbo].[WeekTypes] ([WeekTypeId], [Name]) VALUES (1, N'Знаменатель')
INSERT [dbo].[WeekTypes] ([WeekTypeId], [Name]) VALUES (2, N'Числитель')
SET IDENTITY_INSERT [dbo].[WeekTypes] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Classrooms]    Script Date: 06.06.2023 10:11:12 ******/
ALTER TABLE [dbo].[Classrooms] ADD  CONSTRAINT [IX_Classrooms] UNIQUE NONCLUSTERED 
(
	[Cabinet] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Dates]    Script Date: 06.06.2023 10:11:12 ******/
ALTER TABLE [dbo].[Dates] ADD  CONSTRAINT [IX_Dates] UNIQUE NONCLUSTERED 
(
	[Value] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Days]    Script Date: 06.06.2023 10:11:12 ******/
ALTER TABLE [dbo].[Days] ADD  CONSTRAINT [IX_Days] UNIQUE NONCLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Disciplines]    Script Date: 06.06.2023 10:11:12 ******/
ALTER TABLE [dbo].[Disciplines] ADD  CONSTRAINT [IX_Disciplines] UNIQUE NONCLUSTERED 
(
	[Code] ASC,
	[Name] ASC,
	[SpecialityId] ASC,
	[TermId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_DisciplineType]    Script Date: 06.06.2023 10:11:12 ******/
ALTER TABLE [dbo].[DisciplineType] ADD  CONSTRAINT [IX_DisciplineType] UNIQUE NONCLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Groups]    Script Date: 06.06.2023 10:11:12 ******/
ALTER TABLE [dbo].[Groups] ADD  CONSTRAINT [IX_Groups] UNIQUE NONCLUSTERED 
(
	[Number] ASC,
	[EnrollmentYear] ASC,
	[SpecialityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Lessons]    Script Date: 06.06.2023 10:11:12 ******/
CREATE NONCLUSTERED INDEX [IX_Lessons] ON [dbo].[Lessons]
(
	[Number] ASC,
	[TimetableId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_LessonTemplates]    Script Date: 06.06.2023 10:11:12 ******/
ALTER TABLE [dbo].[LessonTemplates] ADD  CONSTRAINT [IX_LessonTemplates] UNIQUE NONCLUSTERED 
(
	[TemplateId] ASC,
	[Number] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Roles]    Script Date: 06.06.2023 10:11:12 ******/
ALTER TABLE [dbo].[Roles] ADD  CONSTRAINT [IX_Roles] UNIQUE NONCLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Specialities]    Script Date: 06.06.2023 10:11:12 ******/
ALTER TABLE [dbo].[Specialities] ADD  CONSTRAINT [IX_Specialities] UNIQUE NONCLUSTERED 
(
	[Code] ASC,
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Teachers]    Script Date: 06.06.2023 10:11:12 ******/
ALTER TABLE [dbo].[Teachers] ADD  CONSTRAINT [IX_Teachers] UNIQUE NONCLUSTERED 
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Templates_1]    Script Date: 06.06.2023 10:11:12 ******/
ALTER TABLE [dbo].[Templates] ADD  CONSTRAINT [IX_Templates_1] UNIQUE NONCLUSTERED 
(
	[GroupId] ASC,
	[TermId] ASC,
	[DayId] ASC,
	[WeekTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Terms]    Script Date: 06.06.2023 10:11:12 ******/
ALTER TABLE [dbo].[Terms] ADD  CONSTRAINT [IX_Terms] UNIQUE NONCLUSTERED 
(
	[CourseId] ASC,
	[CourseTerm] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Times]    Script Date: 06.06.2023 10:11:12 ******/
ALTER TABLE [dbo].[Times] ADD  CONSTRAINT [IX_Times] UNIQUE NONCLUSTERED 
(
	[TypeId] ASC,
	[LessonNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Timetables]    Script Date: 06.06.2023 10:11:12 ******/
ALTER TABLE [dbo].[Timetables] ADD  CONSTRAINT [IX_Timetables] UNIQUE NONCLUSTERED 
(
	[DateId] ASC,
	[GroupId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_TimeTypes]    Script Date: 06.06.2023 10:11:12 ******/
ALTER TABLE [dbo].[TimeTypes] ADD  CONSTRAINT [IX_TimeTypes] UNIQUE NONCLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Users]    Script Date: 06.06.2023 10:11:12 ******/
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [IX_Users] UNIQUE NONCLUSTERED 
(
	[Login] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_WeekTypes]    Script Date: 06.06.2023 10:11:12 ******/
ALTER TABLE [dbo].[WeekTypes] ADD  CONSTRAINT [IX_WeekTypes] UNIQUE NONCLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Classrooms] ADD  CONSTRAINT [DF_Classrooms_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[Disciplines] ADD  CONSTRAINT [DF_Disciplines_DisciplineTypeId]  DEFAULT ((1)) FOR [DisciplineTypeId]
GO
ALTER TABLE [dbo].[Disciplines] ADD  CONSTRAINT [DF_Disciplines_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[Groups] ADD  CONSTRAINT [DF_Groups_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[GroupTransfers] ADD  CONSTRAINT [DF_TransferingGroupsHistory_IsTransfer]  DEFAULT ((0)) FOR [IsTransferred]
GO
ALTER TABLE [dbo].[Lessons] ADD  CONSTRAINT [DF_Pairs_IsChanged]  DEFAULT ((0)) FOR [IsChanged]
GO
ALTER TABLE [dbo].[Specialities] ADD  CONSTRAINT [DF_SpecialityCodes_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[Teachers] ADD  CONSTRAINT [DF_Teachers_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[Times] ADD  CONSTRAINT [DF_Times_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[TimeTypes] ADD  CONSTRAINT [DF_TimeTypes_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[Dates]  WITH CHECK ADD  CONSTRAINT [FK_Dates_Days] FOREIGN KEY([DayId])
REFERENCES [dbo].[Days] ([DayId])
GO
ALTER TABLE [dbo].[Dates] CHECK CONSTRAINT [FK_Dates_Days]
GO
ALTER TABLE [dbo].[Dates]  WITH CHECK ADD  CONSTRAINT [FK_Dates_WeekTypes] FOREIGN KEY([WeekTypeId])
REFERENCES [dbo].[WeekTypes] ([WeekTypeId])
GO
ALTER TABLE [dbo].[Dates] CHECK CONSTRAINT [FK_Dates_WeekTypes]
GO
ALTER TABLE [dbo].[Disciplines]  WITH CHECK ADD  CONSTRAINT [FK_Disciplines_DisciplineType] FOREIGN KEY([DisciplineTypeId])
REFERENCES [dbo].[DisciplineType] ([DisciplineTypeId])
GO
ALTER TABLE [dbo].[Disciplines] CHECK CONSTRAINT [FK_Disciplines_DisciplineType]
GO
ALTER TABLE [dbo].[Disciplines]  WITH CHECK ADD  CONSTRAINT [FK_Disciplines_Specialities] FOREIGN KEY([SpecialityId])
REFERENCES [dbo].[Specialities] ([SpecialityId])
GO
ALTER TABLE [dbo].[Disciplines] CHECK CONSTRAINT [FK_Disciplines_Specialities]
GO
ALTER TABLE [dbo].[Disciplines]  WITH CHECK ADD  CONSTRAINT [FK_Disciplines_Terms] FOREIGN KEY([TermId])
REFERENCES [dbo].[Terms] ([TermId])
GO
ALTER TABLE [dbo].[Disciplines] CHECK CONSTRAINT [FK_Disciplines_Terms]
GO
ALTER TABLE [dbo].[GroupGroups]  WITH CHECK ADD  CONSTRAINT [FK_GroupGroups_Groups] FOREIGN KEY([GroupId])
REFERENCES [dbo].[Groups] ([GroupId])
GO
ALTER TABLE [dbo].[GroupGroups] CHECK CONSTRAINT [FK_GroupGroups_Groups]
GO
ALTER TABLE [dbo].[GroupGroups]  WITH CHECK ADD  CONSTRAINT [FK_GroupGroups_Groups1] FOREIGN KEY([GroupId2])
REFERENCES [dbo].[Groups] ([GroupId])
GO
ALTER TABLE [dbo].[GroupGroups] CHECK CONSTRAINT [FK_GroupGroups_Groups1]
GO
ALTER TABLE [dbo].[Groups]  WITH CHECK ADD  CONSTRAINT [FK_Groups_Specialities] FOREIGN KEY([SpecialityId])
REFERENCES [dbo].[Specialities] ([SpecialityId])
GO
ALTER TABLE [dbo].[Groups] CHECK CONSTRAINT [FK_Groups_Specialities]
GO
ALTER TABLE [dbo].[Groups]  WITH CHECK ADD  CONSTRAINT [FK_Groups_Terms] FOREIGN KEY([TermId])
REFERENCES [dbo].[Terms] ([TermId])
GO
ALTER TABLE [dbo].[Groups] CHECK CONSTRAINT [FK_Groups_Terms]
GO
ALTER TABLE [dbo].[GroupTransfers]  WITH CHECK ADD  CONSTRAINT [FK_GroupTransfers_Terms] FOREIGN KEY([NextTermId])
REFERENCES [dbo].[Terms] ([TermId])
GO
ALTER TABLE [dbo].[GroupTransfers] CHECK CONSTRAINT [FK_GroupTransfers_Terms]
GO
ALTER TABLE [dbo].[GroupTransfers]  WITH CHECK ADD  CONSTRAINT [FK_TransferingGroupsHistory_Groups] FOREIGN KEY([GroupId])
REFERENCES [dbo].[Groups] ([GroupId])
GO
ALTER TABLE [dbo].[GroupTransfers] CHECK CONSTRAINT [FK_TransferingGroupsHistory_Groups]
GO
ALTER TABLE [dbo].[Lessons]  WITH CHECK ADD  CONSTRAINT [FK_Pairs_Disciplines] FOREIGN KEY([DisciplineId])
REFERENCES [dbo].[Disciplines] ([DisciplineId])
GO
ALTER TABLE [dbo].[Lessons] CHECK CONSTRAINT [FK_Pairs_Disciplines]
GO
ALTER TABLE [dbo].[Lessons]  WITH CHECK ADD  CONSTRAINT [FK_Pairs_Times] FOREIGN KEY([TimeId])
REFERENCES [dbo].[Times] ([TimeId])
GO
ALTER TABLE [dbo].[Lessons] CHECK CONSTRAINT [FK_Pairs_Times]
GO
ALTER TABLE [dbo].[Lessons]  WITH CHECK ADD  CONSTRAINT [FK_Pairs_Timetables] FOREIGN KEY([TimetableId])
REFERENCES [dbo].[Timetables] ([TimetableId])
GO
ALTER TABLE [dbo].[Lessons] CHECK CONSTRAINT [FK_Pairs_Timetables]
GO
ALTER TABLE [dbo].[LessonTeacherClassrooms]  WITH CHECK ADD  CONSTRAINT [FK_LessonTeacherClassrooms_Classrooms2] FOREIGN KEY([ClassroomId])
REFERENCES [dbo].[Classrooms] ([ClassroomId])
GO
ALTER TABLE [dbo].[LessonTeacherClassrooms] CHECK CONSTRAINT [FK_LessonTeacherClassrooms_Classrooms2]
GO
ALTER TABLE [dbo].[LessonTeacherClassrooms]  WITH CHECK ADD  CONSTRAINT [FK_LessonTeacherClassrooms_Lessons] FOREIGN KEY([LessonId])
REFERENCES [dbo].[Lessons] ([LessonId])
GO
ALTER TABLE [dbo].[LessonTeacherClassrooms] CHECK CONSTRAINT [FK_LessonTeacherClassrooms_Lessons]
GO
ALTER TABLE [dbo].[LessonTeacherClassrooms]  WITH CHECK ADD  CONSTRAINT [FK_LessonTeacherClassrooms_Teachers1] FOREIGN KEY([TeacherId])
REFERENCES [dbo].[Teachers] ([TeacherId])
GO
ALTER TABLE [dbo].[LessonTeacherClassrooms] CHECK CONSTRAINT [FK_LessonTeacherClassrooms_Teachers1]
GO
ALTER TABLE [dbo].[LessonTemplates]  WITH CHECK ADD  CONSTRAINT [FK_LessonTemplates_Disciplines] FOREIGN KEY([DisciplineId])
REFERENCES [dbo].[Disciplines] ([DisciplineId])
GO
ALTER TABLE [dbo].[LessonTemplates] CHECK CONSTRAINT [FK_LessonTemplates_Disciplines]
GO
ALTER TABLE [dbo].[LessonTemplates]  WITH CHECK ADD  CONSTRAINT [FK_LessonTemplates_Templates] FOREIGN KEY([TemplateId])
REFERENCES [dbo].[Templates] ([TemplateId])
GO
ALTER TABLE [dbo].[LessonTemplates] CHECK CONSTRAINT [FK_LessonTemplates_Templates]
GO
ALTER TABLE [dbo].[LessonTemplates]  WITH CHECK ADD  CONSTRAINT [FK_LessonTemplates_Times] FOREIGN KEY([TimeId])
REFERENCES [dbo].[Times] ([TimeId])
GO
ALTER TABLE [dbo].[LessonTemplates] CHECK CONSTRAINT [FK_LessonTemplates_Times]
GO
ALTER TABLE [dbo].[LessonTemplateTeacherClassrooms]  WITH CHECK ADD  CONSTRAINT [FK_LessonTemplateTeacherClassrooms_Classrooms] FOREIGN KEY([ClassroomId])
REFERENCES [dbo].[Classrooms] ([ClassroomId])
GO
ALTER TABLE [dbo].[LessonTemplateTeacherClassrooms] CHECK CONSTRAINT [FK_LessonTemplateTeacherClassrooms_Classrooms]
GO
ALTER TABLE [dbo].[LessonTemplateTeacherClassrooms]  WITH CHECK ADD  CONSTRAINT [FK_LessonTemplateTeacherClassrooms_LessonTemplates] FOREIGN KEY([LessonTemplateId])
REFERENCES [dbo].[LessonTemplates] ([LessonTemplateId])
GO
ALTER TABLE [dbo].[LessonTemplateTeacherClassrooms] CHECK CONSTRAINT [FK_LessonTemplateTeacherClassrooms_LessonTemplates]
GO
ALTER TABLE [dbo].[LessonTemplateTeacherClassrooms]  WITH CHECK ADD  CONSTRAINT [FK_LessonTemplateTeacherClassrooms_Teachers] FOREIGN KEY([TeacherId])
REFERENCES [dbo].[Teachers] ([TeacherId])
GO
ALTER TABLE [dbo].[LessonTemplateTeacherClassrooms] CHECK CONSTRAINT [FK_LessonTemplateTeacherClassrooms_Teachers]
GO
ALTER TABLE [dbo].[Sessions]  WITH CHECK ADD  CONSTRAINT [FK_Sessions_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[Sessions] CHECK CONSTRAINT [FK_Sessions_Users]
GO
ALTER TABLE [dbo].[Specialities]  WITH CHECK ADD  CONSTRAINT [FK_Specialities_Terms] FOREIGN KEY([MaxTermId])
REFERENCES [dbo].[Terms] ([TermId])
GO
ALTER TABLE [dbo].[Specialities] CHECK CONSTRAINT [FK_Specialities_Terms]
GO
ALTER TABLE [dbo].[Templates]  WITH CHECK ADD  CONSTRAINT [FK_Templates_Days] FOREIGN KEY([DayId])
REFERENCES [dbo].[Days] ([DayId])
GO
ALTER TABLE [dbo].[Templates] CHECK CONSTRAINT [FK_Templates_Days]
GO
ALTER TABLE [dbo].[Templates]  WITH CHECK ADD  CONSTRAINT [FK_Templates_Groups] FOREIGN KEY([GroupId])
REFERENCES [dbo].[Groups] ([GroupId])
GO
ALTER TABLE [dbo].[Templates] CHECK CONSTRAINT [FK_Templates_Groups]
GO
ALTER TABLE [dbo].[Templates]  WITH CHECK ADD  CONSTRAINT [FK_Templates_Terms] FOREIGN KEY([TermId])
REFERENCES [dbo].[Terms] ([TermId])
GO
ALTER TABLE [dbo].[Templates] CHECK CONSTRAINT [FK_Templates_Terms]
GO
ALTER TABLE [dbo].[Templates]  WITH CHECK ADD  CONSTRAINT [FK_Templates_WeekTypes] FOREIGN KEY([WeekTypeId])
REFERENCES [dbo].[WeekTypes] ([WeekTypeId])
GO
ALTER TABLE [dbo].[Templates] CHECK CONSTRAINT [FK_Templates_WeekTypes]
GO
ALTER TABLE [dbo].[Terms]  WITH CHECK ADD  CONSTRAINT [FK_Terms_Courses] FOREIGN KEY([CourseId])
REFERENCES [dbo].[Courses] ([CourseId])
GO
ALTER TABLE [dbo].[Terms] CHECK CONSTRAINT [FK_Terms_Courses]
GO
ALTER TABLE [dbo].[Times]  WITH CHECK ADD  CONSTRAINT [FK_Times_TimeTypes] FOREIGN KEY([TypeId])
REFERENCES [dbo].[TimeTypes] ([TimeTypeId])
GO
ALTER TABLE [dbo].[Times] CHECK CONSTRAINT [FK_Times_TimeTypes]
GO
ALTER TABLE [dbo].[Timetables]  WITH CHECK ADD  CONSTRAINT [FK_Timetables_Dates] FOREIGN KEY([DateId])
REFERENCES [dbo].[Dates] ([DateId])
GO
ALTER TABLE [dbo].[Timetables] CHECK CONSTRAINT [FK_Timetables_Dates]
GO
ALTER TABLE [dbo].[Timetables]  WITH CHECK ADD  CONSTRAINT [FK_Timetables_Groups] FOREIGN KEY([GroupId])
REFERENCES [dbo].[Groups] ([GroupId])
GO
ALTER TABLE [dbo].[Timetables] CHECK CONSTRAINT [FK_Timetables_Groups]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_Roles] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Roles] ([RoleId])
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_Roles]
GO
ALTER TABLE [dbo].[Dates]  WITH CHECK ADD  CONSTRAINT [CK_Dates] CHECK  (([Term]>=(1) OR [Term]<=(2)))
GO
ALTER TABLE [dbo].[Dates] CHECK CONSTRAINT [CK_Dates]
GO
ALTER TABLE [dbo].[GroupGroups]  WITH CHECK ADD  CONSTRAINT [CK_GroupGroups] CHECK  (([GroupId]<>[GroupId2]))
GO
ALTER TABLE [dbo].[GroupGroups] CHECK CONSTRAINT [CK_GroupGroups]
GO
ALTER TABLE [dbo].[Lessons]  WITH CHECK ADD  CONSTRAINT [CK_Lessons] CHECK  (([Subgroup] IS NULL OR [Subgroup]>=(1) AND [Subgroup]<=(2)))
GO
ALTER TABLE [dbo].[Lessons] CHECK CONSTRAINT [CK_Lessons]
GO
ALTER TABLE [dbo].[LessonTemplates]  WITH CHECK ADD  CONSTRAINT [CK_LessonTemplates] CHECK  (([Subgroup] IS NULL OR [Subgroup]>=(1) AND [Subgroup]<=(2)))
GO
ALTER TABLE [dbo].[LessonTemplates] CHECK CONSTRAINT [CK_LessonTemplates]
GO
/****** Object:  Trigger [dbo].[Classrooms_Delete]    Script Date: 06.06.2023 10:11:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TRIGGER [dbo].[Classrooms_Delete]
ON [dbo].[Classrooms]
INSTEAD OF DELETE
AS
BEGIN
    UPDATE Classrooms
    SET IsDeleted = 1
    WHERE ClassroomId IN (SELECT ClassroomId FROM deleted)
END
GO
ALTER TABLE [dbo].[Classrooms] ENABLE TRIGGER [Classrooms_Delete]
GO
/****** Object:  Trigger [dbo].[Disciplines_Delete]    Script Date: 06.06.2023 10:11:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TRIGGER [dbo].[Disciplines_Delete]
ON [dbo].[Disciplines]
INSTEAD OF DELETE
AS
BEGIN
    UPDATE Disciplines
    SET IsDeleted = 1
    WHERE DisciplineId IN (SELECT DisciplineId FROM deleted)
END
GO
ALTER TABLE [dbo].[Disciplines] ENABLE TRIGGER [Disciplines_Delete]
GO
/****** Object:  Trigger [dbo].[Groups_Delete]    Script Date: 06.06.2023 10:11:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TRIGGER [dbo].[Groups_Delete]
ON [dbo].[Groups]
INSTEAD OF DELETE
AS
BEGIN
    UPDATE Groups
    SET IsDeleted = 1
    WHERE GroupId IN (SELECT GroupId FROM deleted)
END
GO
ALTER TABLE [dbo].[Groups] ENABLE TRIGGER [Groups_Delete]
GO
/****** Object:  Trigger [dbo].[Specialities_Delete]    Script Date: 06.06.2023 10:11:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TRIGGER [dbo].[Specialities_Delete]
ON [dbo].[Specialities]
INSTEAD OF DELETE
AS
BEGIN
    UPDATE Specialities
    SET IsDeleted = 1
    WHERE SpecialityId IN (SELECT SpecialityId FROM deleted)
END
GO
ALTER TABLE [dbo].[Specialities] ENABLE TRIGGER [Specialities_Delete]
GO
/****** Object:  Trigger [dbo].[Teachers_Delete]    Script Date: 06.06.2023 10:11:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TRIGGER [dbo].[Teachers_Delete]
ON [dbo].[Teachers]
INSTEAD OF DELETE
AS
BEGIN
    UPDATE Teachers
    SET IsDeleted = 1
    WHERE TeacherId IN (SELECT TeacherId FROM deleted)
END
GO
ALTER TABLE [dbo].[Teachers] ENABLE TRIGGER [Teachers_Delete]
GO
/****** Object:  Trigger [dbo].[Times_Delete]    Script Date: 06.06.2023 10:11:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TRIGGER [dbo].[Times_Delete]
ON [dbo].[Times]
INSTEAD OF DELETE
AS
BEGIN
    UPDATE Times
    SET IsDeleted = 1
    WHERE TimeId IN (SELECT TimeId FROM deleted)
END
GO
ALTER TABLE [dbo].[Times] ENABLE TRIGGER [Times_Delete]
GO
/****** Object:  Trigger [dbo].[TimeTypes_Delete]    Script Date: 06.06.2023 10:11:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TRIGGER [dbo].[TimeTypes_Delete]
ON [dbo].[TimeTypes]
INSTEAD OF DELETE
AS
BEGIN
    UPDATE TimeTypes
    SET IsDeleted = 1
    WHERE TimeTypeId IN (SELECT TimeTypeId FROM deleted)
END
GO
ALTER TABLE [dbo].[TimeTypes] ENABLE TRIGGER [TimeTypes_Delete]
GO
CREATE INDEX IX_Timetables_DateId
ON Timetables (DateId)
GO
CREATE INDEX IX_Timetables_GroupId
ON Timetables (GroupId)
GO
CREATE INDEX IX_Templates_GroupId
ON Templates (GroupId)
GO
USE [master]
GO
ALTER DATABASE [Schedule] SET  READ_WRITE 
GO