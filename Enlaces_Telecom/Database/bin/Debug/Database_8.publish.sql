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
USE [$(DatabaseName)];


GO
PRINT N'Quitando Restricción CHECK [dbo].[CHK_Oficina_Provincia]...';


GO
ALTER TABLE [dbo].[Oficinas] DROP CONSTRAINT [CHK_Oficina_Provincia];


GO
PRINT N'Quitando Restricción CHECK [dbo].[CHK_Redes_Interface]...';


GO
ALTER TABLE [dbo].[Redes] DROP CONSTRAINT [CHK_Redes_Interface];


GO
PRINT N'Quitando Restricción CHECK [dbo].[CHK_Redes_MedioEnlace]...';


GO
ALTER TABLE [dbo].[Redes] DROP CONSTRAINT [CHK_Redes_MedioEnlace];


GO
PRINT N'Quitando Restricción CHECK [dbo].[CHK_Redes_TipoEnlace]...';


GO
ALTER TABLE [dbo].[Redes] DROP CONSTRAINT [CHK_Redes_TipoEnlace];


GO
PRINT N'Modificando Tabla [dbo].[Oficinas]...';


GO
ALTER TABLE [dbo].[Oficinas] ALTER COLUMN [Provincia] VARCHAR (100) NOT NULL;


GO
PRINT N'Modificando Tabla [dbo].[Redes]...';


GO
ALTER TABLE [dbo].[Redes] ALTER COLUMN [Interface] VARCHAR (100) NOT NULL;

ALTER TABLE [dbo].[Redes] ALTER COLUMN [MedioEnlace] VARCHAR (100) NOT NULL;

ALTER TABLE [dbo].[Redes] ALTER COLUMN [TipoEnlace] VARCHAR (100) NOT NULL;


GO
PRINT N'Modificando Procedimiento [dbo].[OficinasActualizar]...';


GO
ALTER PROCEDURE [dbo].[OficinasActualizar]
    @IdOficina INT,
    @IdRed INT,
	@IdProveedor INT,
	@NombreOficina VARCHAR(200),
	@UE VARCHAR(10),
	@Provincia VARCHAR(100),
	@Estado BIT

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
    Provincia=@Provincia,
	Estado=@Estado

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
	@Provincia VARCHAR(100),
	@Estado BIT

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
	, Estado 
		)
		VALUES
		(
      @IdRed
	, @IdProveedor
	, @NombreOficina
	, @UE 
	, @Provincia
	, @Estado 
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
PRINT N'Modificando Procedimiento [dbo].[RedesActualizar]...';


GO
ALTER PROCEDURE [dbo].[RedesActualizar]
    @IdRed INT,
	@Linea VARCHAR(25),
	@Gateway VARCHAR(25),
	@Interface VARCHAR(100),
    @TipoEnlace VARCHAR(100),
    @Bandwidth VARCHAR(25),
    @MedioEnlace VARCHAR(100)

AS BEGIN
SET NOCOUNT ON

	BEGIN TRANSACTION TRASA

	BEGIN TRY
	-- AQUI VA EL CODIGO
		
		UPDATE dbo.Redes SET
		Linea=@Linea,
		Gateway=@Gateway,
		Interface=@Interface,
		TipoEnlace=@TipoEnlace,
		Bandwidth=@Bandwidth,
		MedioEnlace=@MedioEnlace

		WHERE IdRed=@IdRed
				
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
PRINT N'Modificando Procedimiento [dbo].[RedesInsertar]...';


GO
ALTER PROCEDURE [dbo].[RedesInsertar]
	@Linea VARCHAR(25),
	@Gateway VARCHAR(25),
	@Interface VARCHAR(100),
    @TipoEnlace VARCHAR(100),
    @Bandwidth VARCHAR(25),
    @MedioEnlace VARCHAR(100)

AS BEGIN
SET NOCOUNT ON

	BEGIN TRANSACTION TRANSA

	BEGIN TRY
	-- AQUI VA EL CODIGO
		
		INSERT INTO dbo.Redes
		(
		    Linea
        ,   Gateway
		,   Interface
		,   TipoEnlace
		,	Bandwidth
		,	MedioEnlace
		)
		VALUES
		(
		    @Linea
        ,   @Gateway
		,   @Interface
		,   @TipoEnlace
		,	@Bandwidth
		,	@MedioEnlace
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
PRINT N'Actualizando Procedimiento [dbo].[OficinasEliminar]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[OficinasEliminar]';


GO
PRINT N'Actualizando Procedimiento [dbo].[OficinasLista]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[OficinasLista]';


GO
PRINT N'Actualizando Procedimiento [dbo].[OficinasObtener]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[OficinasObtener]';


GO
PRINT N'Actualizando Procedimiento [dbo].[RedesEliminar]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[RedesEliminar]';


GO
PRINT N'Actualizando Procedimiento [dbo].[RedesLista]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[RedesLista]';


GO
PRINT N'Actualizando Procedimiento [dbo].[RedesObtener]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[RedesObtener]';


GO
PRINT N'Actualización completada.';


GO
