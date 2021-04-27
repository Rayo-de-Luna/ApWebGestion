/*
Archivo de colsulta de Microsft SQL, para la creación de la Base de Datos AWGestionDB,
así como de algunos Procedimientos Almacenados necesarios para la
administración de la misma.

Abrir este archivo con Microsoft SQL Server Management Studio,
conectar con el servicio de bases de datos destino,
dar Ctrl + A para seleccionar todo el contenido de este archivo de consulta
y dar clic en Ejecutar (o la tecla F5) para ejecutar los comandos.
*/

USE [master]
GO
/****** Object:  Database [AWGestionDB]    Script Date: 16/05/2020 06:46:10 a. m. ******/
CREATE DATABASE [AWGestionDB]
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [AWGestionDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [AWGestionDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [AWGestionDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [AWGestionDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [AWGestionDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [AWGestionDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [AWGestionDB] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [AWGestionDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [AWGestionDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [AWGestionDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [AWGestionDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [AWGestionDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [AWGestionDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [AWGestionDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [AWGestionDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [AWGestionDB] SET  ENABLE_BROKER 
GO
ALTER DATABASE [AWGestionDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [AWGestionDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [AWGestionDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [AWGestionDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [AWGestionDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [AWGestionDB] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [AWGestionDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [AWGestionDB] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [AWGestionDB] SET  MULTI_USER 
GO
ALTER DATABASE [AWGestionDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [AWGestionDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [AWGestionDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [AWGestionDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [AWGestionDB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [AWGestionDB] SET QUERY_STORE = OFF
GO
USE [AWGestionDB]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 16/05/2020 06:46:10 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetRoleClaims]    Script Date: 16/05/2020 06:46:10 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoleClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 16/05/2020 06:46:10 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoles](
	[Id] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](256) NULL,
	[NormalizedName] [nvarchar](256) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 16/05/2020 06:46:10 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](450) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 16/05/2020 06:46:10 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserLogins](
	[LoginProvider] [nvarchar](128) NOT NULL,
	[ProviderKey] [nvarchar](128) NOT NULL,
	[ProviderDisplayName] [nvarchar](max) NULL,
	[UserId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 16/05/2020 06:46:10 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserRoles](
	[UserId] [nvarchar](450) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 16/05/2020 06:46:10 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUsers](
	[Id] [nvarchar](450) NOT NULL,
	[UserName] [nvarchar](256) NULL,
	[NormalizedUserName] [nvarchar](256) NULL,
	[Email] [nvarchar](256) NULL,
	[NormalizedEmail] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEnd] [datetimeoffset](7) NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
	[Nombres] [nvarchar](100) NULL,
	[ApPat] [nvarchar](100) NULL,
	[ApMat] [nvarchar](100) NULL,
	[RFC] [nvarchar](15) NULL,
 CONSTRAINT [PK_AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserTokens]    Script Date: 16/05/2020 06:46:10 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserTokens](
	[UserId] [nvarchar](450) NOT NULL,
	[LoginProvider] [nvarchar](128) NOT NULL,
	[Name] [nvarchar](128) NOT NULL,
	[Value] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[LoginProvider] ASC,
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Bitacora]    Script Date: 16/05/2020 06:46:10 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Bitacora](
	[BitacoraId] [int] IDENTITY(1,1) NOT NULL,
	[VcTabla] [varchar](25) NOT NULL,
	[VcOperacion] [varchar](25) NOT NULL,
	[DtFecha] [datetime] NOT NULL,
	[VcUserDb] [varchar](20) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[BitacoraId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CatArticulos]    Script Date: 16/05/2020 06:46:10 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CatArticulos](
	[ArtId] [int] IDENTITY(1,1) NOT NULL,
	[ArtNombre] [varchar](30) NOT NULL,
	[MarcaId] [int] NOT NULL,
	[TipoArtId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ArtId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CatEntidades]    Script Date: 16/05/2020 06:46:10 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CatEntidades](
	[EntidadId] [int] NOT NULL,
	[Estado] [varchar](30) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[EntidadId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CatTipoArts]    Script Date: 16/05/2020 06:46:10 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CatTipoArts](
	[TipoArtId] [int] IDENTITY(1,1) NOT NULL,
	[TipoArtDesc] [varchar](30) NULL,
PRIMARY KEY CLUSTERED 
(
	[TipoArtId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Empresas]    Script Date: 16/05/2020 06:46:10 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Empresas](
	[EmpresaRfc] [varchar](12) NOT NULL,
	[EmpresaRazSoc] [varchar](150) NOT NULL,
	[EmpresaCalle] [varchar](150) NOT NULL,
	[EmpresaNumInt] [varchar](10) NULL,
	[EmpresaNumExt] [varchar](10) NOT NULL,
	[EmpresaCp] [varchar](5) NOT NULL,
	[EmpresaCd] [varchar](30) NOT NULL,
	[EmpresaEdo] [varchar](30) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[EmpresaRfc] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Entradas]    Script Date: 16/05/2020 06:46:10 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Entradas](
	[EntradaId] [int] IDENTITY(1,1) NOT NULL,
	[ArtModelo] [varchar](100) NOT NULL,
	[ProyectoId] [int] NOT NULL,
	[Cantidad] [int] NOT NULL,
	[Fecha] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[EntradaId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [UC_Entradas] UNIQUE NONCLUSTERED 
(
	[ArtModelo] ASC,
	[ProyectoId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[InvArticulos]    Script Date: 16/05/2020 06:46:10 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InvArticulos](
	[ArtModelo] [varchar](100) NOT NULL,
	[ArtDesc] [varchar](120) NULL,
	[ArtId] [int] NOT NULL,
	[CantidadNeta] [int] NOT NULL,
	[CantidadPrestada] [int] NOT NULL,
	[CantidadEnAlmacen] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ArtModelo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Marcas]    Script Date: 16/05/2020 06:46:10 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Marcas](
	[MarcaId] [int] IDENTITY(1,1) NOT NULL,
	[VcMarcaName] [nvarchar](max) NULL,
	[VcMarcaStatus] [nvarchar](max) NULL,
 CONSTRAINT [PK_Marcas] PRIMARY KEY CLUSTERED 
(
	[MarcaId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Parametros]    Script Date: 16/05/2020 06:46:10 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Parametros](
	[ParametroId] [int] IDENTITY(1,1) NOT NULL,
	[VcParamName] [varchar](35) NOT NULL,
	[VcParamValue] [varchar](200) NOT NULL,
	[VcParamStatus] [varchar](2) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ParametroId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Proyectos]    Script Date: 16/05/2020 06:46:10 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Proyectos](
	[ProyectoId] [int] IDENTITY(1,1) NOT NULL,
	[ProyectoName] [varchar](100) NOT NULL,
	[EmpresaRfc] [varchar](12) NOT NULL,
	[UserId] [nvarchar](450) NOT NULL,
	[AuthSalida] [varchar](2) NULL,
	[AuthEntrada] [varchar](2) NULL,
PRIMARY KEY CLUSTERED 
(
	[ProyectoId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Salidas]    Script Date: 16/05/2020 06:46:10 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Salidas](
	[SalidaId] [int] IDENTITY(1,1) NOT NULL,
	[ArtModelo] [varchar](100) NOT NULL,
	[ProyectoId] [int] NOT NULL,
	[Cantidad] [int] NOT NULL,
	[Fecha] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[SalidaId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [UC_Salidas] UNIQUE NONCLUSTERED 
(
	[ArtModelo] ASC,
	[ProyectoId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetRoleClaims_RoleId]    Script Date: 16/05/2020 06:46:11 a. m. ******/
CREATE NONCLUSTERED INDEX [IX_AspNetRoleClaims_RoleId] ON [dbo].[AspNetRoleClaims]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [RoleNameIndex]    Script Date: 16/05/2020 06:46:11 a. m. ******/
CREATE UNIQUE NONCLUSTERED INDEX [RoleNameIndex] ON [dbo].[AspNetRoles]
(
	[NormalizedName] ASC
)
WHERE ([NormalizedName] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetUserClaims_UserId]    Script Date: 16/05/2020 06:46:11 a. m. ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUserClaims_UserId] ON [dbo].[AspNetUserClaims]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetUserLogins_UserId]    Script Date: 16/05/2020 06:46:11 a. m. ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUserLogins_UserId] ON [dbo].[AspNetUserLogins]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetUserRoles_RoleId]    Script Date: 16/05/2020 06:46:11 a. m. ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUserRoles_RoleId] ON [dbo].[AspNetUserRoles]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [EmailIndex]    Script Date: 16/05/2020 06:46:11 a. m. ******/
CREATE NONCLUSTERED INDEX [EmailIndex] ON [dbo].[AspNetUsers]
(
	[NormalizedEmail] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UserNameIndex]    Script Date: 16/05/2020 06:46:11 a. m. ******/
CREATE UNIQUE NONCLUSTERED INDEX [UserNameIndex] ON [dbo].[AspNetUsers]
(
	[NormalizedUserName] ASC
)
WHERE ([NormalizedUserName] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[AspNetRoleClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetRoleClaims] CHECK CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserTokens]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserTokens] CHECK CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[CatArticulos]  WITH CHECK ADD FOREIGN KEY([MarcaId])
REFERENCES [dbo].[Marcas] ([MarcaId])
GO
ALTER TABLE [dbo].[CatArticulos]  WITH CHECK ADD FOREIGN KEY([TipoArtId])
REFERENCES [dbo].[CatTipoArts] ([TipoArtId])
GO
ALTER TABLE [dbo].[InvArticulos]  WITH CHECK ADD FOREIGN KEY([ArtId])
REFERENCES [dbo].[CatArticulos] ([ArtId])
GO
ALTER TABLE [dbo].[Proyectos]  WITH CHECK ADD FOREIGN KEY([EmpresaRfc])
REFERENCES [dbo].[Empresas] ([EmpresaRfc])
GO
ALTER TABLE [dbo].[Proyectos]  WITH CHECK ADD FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[Salidas]  WITH CHECK ADD FOREIGN KEY([ArtModelo])
REFERENCES [dbo].[InvArticulos] ([ArtModelo])
GO
ALTER TABLE [dbo].[Salidas]  WITH CHECK ADD FOREIGN KEY([ProyectoId])
REFERENCES [dbo].[Proyectos] ([ProyectoId])
GO
/****** Object:  StoredProcedure [dbo].[ActualizaEntradaStock]    Script Date: 16/05/2020 06:46:11 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[ActualizaEntradaStock] @ArtModelo varchar(100), @Cantidad int
AS
BEGIN
    SET NOCOUNT ON;
	DECLARE @OldCantidadEnAlmacen int, @OldCantidadPrestada int;
	SET @OldCantidadEnAlmacen = (SELECT CantidadEnAlmacen FROM InvArticulos where ArtModelo = @ArtModelo);
	SET @OldCantidadPrestada = (SELECT CantidadPrestada FROM InvArticulos where ArtModelo = @ArtModelo);
    update InvArticulos set CantidadEnAlmacen = (@OldCantidadEnAlmacen + @Cantidad), CantidadPrestada = (@OldCantidadPrestada - @Cantidad) where ArtModelo = @ArtModelo;
END
GO
/****** Object:  StoredProcedure [dbo].[ActualizaMonto]    Script Date: 16/05/2020 06:46:11 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE   PROCEDURE [dbo].[ActualizaMonto] @VentaId int
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @Monto float;
    set @Monto = (select sum(FlUnitAmount)
    from Productos P
        join Inventario I on P.ProductoId = I.ProductoId
        join RelVentaProductos RVP on I.InventarioId = RVP.InventarioId
        join Ventas V on RVP.VentaId = V.VentaId
    where V.VentaId = @VentaId);
    update Ventas set FlAmout = @Monto where VentaId = @VentaId;
END
GO
/****** Object:  StoredProcedure [dbo].[ActualizaSalidaStock]    Script Date: 16/05/2020 06:46:11 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[ActualizaSalidaStock] @ArtModelo varchar(100), @Cantidad int
AS
BEGIN
    SET NOCOUNT ON;
	DECLARE @OldCantidadEnAlmacen int, @OldCantidadPrestada int;
	SET @OldCantidadEnAlmacen = (SELECT CantidadEnAlmacen FROM InvArticulos where ArtModelo = @ArtModelo);
	SET @OldCantidadPrestada = (SELECT CantidadPrestada FROM InvArticulos where ArtModelo = @ArtModelo);
    update InvArticulos set CantidadEnAlmacen = (@OldCantidadEnAlmacen - @Cantidad), CantidadPrestada = (@OldCantidadPrestada + @Cantidad) where ArtModelo = @ArtModelo;
END
GO
/****** Object:  StoredProcedure [dbo].[AltaUsuario]    Script Date: 16/05/2020 06:46:11 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE   PROCEDURE [dbo].[AltaUsuario]
    @rfc varchar(35),
    @nombre varchar(35),
    @apellido varchar(35),
    @pass varchar(35)
AS
BEGIN
    SET NOCOUNT ON;
    insert into Usuarios values (@rfc, @nombre, @apellido, ENCRYPTBYPASSPHRASE('PassDelCifrado', @pass));
END
GO
/****** Object:  StoredProcedure [dbo].[PrepareFactura]    Script Date: 16/05/2020 06:46:11 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE   PROCEDURE [dbo].[PrepareFactura] @VentaId int
AS
BEGIN
      SET NOCOUNT ON;

      --DECLARE THE VARIABLES FOR HOLDING DATA.
      DECLARE @FlAmout float,
          @VcUsrRfc VARCHAR(18),
          @VcUsrNombre VARCHAR(35),
          @DtVenta datetime,
          @VcMarcaName VARCHAR(35),
          @VcProvName VARCHAR(35),
          @VcProdName VARCHAR(35),
          @FlUnitAmount float

      --DECLARE THE CURSOR FOR A QUERY.
      DECLARE Registros CURSOR READ_ONLY
      FOR select V.VentaId,
               FlAmout,
               VcUsrRfc,
               VcUsrNombre,
               V.DtVenta,
               VcMarcaName,
               VcProvName,
               VcProdName,
               FlUnitAmount
        from RelVentaProductos
        join Ventas V on RelVentaProductos.VentaId = V.VentaId
        join Inventario I on RelVentaProductos.InventarioId = I.InventarioId
        join Productos P on I.ProductoId = P.ProductoId
        join Marcas M on P.MarcaId = M.MarcaId
        join Proveedores P2 on P.ProveedorId = P2.ProveedorId
        join Usuarios U on V.UsuarioId = U.UsuarioId
        where V.VentaId = @VentaId;

      --delete from Facturas where CONVERT(VARCHAR(10), DtVenta, 111) between CONVERT(VARCHAR(10), getdate(), 111) and CONVERT(VARCHAR(10), getdate() - 7, 111);
      delete from Facturas where 0=0;

      OPEN Registros
      FETCH NEXT FROM Registros INTO @VentaId,@FlAmout,@VcUsrRfc,@VcUsrNombre,@DtVenta,@VcMarcaName,@VcProvName,@VcProdName,@FlUnitAmount
      WHILE @@FETCH_STATUS = 0
          BEGIN
              insert into Facturas values (@VentaId,@FlAmout,@VcUsrRfc,@VcUsrNombre,@DtVenta,@VcMarcaName,@VcProvName,@VcProdName,@FlUnitAmount);
              FETCH NEXT FROM Registros INTO @VentaId,@FlAmout,@VcUsrRfc,@VcUsrNombre,@DtVenta,@VcMarcaName,@VcProvName,@VcProdName,@FlUnitAmount
          END
      CLOSE Registros
      DEALLOCATE Registros
END
GO
/****** Object:  StoredProcedure [dbo].[PrepareFacturas]    Script Date: 16/05/2020 06:46:11 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE   PROCEDURE [dbo].[PrepareFacturas]
AS
BEGIN
      SET NOCOUNT ON;

      --DECLARE THE VARIABLES FOR HOLDING DATA.
      DECLARE @FlAmout float,
          @VcUsrRfc VARCHAR(18),
          @VcUsrNombre VARCHAR(35),
          @DtVenta datetime,
          @VcMarcaName VARCHAR(35),
          @VcProvName VARCHAR(35),
          @VcProdName VARCHAR(35),
          @FlUnitAmount float,
          @VentaId int

      --DECLARE THE CURSOR FOR A QUERY.
      DECLARE Registros CURSOR READ_ONLY
      FOR select V.VentaId,
               FlAmout,
               VcUsrRfc,
               VcUsrNombre,
               V.DtVenta,
               VcMarcaName,
               VcProvName,
               VcProdName,
               FlUnitAmount
        from RelVentaProductos
        join Ventas V on RelVentaProductos.VentaId = V.VentaId
        join Inventario I on RelVentaProductos.InventarioId = I.InventarioId
        join Productos P on I.ProductoId = P.ProductoId
        join Marcas M on P.MarcaId = M.MarcaId
        join Proveedores P2 on P.ProveedorId = P2.ProveedorId
        join Usuarios U on V.UsuarioId = U.UsuarioId;
        --where V.VentaId not in (select VentaId from Facturas);

      --delete from Facturas where CONVERT(VARCHAR(10), DtVenta, 111) between CONVERT(VARCHAR(10), getdate(), 111) and CONVERT(VARCHAR(10), getdate() - 7, 111);
      delete from Facturas where 0=0;

      OPEN Registros
      FETCH NEXT FROM Registros INTO @VentaId,@FlAmout,@VcUsrRfc,@VcUsrNombre,@DtVenta,@VcMarcaName,@VcProvName,@VcProdName,@FlUnitAmount
      WHILE @@FETCH_STATUS = 0
          BEGIN
              insert into Facturas values (@VentaId,@FlAmout,@VcUsrRfc,@VcUsrNombre,@DtVenta,@VcMarcaName,@VcProvName,@VcProdName,@FlUnitAmount);
              FETCH NEXT FROM Registros INTO @VentaId,@FlAmout,@VcUsrRfc,@VcUsrNombre,@DtVenta,@VcMarcaName,@VcProvName,@VcProdName,@FlUnitAmount
          END
      CLOSE Registros
      DEALLOCATE Registros
END
GO
/****** Object:  StoredProcedure [dbo].[RegistraBitacora]    Script Date: 16/05/2020 06:46:11 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE   PROCEDURE [dbo].[RegistraBitacora]
    @Tabla varchar(20),
    @Operacion varchar(20)
AS
BEGIN
    SET NOCOUNT ON;
    insert into Bitacora values (@Tabla, @Operacion, SYSDATETIME(), current_user);
END
GO
USE [master]
GO
ALTER DATABASE [AWGestionDB] SET  READ_WRITE 
GO
