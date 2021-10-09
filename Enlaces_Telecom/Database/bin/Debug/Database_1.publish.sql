/*
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
USE [$(DatabaseName)];


GO
/*
Se está quitando la columna [dbo].[Usuarios].[Correo]; puede que se pierdan datos.

Se está quitando la columna [dbo].[Usuarios].[Direccion]; puede que se pierdan datos.

Debe agregarse la columna [dbo].[Usuarios].[Email] de la tabla [dbo].[Usuarios], pero esta columna no tiene un valor predeterminado y no admite valores NULL. Si la tabla contiene datos, el script ALTER no funcionará. Para evitar esta incidencia, agregue un valor predeterminado a la columna, márquela de modo que permita valores NULL o habilite la generación de valores predeterminados inteligentes como opción de implementación.

El tipo de la columna Telefono en la tabla [dbo].[Usuarios] es  VARCHAR (50) NOT NULL, pero se va a cambiar a  VARCHAR (10) NOT NULL. Si la columna contiene datos no compatibles con el tipo  VARCHAR (10) NOT NULL, podrían producirse pérdidas de datos y errores en la implementación.
*/

IF EXISTS (select top 1 1 from [dbo].[Usuarios])
    RAISERROR (N'Se detectaron filas. La actualización del esquema va a terminar debido a una posible pérdida de datos.', 16, 127) WITH NOWAIT

GO
PRINT N'Quitando Restricción DEFAULT [dbo].[DF_Usuarios_Estado]...';


GO
ALTER TABLE [dbo].[Usuarios] DROP CONSTRAINT [DF_Usuarios_Estado];


GO
PRINT N'Quitando Clave externa [dbo].[FK_RegistroLogin_Usuarios]...';


GO
ALTER TABLE [dbo].[RegistroLogin] DROP CONSTRAINT [FK_RegistroLogin_Usuarios];


GO
PRINT N'Quitando Clave externa [dbo].[FK_RegistroMovimientos_Usuarios]...';


GO
ALTER TABLE [dbo].[RegistroMovimientos] DROP CONSTRAINT [FK_RegistroMovimientos_Usuarios];


GO
PRINT N'Quitando Clave externa [dbo].[FK_Usuarios_Roles]...';


GO
ALTER TABLE [dbo].[Usuarios] DROP CONSTRAINT [FK_Usuarios_Roles];


GO
PRINT N'Iniciando recompilación de la tabla [dbo].[Usuarios]...';


GO
BEGIN TRANSACTION;

SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;

SET XACT_ABORT ON;

CREATE TABLE [dbo].[tmp_ms_xx_Usuarios] (
    [IdUsuario]      INT          IDENTITY (1, 1) NOT NULL,
    [IdRol]          INT          NOT NULL,
    [Identificacion] VARCHAR (9)  NOT NULL,
    [Nombre]         VARCHAR (50) NOT NULL,
    [Telefono]       VARCHAR (10) NOT NULL,
    [Email]          VARCHAR (50) NOT NULL,
    [Usuario]        VARCHAR (50) NOT NULL,
    [Contrasena]     VARCHAR (50) NOT NULL,
    [Estado]         BIT          CONSTRAINT [DF_Usuarios_Estado] DEFAULT (0) NOT NULL,
    CONSTRAINT [tmp_ms_xx_constraint_PK_Usuario1] PRIMARY KEY CLUSTERED ([IdUsuario] ASC)
)
WITH (DATA_COMPRESSION = PAGE);

IF EXISTS (SELECT TOP 1 1 
           FROM   [dbo].[Usuarios])
    BEGIN
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_Usuarios] ON;
        INSERT INTO [dbo].[tmp_ms_xx_Usuarios] ([IdUsuario], [IdRol], [Identificacion], [Nombre], [Telefono], [Usuario], [Contrasena], [Estado])
        SELECT   [IdUsuario],
                 [IdRol],
                 [Identificacion],
                 [Nombre],
                 [Telefono],
                 [Usuario],
                 [Contrasena],
                 [Estado]
        FROM     [dbo].[Usuarios]
        ORDER BY [IdUsuario] ASC;
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_Usuarios] OFF;
    END

DROP TABLE [dbo].[Usuarios];

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_Usuarios]', N'Usuarios';

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_constraint_PK_Usuario1]', N'PK_Usuario', N'OBJECT';

COMMIT TRANSACTION;

SET TRANSACTION ISOLATION LEVEL READ COMMITTED;


GO
PRINT N'Creando Clave externa [dbo].[FK_RegistroLogin_Usuarios]...';


GO
ALTER TABLE [dbo].[RegistroLogin] WITH NOCHECK
    ADD CONSTRAINT [FK_RegistroLogin_Usuarios] FOREIGN KEY ([IdUsuario]) REFERENCES [dbo].[Usuarios] ([IdUsuario]);


GO
PRINT N'Creando Clave externa [dbo].[FK_RegistroMovimientos_Usuarios]...';


GO
ALTER TABLE [dbo].[RegistroMovimientos] WITH NOCHECK
    ADD CONSTRAINT [FK_RegistroMovimientos_Usuarios] FOREIGN KEY ([IdUsuario]) REFERENCES [dbo].[Usuarios] ([IdUsuario]);


GO
PRINT N'Creando Clave externa [dbo].[FK_Usuarios_Roles]...';


GO
ALTER TABLE [dbo].[Usuarios] WITH NOCHECK
    ADD CONSTRAINT [FK_Usuarios_Roles] FOREIGN KEY ([IdRol]) REFERENCES [dbo].[Roles] ([IdRol]);


GO
PRINT N'Creando Procedimiento [dbo].[RolesActualizar]...';


GO
CREATE PROCEDURE [dbo].RolesActualizar
    @IdRol INT,
    @Rol VARCHAR(50),
	@Descripcion VARCHAR(200),
	@Estado BIT
AS BEGIN
SET NOCOUNT ON

	BEGIN TRANSACTION TRASA

	BEGIN TRY
	-- AQUI VA EL CODIGO
		
		UPDATE dbo.Roles SET
		Rol=@Rol,
		Descripcion=@Descripcion,
		Estado=@Estado

		WHERE IdRol=@IdRol
				
		COMMIT TRANSACTION TRASA
		
		SELECT 0 AS CodeError, '' AS MsgError

	END TRY

	BEGIN CATCH
		SELECT 
				ERROR_NUMBER() AS CodeError
			,	ERROR_MESSAGE() AS MsgError

			ROLLBACK TRANSACTION TRASA
	END CATCH
END
GO
PRINT N'Creando Procedimiento [dbo].[RolesEliminar]...';


GO
CREATE PROCEDURE [dbo].RolesEliminar
	@IdRol INT

AS BEGIN
SET NOCOUNT ON

	BEGIN TRANSACTION TRANSA

	BEGIN TRY
	-- AQUI VA EL CODIGO
		
		DELETE FROM dbo.Roles WHERE IdRol=@IdRol

		
		COMMIT TRANSACTION TRANSA
		
		SELECT 0 AS CodeError, '' AS MsgError

	END TRY

	BEGIN CATCH
		SELECT 
				ERROR_NUMBER() AS CodeError
			,	ERROR_MESSAGE() AS MsgError

			ROLLBACK TRANSACTION TRANSA
	END CATCH

END
GO
PRINT N'Creando Procedimiento [dbo].[RolesInsertar]...';


GO
CREATE PROCEDURE [dbo].RolesInsertar
    @Rol VARCHAR(50), 
	@Descripcion VARCHAR(200),
	@Estado BIT


AS BEGIN
SET NOCOUNT ON

	BEGIN TRANSACTION TRANSA

	BEGIN TRY
	-- AQUI VA EL CODIGO
		
		INSERT INTO dbo.Roles
		(
		    Rol
        ,   Descripcion
		,	Estado
		)
		VALUES
		(
		    @Rol
		,	@Descripcion
		,	@Estado
		)


		COMMIT TRANSACTION TRANSA
		
		SELECT 0 AS CodeError, '' AS MsgError

	END TRY

	BEGIN CATCH
		SELECT 
				ERROR_NUMBER() AS CodeError
			,	ERROR_MESSAGE() AS MsgError

			ROLLBACK TRANSACTION TRANSA
	END CATCH

END
GO
PRINT N'Creando Procedimiento [dbo].[RolesLista]...';


GO
CREATE PROCEDURE [dbo].RolesLista
AS BEGIN
	SET NOCOUNT ON

	SELECT
			IdRol
		,	Rol
		,   Descripcion

	FROM dbo.Roles
	WHERE
		Estado=1

	ORDER BY Rol

END
GO
PRINT N'Creando Procedimiento [dbo].[RolesObtener]...';


GO
CREATE PROCEDURE [dbo].RolesObtener
@IdRol INT =NULL
AS BEGIN
	SET NOCOUNT ON

	SELECT
			IdRol
        ,   Rol
		,	Descripcion
		,	Estado

	FROM dbo.Roles
	WHERE
		(@IdRol IS NULL OR IdRol=@IdRol)

END
GO
PRINT N'Creando Procedimiento [dbo].[UsuariosActualizar]...';


GO
CREATE PROCEDURE [dbo].[UsuariosActualizar]
    @IdUsuario INT,
	@IdRol INT,
	@Identificacion varchar(9),
	@Nombre VARCHAR(50),
	@Telefono VARCHAR(10),
	@Email  VARCHAR(50),
	@Usuario VARCHAR(50),
	@Contrasena VARCHAR(50),
	@Estado BIT


	AS BEGIN

SET NOCOUNT ON

	BEGIN TRANSACTION TRANSA

	BEGIN TRY
	-- AQUI VA EL CODIGO
		
	UPDATE DBO.Usuarios SET

	IdRol=@IdRol,
	Identificacion=@Identificacion,
    Nombre=@Nombre,
	Telefono=@Telefono,
	Email=@Email,
	Usuario=@Usuario,
	Contrasena=@Contrasena,
	Estado=@Estado	

	WHERE IdUsuario=@IdUsuario


		COMMIT TRANSACTION TRANSA
		
		SELECT 0 AS CodeError, '' AS MsgError

	END TRY

	BEGIN CATCH
		SELECT 
				ERROR_NUMBER() AS CodeError
			,	ERROR_MESSAGE() AS MsgError

			ROLLBACK TRANSACTION TRANSA
	END CATCH
END
GO
PRINT N'Creando Procedimiento [dbo].[UsuariosEliminar]...';


GO
CREATE PROCEDURE [dbo].UsuariosEliminar
	@IdUsuario INT

AS BEGIN
SET NOCOUNT ON

	BEGIN TRANSACTION TRANSA

	BEGIN TRY
	-- AQUI VA EL CODIGO
		
		DELETE FROM dbo.Usuarios WHERE IdUsuario=@IdUsuario

		
		COMMIT TRANSACTION TRANSA
		
		SELECT 0 AS CodeError, '' AS MsgError

	END TRY

	BEGIN CATCH
		SELECT 
				ERROR_NUMBER() AS CodeError
			,	ERROR_MESSAGE() AS MsgError

			ROLLBACK TRANSACTION TRANSA
	END CATCH

END
GO
PRINT N'Creando Procedimiento [dbo].[UsuariosInsertar]...';


GO
CREATE PROCEDURE [dbo].[UsuariosInsertar]
	@IdRol INT,		
	@Identificacion VARCHAR(9),
	@Nombre VARCHAR(50),
	@Telefono VARCHAR(10),
	@Email  VARCHAR(50),
	@Usuario VARCHAR(50),
	@Contrasena VARCHAR(50),
	@Estado BIT

AS BEGIN
SET NOCOUNT ON

	BEGIN TRANSACTION TRANSA

	BEGIN TRY
	-- AQUI VA EL CODIGO
		
		INSERT INTO dbo.Usuarios
		(
		    IdRol
        ,   Identificacion
		,   Nombre
		,   Telefono
		,	Email
		,   Usuario
		,   Contrasena
		,	Estado
		)
		VALUES
		(
		    @IdRol
        ,   @Identificacion
		,   @Nombre
		,   @Telefono
		,	@Email
		,   @Usuario
		,   @Contrasena
		,	@Estado
		)


		COMMIT TRANSACTION TRANSA
		
		SELECT 0 AS CodeError, '' AS MsgError

	END TRY

	BEGIN CATCH
		SELECT 
				ERROR_NUMBER() AS CodeError
			,	ERROR_MESSAGE() AS MsgError

			ROLLBACK TRANSACTION TRANSA
	END CATCH

END
GO
PRINT N'Creando Procedimiento [dbo].[UsuariosLista]...';


GO
CREATE PROCEDURE [dbo].UsuariosLista
AS BEGIN
	SET NOCOUNT ON

	SELECT
			IdUsuario
		,	Nombre
		,   Telefono
		,   Email
		,   Usuario
		,   Estado

	FROM dbo.Usuarios
	WHERE
		Estado=1
	ORDER BY Nombre

END
GO
PRINT N'Creando Procedimiento [dbo].[UsuariosObtener]...';


GO
CREATE PROCEDURE [dbo].UsuariosObtener
@IdUsuario INT =NULL
AS BEGIN
	SET NOCOUNT ON

	SELECT
			U.IdUsuario
        ,   U.Identificacion
		,   U.Nombre
		,   U.Telefono
		,	U.Email
		,   U.Usuario
		,	U.Estado

		,   R.IdRol
		,   R.Rol

	FROM dbo.Usuarios U
	  	LEFT JOIN dbo.Roles R
	   ON U.IdRol=R.IdRol
	WHERE
		(@IdUsuario IS NULL OR IdUsuario=@IdUsuario)

END
GO
PRINT N'Comprobando los datos existentes con las restricciones recién creadas';


GO
USE [$(DatabaseName)];


GO
ALTER TABLE [dbo].[RegistroLogin] WITH CHECK CHECK CONSTRAINT [FK_RegistroLogin_Usuarios];

ALTER TABLE [dbo].[RegistroMovimientos] WITH CHECK CHECK CONSTRAINT [FK_RegistroMovimientos_Usuarios];

ALTER TABLE [dbo].[Usuarios] WITH CHECK CHECK CONSTRAINT [FK_Usuarios_Roles];


GO
PRINT N'Actualización completada.';


GO
