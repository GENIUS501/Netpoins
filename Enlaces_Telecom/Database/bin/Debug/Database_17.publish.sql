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
PRINT N'Modificando Procedimiento [dbo].[RedesInsertar]...';


GO
ALTER PROCEDURE [dbo].[RedesInsertar]
	@Linea VARCHAR(25),
	@Gateway VARCHAR(25),
	@Interface VARCHAR(100),
    @TipoEnlace VARCHAR(100),
    @Bandwidth VARCHAR(25),
    @MedioEnlace VARCHAR(100),
	@Comentario VARCHAR(500)

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
		,   Comentario
		)
		VALUES
		(
		    @Linea
        ,   @Gateway
		,   @Interface
		,   @TipoEnlace
		,	@Bandwidth
		,	@MedioEnlace
		,   @Comentario
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
PRINT N'Actualización completada.';


GO
