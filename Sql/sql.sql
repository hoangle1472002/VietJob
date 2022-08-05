USE [master]
GO
/****** Object:  Database [JobPortalWeb]    Script Date: 5/6/2022 9:35:07 AM ******/
CREATE DATABASE [JobPortalWeb]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'JobPortalWeb', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\JobPortalWeb.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'JobPortalWeb_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\JobPortalWeb_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [JobPortalWeb] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [JobPortalWeb].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [JobPortalWeb] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [JobPortalWeb] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [JobPortalWeb] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [JobPortalWeb] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [JobPortalWeb] SET ARITHABORT OFF 
GO
ALTER DATABASE [JobPortalWeb] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [JobPortalWeb] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [JobPortalWeb] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [JobPortalWeb] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [JobPortalWeb] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [JobPortalWeb] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [JobPortalWeb] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [JobPortalWeb] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [JobPortalWeb] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [JobPortalWeb] SET  DISABLE_BROKER 
GO
ALTER DATABASE [JobPortalWeb] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [JobPortalWeb] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [JobPortalWeb] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [JobPortalWeb] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [JobPortalWeb] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [JobPortalWeb] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [JobPortalWeb] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [JobPortalWeb] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [JobPortalWeb] SET  MULTI_USER 
GO
ALTER DATABASE [JobPortalWeb] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [JobPortalWeb] SET DB_CHAINING OFF 
GO
ALTER DATABASE [JobPortalWeb] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [JobPortalWeb] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [JobPortalWeb] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [JobPortalWeb] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [JobPortalWeb] SET QUERY_STORE = OFF
GO
USE [JobPortalWeb]
GO
/****** Object:  Table [dbo].[Company]    Script Date: 5/6/2022 9:35:08 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Company](
	[id] [int] NOT NULL,
	[company_name] [varchar](100) NOT NULL,
	[profile_description] [nvarchar](4000) NULL,
	[establishment_date] [date] NOT NULL,
	[company_website_url] [varchar](500) NOT NULL,
	[company_logo_url] [varchar](500) NOT NULL,
 CONSTRAINT [company_pk] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EducationDetail]    Script Date: 5/6/2022 9:35:08 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EducationDetail](
	[user_account_id] [int] NOT NULL,
	[certificate_degree_name] [varchar](50) NOT NULL,
	[major] [varchar](50) NOT NULL,
	[Institute_university_name] [varchar](50) NOT NULL,
 CONSTRAINT [education_detail_pk] PRIMARY KEY CLUSTERED 
(
	[user_account_id] ASC,
	[certificate_degree_name] ASC,
	[major] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ExperienceDetail]    Script Date: 5/6/2022 9:35:08 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ExperienceDetail](
	[user_account_id] [int] NOT NULL,
	[start_date] [date] NOT NULL,
	[end_date] [date] NOT NULL,
	[job_title] [varchar](50) NOT NULL,
	[company_name] [varchar](100) NOT NULL,
	[description] [nvarchar](4000) NULL,
 CONSTRAINT [experience_detail_pk] PRIMARY KEY CLUSTERED 
(
	[user_account_id] ASC,
	[start_date] ASC,
	[end_date] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[JobPost]    Script Date: 5/6/2022 9:35:08 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[JobPost](
	[id] [int] NOT NULL,
	[job_type_id] [int] NOT NULL,
	[company_id] [int] NOT NULL,
	[created_date] [date] NOT NULL,
	[job_description] [nvarchar](1000) NULL,
	[job_location] [nvarchar](100) NULL,
 CONSTRAINT [job_post_pk] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[JobPostActivity]    Script Date: 5/6/2022 9:35:08 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[JobPostActivity](
	[user_account_id] [int] NOT NULL,
	[job_post_id] [int] NOT NULL,
	[apply_date] [date] NOT NULL,
 CONSTRAINT [job_post_activity_pk] PRIMARY KEY CLUSTERED 
(
	[user_account_id] ASC,
	[job_post_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[JobType]    Script Date: 5/6/2022 9:35:08 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[JobType](
	[id] [int] NOT NULL,
	[job_type] [varchar](20) NOT NULL,
 CONSTRAINT [job_type_pk] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SeekerProfile]    Script Date: 5/6/2022 9:35:08 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SeekerProfile](
	[user_account_id] [int] NOT NULL,
	[first_name] [nvarchar](10) NULL,
	[last_name] [nvarchar](10) NULL,
 CONSTRAINT [seeker_profile_pk] PRIMARY KEY CLUSTERED 
(
	[user_account_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserAccount]    Script Date: 5/6/2022 9:35:08 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserAccount](
	[id] [int] NOT NULL,
	[user_type_id] [int] NOT NULL,
	[email] [varchar](255) NOT NULL,
	[password] [varchar](100) NOT NULL,
	[date_of_birth] [date] NOT NULL,
	[gender] [char](1) NOT NULL,
	[contact_number] [varchar](10) NOT NULL,
	[user_image_url] [varchar](500) NULL,
	[registration_date] [date] NOT NULL,
 CONSTRAINT [user_account_pk] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserType]    Script Date: 5/6/2022 9:35:08 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserType](
	[id] [int] NOT NULL,
	[user_type_name] [varchar](20) NOT NULL,
 CONSTRAINT [user_type_pk] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[EducationDetail]  WITH CHECK ADD  CONSTRAINT [educ_dtl_seeker_profile] FOREIGN KEY([user_account_id])
REFERENCES [dbo].[SeekerProfile] ([user_account_id])
GO
ALTER TABLE [dbo].[EducationDetail] CHECK CONSTRAINT [educ_dtl_seeker_profile]
GO
ALTER TABLE [dbo].[ExperienceDetail]  WITH CHECK ADD  CONSTRAINT [exp_dtl_seeker_profile] FOREIGN KEY([user_account_id])
REFERENCES [dbo].[SeekerProfile] ([user_account_id])
GO
ALTER TABLE [dbo].[ExperienceDetail] CHECK CONSTRAINT [exp_dtl_seeker_profile]
GO
ALTER TABLE [dbo].[JobPost]  WITH CHECK ADD  CONSTRAINT [job_post_company] FOREIGN KEY([company_id])
REFERENCES [dbo].[Company] ([id])
GO
ALTER TABLE [dbo].[JobPost] CHECK CONSTRAINT [job_post_company]
GO
ALTER TABLE [dbo].[JobPost]  WITH CHECK ADD  CONSTRAINT [job_post_job_type] FOREIGN KEY([job_type_id])
REFERENCES [dbo].[JobType] ([id])
GO
ALTER TABLE [dbo].[JobPost] CHECK CONSTRAINT [job_post_job_type]
GO
ALTER TABLE [dbo].[JobPostActivity]  WITH CHECK ADD  CONSTRAINT [job_post_act_user_register] FOREIGN KEY([user_account_id])
REFERENCES [dbo].[UserAccount] ([id])
GO
ALTER TABLE [dbo].[JobPostActivity] CHECK CONSTRAINT [job_post_act_user_register]
GO
ALTER TABLE [dbo].[JobPostActivity]  WITH CHECK ADD  CONSTRAINT [job_post_activity_job_post] FOREIGN KEY([job_post_id])
REFERENCES [dbo].[JobPost] ([id])
GO
ALTER TABLE [dbo].[JobPostActivity] CHECK CONSTRAINT [job_post_activity_job_post]
GO
ALTER TABLE [dbo].[SeekerProfile]  WITH CHECK ADD  CONSTRAINT [seeker_profile_user_register] FOREIGN KEY([user_account_id])
REFERENCES [dbo].[UserAccount] ([id])
GO
ALTER TABLE [dbo].[SeekerProfile] CHECK CONSTRAINT [seeker_profile_user_register]
GO
ALTER TABLE [dbo].[UserAccount]  WITH CHECK ADD  CONSTRAINT [user_register_user_type] FOREIGN KEY([user_type_id])
REFERENCES [dbo].[UserType] ([id])
GO
ALTER TABLE [dbo].[UserAccount] CHECK CONSTRAINT [user_register_user_type]
GO
USE [master]
GO
ALTER DATABASE [JobPortalWeb] SET  READ_WRITE 
GO
