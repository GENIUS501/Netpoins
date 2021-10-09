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
Se está quitando la columna [dbo].[Oficinas].[Estado]; puede que se pierdan datos.
*/

IF EXISTS (select top 1 1 from [dbo].[Oficinas])
    RAISERROR (N'Se detectaron filas. La actualización del esquema va a terminar debido a una posible pérdida de datos.', 16, 127) WITH NOWAIT

GO
/*
Se está quitando la columna [dbo].[Proveedores].[Estado]; puede que se pierdan datos.
*/

IF EXISTS (select top 1 1 from [dbo].[Proveedores])
    RAISERROR (N'Se detectaron filas. La actualización del esquema va a terminar debido a una posible pérdida de datos.', 16, 127) WITH NOWAIT

GO
PRINT N'Quitando Restricción DEFAULT [dbo].[DF_Oficinas_Estado]...';


GO
ALTER TABLE [dbo].[Oficinas] DROP CONSTRAINT [DF_Oficinas_Estado];


GO
PRINT N'Quitando Restricción DEFAULT [dbo].[DF_Proveedores_Estado]...';


GO
ALTER TABLE [dbo].[Proveedores] DROP CONSTRAINT [DF_Proveedores_Estado];


GO
PRINT N'Modificando Tabla [dbo].[Oficinas]...';


GO
ALTER TABLE [dbo].[Oficinas] DROP COLUMN [Estado];


GO
PRINT N'Modificando Tabla [dbo].[Proveedores]...';


GO
ALTER TABLE [dbo].[Proveedores] DROP COLUMN [Estado];


GO
PRINT N'Modificando Procedimiento [dbo].[OficinasActualizar]...';


GO
ALTER PROCEDURE [dbo].[OficinasActualizar]
    @IdOficina INT,
    @IdRed INT,
	@IdProveedor INT,
	@NombreOficina VARCHAR(200),
	@UE VARCHAR(10),
	@Provincia VARCHAR(100)

AS BEGIN
SET NOCOUNT ON

	BEGIN TRANSACTION TRANSA

	BEGIN TRY
	-- AQUI VA EL CODIGO
		
	UPDATE DBO.Oficinas SET
	IdRed=@IdRed,
	IdProveedor=@IdProveedor,
	NombreOficina=@NombreOficina,
	UE=@UE,
    Provincia=@Provincia

	WHERE IdOficina=@IdOficina

		COMMIT TRANSACTION TRANSA
		
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
PRINT N'Modificando Procedimiento [dbo].[OficinasInsertar]...';


GO
ALTER PROCEDURE [dbo].[OficinasInsertar]
    @IdRed INT,
	@IdProveedor INT,
	@NombreOficina VARCHAR(200),
	@UE VARCHAR(10),
	@Provincia VARCHAR(100)

AS BEGIN
SET NOCOUNT ON

	BEGIN TRANSACTION TRANSA

	BEGIN TRY
	-- AQUI VA EL CODIGO
		
		INSERT INTO dbo.Oficinas 
		(
      IdRed
	, IdProveedor
	, NombreOficina
	, UE 
	, Provincia 
		)
		VALUES
		(
      @IdRed
	, @IdProveedor
	, @NombreOficina
	, @UE 
	, @Provincia
		)

		COMMIT TRANSACTION TRANSA
		
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
PRINT N'Modificando Procedimiento [dbo].[OficinasLista]...';


GO
ALTER PROCEDURE [dbo].[OficinasLista]
AS BEGIN
	SET NOCOUNT ON

	SELECT
			IdOficina
		,	NombreOficina
		,   UE
		,   Provincia


	FROM dbo.Oficinas

		 order by NombreOficina

END
GO
PRINT N'Modificando Procedimiento [dbo].[ProveedoresActualizar]...';


GO
ALTER PROCEDURE [dbo].[ProveedoresActualizar]
	@IdProveedor INT,
	@NombreEmpresa VARCHAR(50),
	@Contacto  VARCHAR(50),
	@Telefono VARCHAR (10),
	@Email VARCHAR(50)

AS BEGIN
SET NOCOUNT ON

	BEGIN TRANSACTION TRASA

	BEGIN TRY
	-- AQUI VA EL CODIGO
		
		UPDATE dbo.Proveedores SET
		NombreEmpresa=@NombreEmpresa,
		Contacto=@Contacto,
		Telefono=@Telefono,
		Email=@Email

		WHERE IdProveedor=@IdProveedor
				
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
PRINT N'Modificando Procedimiento [dbo].[ProveedoresInsertar]...';


GO
ALTER PROCEDURE [dbo].[ProveedoresInsertar]
	@NombreEmpresa VARCHAR(50),
	@Contacto  VARCHAR(50),
	@Telefono VARCHAR (10),
	@Email VARCHAR(50)

AS BEGIN
SET NOCOUNT ON

	BEGIN TRANSACTION TRANSA

	BEGIN TRY
	-- AQUI VA EL CODIGO
		
		INSERT INTO dbo.Proveedores
		(
		    NombreEmpresa
        ,   Contacto
		,   Telefono
		,   Email
		)
		VALUES
		(
		    @NombreEmpresa
        ,   @Contacto
		,   @Telefono
		,   @Email
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
PRINT N'Modificando Procedimiento [dbo].[ProveedoresLista]...';


GO
ALTER PROCEDURE [dbo].[ProveedoresLista]
AS BEGIN
	SET NOCOUNT ON

	SELECT
			IdProveedor
		,	NombreEmpresa


	FROM dbo.Proveedores

	ORDER BY NombreEmpresa

END
GO
PRINT N'Modificando Procedimiento [dbo].[ProveedoresObtener]...';


GO
ALTER PROCEDURE [dbo].[ProveedoresObtener]
@IdProveedor INT =NULL
AS BEGIN
	SET NOCOUNT ON

	SELECT
			IdProveedor
        ,   NombreEmpresa
		,   Contacto
		,   Telefono
		,   Email

	FROM dbo.Proveedores
	WHERE
		(@IdProveedor IS NULL OR IdProveedor=@IdProveedor)

END
GO
PRINT N'Actualizando Procedimiento [dbo].[OficinasEliminar]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[OficinasEliminar]';


GO
PRINT N'Actualizando Procedimiento [dbo].[OficinasObtener]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[OficinasObtener]';


GO
PRINT N'Actualizando Procedimiento [dbo].[ProveedoresEliminar]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[ProveedoresEliminar]';


GO
PRINT N'Actualización completada.';


GO
