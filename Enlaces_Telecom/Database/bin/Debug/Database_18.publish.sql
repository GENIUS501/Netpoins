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
Se está quitando la columna [dbo].[Enlaces].[Comentario]; puede que se pierdan datos.
*/

IF EXISTS (select top 1 1 from [dbo].[Enlaces])
    RAISERROR (N'Se detectaron filas. La actualización del esquema va a terminar debido a una posible pérdida de datos.', 16, 127) WITH NOWAIT

GO
PRINT N'Modificando Tabla [dbo].[Enlaces]...';


GO
ALTER TABLE [dbo].[Enlaces] DROP COLUMN [Comentario];


GO
PRINT N'Creando Procedimiento [dbo].[EnlacesActualizar]...';


GO
CREATE PROCEDURE [dbo].[EnlacesActualizar]
    @IdEnlace INT,
    @IdOficina INT,
	@IdRed INT,
	@IdProveedor INT

AS BEGIN
SET NOCOUNT ON

	BEGIN TRANSACTION TRASA

	BEGIN TRY
	-- AQUI VA EL CODIGO
		
	UPDATE DBO.Enlaces SET
	IdOficina=@IdOficina,
	IdRed=@IdRed,
	IdProveedor=@IdProveedor

	WHERE IdEnlace=@IdEnlace


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
PRINT N'Creando Procedimiento [dbo].[EnlacesEliminar]...';


GO
CREATE PROCEDURE [dbo].[EnlacesEliminar]
@IdEnlace INT
  
AS BEGIN
SET NOCOUNT ON

	BEGIN TRANSACTION TRASA

	BEGIN TRY
	-- AQUI VA EL CODIGO
		
	DELETE FROM DBO.Enlaces WHERE IdEnlace=@IdEnlace

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
PRINT N'Creando Procedimiento [dbo].[EnlacesInsertar]...';


GO
CREATE PROCEDURE [dbo].[EnlacesInsertar]
    @IdOficina INT,
	@IdRed INT,
	@IdProveedor INT

AS BEGIN
SET NOCOUNT ON

	BEGIN TRANSACTION TRANSA

	BEGIN TRY
	-- AQUI VA EL CODIGO
		
		INSERT INTO dbo.Enlaces 
		(
			IdOficina
		,	IdRed
		,   IdProveedor
		)
		VALUES
		(
		 @IdOficina
	    , @IdRed
	    , @IdProveedor
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
PRINT N'Creando Procedimiento [dbo].[EnlacesObtener]...';


GO
CREATE PROCEDURE [dbo].[EnlacesObtener]
@IdEnlace INT=NULL

AS BEGIN
	SET NOCOUNT ON

	SELECT
			O.IdOficina
		,   O.NombreOficina
		,   O.UE
		,   O.Provincia
		,   O.Comentario

		,   R.IdRed
		,	R.Linea
		,   R.Gateway
		,	R.Interface
		,   R.TipoEnlace
		,   R.Bandwidth
		,   R.MedioEnlace
		,   R.Comentario

		,   P.IdProveedor
		,   P.NombreEmpresa
		,   P.Contacto
		,   P.Telefono
		,   P.Email
		,   p.Comentario
		

	FROM dbo.Enlaces E
	 LEFT JOIN dbo.Oficinas O
         ON E.IdOficina = O.IdOficina
	 LEFT JOIN dbo.Redes R
         ON E.IdRed = R.IdRed
    LEFT JOIN dbo.Proveedores P
         ON E.IdProveedor = P.IdProveedor
	WHERE
	     (@IdEnlace IS NULL OR IdEnlace=@IdEnlace)

END
GO
PRINT N'Actualización completada.';


GO
