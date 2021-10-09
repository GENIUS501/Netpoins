﻿/*
Script de implementación para Enlaces_Telecom

Una herramienta generó este código.
Los cambios realizados en este archivo podrían generar un comportamiento incorrecto y se perderán si
se vuelve a generar el código.
*/

GO
SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;

SET NUMERIC_ROUNDABORT OFF;


GO
:setvar DatabaseName "Enlaces_Telecom"
:setvar DefaultFilePrefix "Enlaces_Telecom"
:setvar DefaultDataPath "C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\"
:setvar DefaultLogPath "C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\"

GO
:on error exit
GO
/*
Detectar el modo SQLCMD y deshabilitar la ejecución del script si no se admite el modo SQLCMD.
Para volver a habilitar el script después de habilitar el modo SQLCMD, ejecute lo siguiente:
SET NOEXEC OFF; 
*/
:setvar __IsSqlCmdEnabled "True"
GO
IF N'$(__IsSqlCmdEnabled)' NOT LIKE N'True'
    BEGIN
        PRINT N'El modo SQLCMD debe estar habilitado para ejecutar correctamente este script.';
        SET NOEXEC ON;
    END


GO
USE [master];


GO

IF (DB_ID(N'$(DatabaseName)') IS NOT NULL) 
BEGIN
    ALTER DATABASE [$(DatabaseName)]
    SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE [$(DatabaseName)];
END

GO
PRINT N'Creando la base de datos $(DatabaseName)...'
GO
CREATE DATABASE [$(DatabaseName)]
    ON 
    PRIMARY(NAME = [$(DatabaseName)], FILENAME = N'$(DefaultDataPath)$(DefaultFilePrefix)_Primary.mdf')
    LOG ON (NAME = [$(DatabaseName)_log], FILENAME = N'$(DefaultLogPath)$(DefaultFilePrefix)_Primary.ldf') COLLATE SQL_Latin1_General_CP1_CI_AS
GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET AUTO_CLOSE OFF 
            WITH ROLLBACK IMMEDIATE;
    END


GO
USE [$(DatabaseName)];


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET ANSI_NULLS ON,
                ANSI_PADDING ON,
                ANSI_WARNINGS ON,
                ARITHABORT ON,
                CONCAT_NULL_YIELDS_NULL ON,
                NUMERIC_ROUNDABORT OFF,
                QUOTED_IDENTIFIER ON,
                ANSI_NULL_DEFAULT ON,
                CURSOR_DEFAULT LOCAL,
                RECOVERY FULL,
                CURSOR_CLOSE_ON_COMMIT OFF,
                AUTO_CREATE_STATISTICS ON,
                AUTO_SHRINK OFF,
                AUTO_UPDATE_STATISTICS ON,
                RECURSIVE_TRIGGERS OFF 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET ALLOW_SNAPSHOT_ISOLATION OFF;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET READ_COMMITTED_SNAPSHOT OFF 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET AUTO_UPDATE_STATISTICS_ASYNC OFF,
                PAGE_VERIFY NONE,
                DATE_CORRELATION_OPTIMIZATION OFF,
                DISABLE_BROKER,
                PARAMETERIZATION SIMPLE,
                SUPPLEMENTAL_LOGGING OFF 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF IS_SRVROLEMEMBER(N'sysadmin') = 1
    BEGIN
        IF EXISTS (SELECT 1
                   FROM   [master].[dbo].[sysdatabases]
                   WHERE  [name] = N'$(DatabaseName)')
            BEGIN
                EXECUTE sp_executesql N'ALTER DATABASE [$(DatabaseName)]
    SET TRUSTWORTHY OFF,
        DB_CHAINING OFF 
    WITH ROLLBACK IMMEDIATE';
            END
    END
ELSE
    BEGIN
        PRINT N'No se puede modificar la configuración de la base de datos. Debe ser un administrador del sistema para poder aplicar esta configuración.';
    END


GO
IF IS_SRVROLEMEMBER(N'sysadmin') = 1
    BEGIN
        IF EXISTS (SELECT 1
                   FROM   [master].[dbo].[sysdatabases]
                   WHERE  [name] = N'$(DatabaseName)')
            BEGIN
                EXECUTE sp_executesql N'ALTER DATABASE [$(DatabaseName)]
    SET HONOR_BROKER_PRIORITY OFF 
    WITH ROLLBACK IMMEDIATE';
            END
    END
ELSE
    BEGIN
        PRINT N'No se puede modificar la configuración de la base de datos. Debe ser un administrador del sistema para poder aplicar esta configuración.';
    END


GO
ALTER DATABASE [$(DatabaseName)]
    SET TARGET_RECOVERY_TIME = 0 SECONDS 
    WITH ROLLBACK IMMEDIATE;


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET FILESTREAM(NON_TRANSACTED_ACCESS = OFF),
                CONTAINMENT = NONE 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET AUTO_CREATE_STATISTICS ON(INCREMENTAL = OFF),
                MEMORY_OPTIMIZED_ELEVATE_TO_SNAPSHOT = OFF,
                DELAYED_DURABILITY = DISABLED 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET QUERY_STORE (QUERY_CAPTURE_MODE = ALL, DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_PLANS_PER_QUERY = 200, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 367), MAX_STORAGE_SIZE_MB = 100) 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET QUERY_STORE = OFF 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
        ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET MAXDOP = PRIMARY;
        ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
        ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET LEGACY_CARDINALITY_ESTIMATION = PRIMARY;
        ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
        ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET PARAMETER_SNIFFING = PRIMARY;
        ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
        ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET QUERY_OPTIMIZER_HOTFIXES = PRIMARY;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET TEMPORAL_HISTORY_RETENTION ON 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF fulltextserviceproperty(N'IsFulltextInstalled') = 1
    EXECUTE sp_fulltext_database 'enable';


GO
PRINT N'Creando Tabla [dbo].[Oficinas]...';


GO
CREATE TABLE [dbo].[Oficinas] (
    [IdOficina]     INT           IDENTITY (1, 1) NOT NULL,
    [NombreOficina] VARCHAR (200) NOT NULL,
    [EU]            VARCHAR (50)  NOT NULL,
    [Provincia]     VARCHAR (1)   NOT NULL,
    [Estado]        BIT           NOT NULL,
    CONSTRAINT [PK_Oficinas] PRIMARY KEY CLUSTERED ([IdOficina] ASC)
)
WITH (DATA_COMPRESSION = PAGE);


GO
PRINT N'Creando Tabla [dbo].[Proveedores]...';


GO
CREATE TABLE [dbo].[Proveedores] (
    [IdProveedor] INT          IDENTITY (1, 1) NOT NULL,
    [Empresa]     VARCHAR (50) NOT NULL,
    [Contacto]    VARCHAR (50) NOT NULL,
    [Telefono]    VARCHAR (50) NOT NULL,
    [Email]       VARCHAR (50) NOT NULL,
    [Estado]      BIT          NOT NULL,
    CONSTRAINT [PK_Proveedores] PRIMARY KEY CLUSTERED ([IdProveedor] ASC)
)
WITH (DATA_COMPRESSION = PAGE);


GO
PRINT N'Creando Tabla [dbo].[Redes]...';


GO
CREATE TABLE [dbo].[Redes] (
    [IdRed]       INT          IDENTITY (1, 1) NOT NULL,
    [Linea]       VARCHAR (25) NOT NULL,
    [Gateway]     VARCHAR (25) NOT NULL,
    [Interface]   VARCHAR (1)  NOT NULL,
    [TipoEnlace]  VARCHAR (1)  NOT NULL,
    [Bandwidth]   VARCHAR (25) NOT NULL,
    [MedioEnlace] VARCHAR (1)  NOT NULL,
    [Estado]      BIT          NOT NULL,
    CONSTRAINT [PK_Redes] PRIMARY KEY CLUSTERED ([IdRed] ASC)
)
WITH (DATA_COMPRESSION = PAGE);


GO
PRINT N'Creando Tabla [dbo].[RegistroLogin]...';


GO
CREATE TABLE [dbo].[RegistroLogin] (
    [IdRegistroLog]    INT           IDENTITY (1, 1) NOT NULL,
    [IdUsuario]        INT           NOT NULL,
    [FechaHoraIngreso] DATETIME2 (7) NULL,
    [FechaHoraSalida]  DATETIME2 (7) NULL,
    CONSTRAINT [PK_RegistroLogin] PRIMARY KEY CLUSTERED ([IdRegistroLog] ASC)
)
WITH (DATA_COMPRESSION = PAGE);


GO
PRINT N'Creando Tabla [dbo].[RegistroMovimientos]...';


GO
CREATE TABLE [dbo].[RegistroMovimientos] (
    [IdRegistroMov] INT           IDENTITY (1, 1) NOT NULL,
    [IdUsuario]     INT           NOT NULL,
    [FechaHoraMov]  DATETIME2 (7) NULL,
    [TipoMovi]      VARCHAR (100) NOT NULL,
    [DetalleMov]    VARCHAR (500) NOT NULL,
    CONSTRAINT [PK_RegistroMovimientos] PRIMARY KEY CLUSTERED ([IdRegistroMov] ASC)
)
WITH (DATA_COMPRESSION = PAGE);


GO
PRINT N'Creando Tabla [dbo].[Roles]...';


GO
CREATE TABLE [dbo].[Roles] (
    [IdRol]       INT           IDENTITY (1, 1) NOT NULL,
    [Rol]         VARCHAR (50)  NOT NULL,
    [Descripcion] VARCHAR (200) NOT NULL,
    [Estado]      BIT           NOT NULL,
    CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED ([IdRol] ASC)
)
WITH (DATA_COMPRESSION = PAGE);


GO
PRINT N'Creando Tabla [dbo].[Usuarios]...';


GO
CREATE TABLE [dbo].[Usuarios] (
    [IdUsuario]      INT          IDENTITY (1, 1) NOT NULL,
    [IdRol]          INT          NOT NULL,
    [Identificacion] VARCHAR (9)  NOT NULL,
    [Nombre]         VARCHAR (50) NOT NULL,
    [Direccion]      VARCHAR (50) NOT NULL,
    [Telefono]       VARCHAR (50) NOT NULL,
    [Correo]         VARCHAR (50) NOT NULL,
    [Usuario]        VARCHAR (50) NOT NULL,
    [Contrasena]     VARCHAR (50) NOT NULL,
    [Estado]         BIT          NOT NULL,
    CONSTRAINT [PK_Usuario] PRIMARY KEY CLUSTERED ([IdUsuario] ASC)
)
WITH (DATA_COMPRESSION = PAGE);


GO
PRINT N'Creando Restricción DEFAULT [dbo].[DF_Oficinas_Estado]...';


GO
ALTER TABLE [dbo].[Oficinas]
    ADD CONSTRAINT [DF_Oficinas_Estado] DEFAULT (0) FOR [Estado];


GO
PRINT N'Creando Restricción DEFAULT [dbo].[DF_Proveedores_Estado]...';


GO
ALTER TABLE [dbo].[Proveedores]
    ADD CONSTRAINT [DF_Proveedores_Estado] DEFAULT (0) FOR [Estado];


GO
PRINT N'Creando Restricción DEFAULT [dbo].[DF_Actividades_Estado]...';


GO
ALTER TABLE [dbo].[Redes]
    ADD CONSTRAINT [DF_Actividades_Estado] DEFAULT (0) FOR [Estado];


GO
PRINT N'Creando Restricción DEFAULT [dbo].[DF_Roles_Estado]...';


GO
ALTER TABLE [dbo].[Roles]
    ADD CONSTRAINT [DF_Roles_Estado] DEFAULT (0) FOR [Estado];


GO
PRINT N'Creando Restricción DEFAULT [dbo].[DF_Usuarios_Estado]...';


GO
ALTER TABLE [dbo].[Usuarios]
    ADD CONSTRAINT [DF_Usuarios_Estado] DEFAULT (0) FOR [Estado];


GO
PRINT N'Creando Clave externa [dbo].[FK_RegistroLogin_Usuarios]...';


GO
ALTER TABLE [dbo].[RegistroLogin]
    ADD CONSTRAINT [FK_RegistroLogin_Usuarios] FOREIGN KEY ([IdUsuario]) REFERENCES [dbo].[Usuarios] ([IdUsuario]);


GO
PRINT N'Creando Clave externa [dbo].[FK_RegistroMovimientos_Usuarios]...';


GO
ALTER TABLE [dbo].[RegistroMovimientos]
    ADD CONSTRAINT [FK_RegistroMovimientos_Usuarios] FOREIGN KEY ([IdUsuario]) REFERENCES [dbo].[Usuarios] ([IdUsuario]);


GO
PRINT N'Creando Clave externa [dbo].[FK_Usuarios_Roles]...';


GO
ALTER TABLE [dbo].[Usuarios]
    ADD CONSTRAINT [FK_Usuarios_Roles] FOREIGN KEY ([IdRol]) REFERENCES [dbo].[Roles] ([IdRol]);


GO
PRINT N'Creando Restricción CHECK [dbo].[CHK_Oficina_Provincia]...';


GO
ALTER TABLE [dbo].[Oficinas]
    ADD CONSTRAINT [CHK_Oficina_Provincia] CHECK (Provincia='S' OR Provincia='H' OR Provincia='A' OR Provincia='C' OR Provincia='L' OR Provincia='G' OR Provincia='P');


GO
PRINT N'Creando Restricción CHECK [dbo].[CHK_Redes_Interface]...';


GO
ALTER TABLE [dbo].[Redes]
    ADD CONSTRAINT [CHK_Redes_Interface] CHECK (Interface='I' OR Interface='G');


GO
PRINT N'Creando Restricción CHECK [dbo].[CHK_Redes_TipoEnlace]...';


GO
ALTER TABLE [dbo].[Redes]
    ADD CONSTRAINT [CHK_Redes_TipoEnlace] CHECK (TipoEnlace='P' OR TipoEnlace='R');


GO
PRINT N'Creando Restricción CHECK [dbo].[CHK_Redes_MedioEnlace]...';


GO
ALTER TABLE [dbo].[Redes]
    ADD CONSTRAINT [CHK_Redes_MedioEnlace] CHECK (MedioEnlace='F' OR MedioEnlace='C' OR MedioEnlace='I' OR MedioEnlace='W');


GO
DECLARE @VarDecimalSupported AS BIT;

SELECT @VarDecimalSupported = 0;

IF ((ServerProperty(N'EngineEdition') = 3)
    AND (((@@microsoftversion / power(2, 24) = 9)
          AND (@@microsoftversion & 0xffff >= 3024))
         OR ((@@microsoftversion / power(2, 24) = 10)
             AND (@@microsoftversion & 0xffff >= 1600))))
    SELECT @VarDecimalSupported = 1;

IF (@VarDecimalSupported > 0)
    BEGIN
        EXECUTE sp_db_vardecimal_storage_format N'$(DatabaseName)', 'ON';
    END


GO
PRINT N'Actualización completada.';


GO
