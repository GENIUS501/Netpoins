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
Se está quitando la columna [dbo].[BitacoraCambios].[IdBitCambios]; puede que se pierdan datos.
*/

IF EXISTS (select top 1 1 from [dbo].[BitacoraCambios])
    RAISERROR (N'Se detectaron filas. La actualización del esquema va a terminar debido a una posible pérdida de datos.', 16, 127) WITH NOWAIT

GO
PRINT N'Quitando Clave externa [dbo].[FK_BitacoraCambios_Usuarios]...';


GO
ALTER TABLE [dbo].[BitacoraCambios] DROP CONSTRAINT [FK_BitacoraCambios_Usuarios];


GO
PRINT N'Iniciando recompilación de la tabla [dbo].[BitacoraCambios]...';


GO
BEGIN TRANSACTION;

SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;

SET XACT_ABORT ON;

CREATE TABLE [dbo].[tmp_ms_xx_BitacoraCambios] (
    [IdCambio]  INT           IDENTITY (1, 1) NOT NULL,
    [IdUsuario] INT           NOT NULL,
    [FechaHora] DATETIME2 (7) NULL,
    [Tipo]      VARCHAR (100) NOT NULL,
    [Detalle]   VARCHAR (500) NOT NULL,
    CONSTRAINT [tmp_ms_xx_constraint_PK_BitacoraCambios1] PRIMARY KEY CLUSTERED ([IdCambio] ASC)
)
WITH (DATA_COMPRESSION = PAGE);

IF EXISTS (SELECT TOP 1 1 
           FROM   [dbo].[BitacoraCambios])
    BEGIN
        INSERT INTO [dbo].[tmp_ms_xx_BitacoraCambios] ([IdUsuario], [FechaHora], [Tipo], [Detalle])
        SELECT [IdUsuario],
               [FechaHora],
               [Tipo],
               [Detalle]
        FROM   [dbo].[BitacoraCambios];
    END

DROP TABLE [dbo].[BitacoraCambios];

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_BitacoraCambios]', N'BitacoraCambios';

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_constraint_PK_BitacoraCambios1]', N'PK_BitacoraCambios', N'OBJECT';

COMMIT TRANSACTION;

SET TRANSACTION ISOLATION LEVEL READ COMMITTED;


GO
PRINT N'Creando Tabla [dbo].[BitacoraRegistros]...';


GO
CREATE TABLE [dbo].[BitacoraRegistros] (
    [IdRegistro]       INT           IDENTITY (1, 1) NOT NULL,
    [IdUsuario]        INT           NOT NULL,
    [FechaHoraIngreso] DATETIME2 (7) NULL,
    [FechaHoraSalida]  DATETIME2 (7) NULL,
    CONSTRAINT [PK_BitacoraRegistros] PRIMARY KEY CLUSTERED ([IdRegistro] ASC)
)
WITH (DATA_COMPRESSION = PAGE);


GO
PRINT N'Creando Clave externa [dbo].[FK_BitacoraCambios_Usuarios]...';


GO
ALTER TABLE [dbo].[BitacoraCambios] WITH NOCHECK
    ADD CONSTRAINT [FK_BitacoraCambios_Usuarios] FOREIGN KEY ([IdUsuario]) REFERENCES [dbo].[Usuarios] ([IdUsuario]);


GO
PRINT N'Creando Clave externa [dbo].[FK_BitacoraRegistros_Usuarios]...';


GO
ALTER TABLE [dbo].[BitacoraRegistros] WITH NOCHECK
    ADD CONSTRAINT [FK_BitacoraRegistros_Usuarios] FOREIGN KEY ([IdUsuario]) REFERENCES [dbo].[Usuarios] ([IdUsuario]);


GO
PRINT N'Creando Procedimiento [dbo].[BitacoraCambiosInsertar]...';


GO
CREATE PROCEDURE [dbo].BitacoraCambiosInsertar
	@IdUsuario INT,		
	@FechaHora DATETIME2,
	@Tipo VARCHAR(100),
	@Detalle VARCHAR(500)

AS BEGIN
SET NOCOUNT ON

	BEGIN TRANSACTION TRANSA

	BEGIN TRY
	-- AQUI VA EL CODIGO
		
		INSERT INTO dbo.BitacoraCambios
		(
		    IdUsuario
        ,   FechaHora
		,   Tipo
		,   Detalle
		)
		VALUES
		(
		    @IdUsuario
        ,   @FechaHora
		,   @Tipo
		,   @Detalle
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
PRINT N'Creando Procedimiento [dbo].[BitacoraCambiosObtener]...';


GO
CREATE PROCEDURE [dbo].BitacoraCambiosObtener
@IdCambio INT =NULL
AS BEGIN
	SET NOCOUNT ON

	SELECT
			BC.IdCambio
        ,   BC.FechaHora
		,   BC.Tipo
		,   BC.Detalle

		,   U.IdUsuario
		,   U.Nombre

	FROM dbo.BitacoraCambios BC
	  	LEFT JOIN dbo.Usuarios U
	   ON BC.IdUsuario=U.IdUsuario
	WHERE
		(@IdCambio IS NULL OR IdCambio=@IdCambio)
END
GO
PRINT N'Creando Procedimiento [dbo].[BitacoraRegistrosInsertar]...';


GO
CREATE PROCEDURE [dbo].BitacoraRegistrosInsertar
	@IdUsuario INT,		
	@FechaHoraIngreso DATETIME2,
	@FechaHoraSalida  DATETIME2

AS BEGIN
SET NOCOUNT ON

	BEGIN TRANSACTION TRANSA

	BEGIN TRY
	-- AQUI VA EL CODIGO
		
		INSERT INTO dbo.BitacoraRegistros
		(
		    IdUsuario
        ,   FechaHoraIngreso
		,   FechaHoraSalida
		)
		VALUES
		(
		    @IdUsuario
        ,   @FechaHoraIngreso
		,   @FechaHoraSalida
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
PRINT N'Creando Procedimiento [dbo].[BitacoraRegistrosObtener]...';


GO
CREATE PROCEDURE [dbo].BitacoraRegistrosObtener
@IdRegistro INT =NULL
AS BEGIN
	SET NOCOUNT ON

	SELECT
			BR.IdRegistro
        ,   BR.FechaHoraIngreso
		,   BR.FechaHoraSalida

		,   U.IdUsuario
		,   U.Nombre

	FROM dbo.BitacoraRegistros BR
	  	LEFT JOIN dbo.Usuarios U
	   ON BR.IdUsuario=U.IdUsuario
	WHERE
		(@IdRegistro IS NULL OR IdRegistro=@IdRegistro)
END
GO
PRINT N'Comprobando los datos existentes con las restricciones recién creadas';


GO
USE [$(DatabaseName)];


GO
ALTER TABLE [dbo].[BitacoraCambios] WITH CHECK CHECK CONSTRAINT [FK_BitacoraCambios_Usuarios];

ALTER TABLE [dbo].[BitacoraRegistros] WITH CHECK CHECK CONSTRAINT [FK_BitacoraRegistros_Usuarios];


GO
PRINT N'Actualización completada.';


GO
