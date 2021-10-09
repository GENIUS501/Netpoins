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
Debe agregarse la columna [dbo].[Redes].[IdProveedor] de la tabla [dbo].[Redes], pero esta columna no tiene un valor predeterminado y no admite valores NULL. Si la tabla contiene datos, el script ALTER no funcionará. Para evitar esta incidencia, agregue un valor predeterminado a la columna, márquela de modo que permita valores NULL o habilite la generación de valores predeterminados inteligentes como opción de implementación.
*/

IF EXISTS (select top 1 1 from [dbo].[Redes])
    RAISERROR (N'Se detectaron filas. La actualización del esquema va a terminar debido a una posible pérdida de datos.', 16, 127) WITH NOWAIT

GO
PRINT N'Quitando Clave externa [dbo].[FK_Enlaces_Redes]...';


GO
ALTER TABLE [dbo].[Enlaces] DROP CONSTRAINT [FK_Enlaces_Redes];


GO
PRINT N'Modificando Tabla [dbo].[Enlaces]...';


GO
ALTER TABLE [dbo].[Enlaces]
    ADD [Comentario] VARCHAR (500) NULL;


GO
PRINT N'Iniciando recompilación de la tabla [dbo].[Redes]...';


GO
BEGIN TRANSACTION;

SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;

SET XACT_ABORT ON;

CREATE TABLE [dbo].[tmp_ms_xx_Redes] (
    [IdRed]       INT           IDENTITY (1, 1) NOT NULL,
    [IdProveedor] INT           NOT NULL,
    [Linea]       VARCHAR (25)  NOT NULL,
    [Gateway]     VARCHAR (25)  NOT NULL,
    [Interface]   VARCHAR (100) NOT NULL,
    [TipoEnlace]  VARCHAR (100) NOT NULL,
    [Bandwidth]   VARCHAR (25)  NOT NULL,
    [MedioEnlace] VARCHAR (100) NOT NULL,
    [Comentario]  VARCHAR (500) NULL,
    CONSTRAINT [tmp_ms_xx_constraint_PK_Redes1] PRIMARY KEY CLUSTERED ([IdRed] ASC)
)
WITH (DATA_COMPRESSION = PAGE);

IF EXISTS (SELECT TOP 1 1 
           FROM   [dbo].[Redes])
    BEGIN
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_Redes] ON;
        INSERT INTO [dbo].[tmp_ms_xx_Redes] ([IdRed], [Linea], [Gateway], [Interface], [TipoEnlace], [Bandwidth], [MedioEnlace], [Comentario])
        SELECT   [IdRed],
                 [Linea],
                 [Gateway],
                 [Interface],
                 [TipoEnlace],
                 [Bandwidth],
                 [MedioEnlace],
                 [Comentario]
        FROM     [dbo].[Redes]
        ORDER BY [IdRed] ASC;
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_Redes] OFF;
    END

DROP TABLE [dbo].[Redes];

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_Redes]', N'Redes';

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_constraint_PK_Redes1]', N'PK_Redes', N'OBJECT';

COMMIT TRANSACTION;

SET TRANSACTION ISOLATION LEVEL READ COMMITTED;


GO
PRINT N'Creando Tabla [dbo].[BitacoraCambios]...';


GO
CREATE TABLE [dbo].[BitacoraCambios] (
    [IdBitCambios] INT           IDENTITY (1, 1) NOT NULL,
    [IdUsuario]    INT           NOT NULL,
    [FechaHora]    DATETIME2 (7) NULL,
    [Tipo]         VARCHAR (100) NOT NULL,
    [Detalle]      VARCHAR (500) NOT NULL,
    CONSTRAINT [PK_BitacoraCambios] PRIMARY KEY CLUSTERED ([IdBitCambios] ASC)
)
WITH (DATA_COMPRESSION = PAGE);


GO
PRINT N'Creando Tabla [dbo].[BitacoraRegistro]...';


GO
CREATE TABLE [dbo].[BitacoraRegistro] (
    [IdRegistroLog]    INT           IDENTITY (1, 1) NOT NULL,
    [IdUsuario]        INT           NOT NULL,
    [FechaHoraIngreso] DATETIME2 (7) NULL,
    [FechaHoraSalida]  DATETIME2 (7) NULL,
    CONSTRAINT [PK_BitacoraRegistro] PRIMARY KEY CLUSTERED ([IdRegistroLog] ASC)
)
WITH (DATA_COMPRESSION = PAGE);


GO
PRINT N'Creando Clave externa [dbo].[FK_Enlaces_Redes]...';


GO
ALTER TABLE [dbo].[Enlaces] WITH NOCHECK
    ADD CONSTRAINT [FK_Enlaces_Redes] FOREIGN KEY ([IdRed]) REFERENCES [dbo].[Redes] ([IdRed]);


GO
PRINT N'Creando Clave externa [dbo].[FK_Redes_Proveedores]...';


GO
ALTER TABLE [dbo].[Redes] WITH NOCHECK
    ADD CONSTRAINT [FK_Redes_Proveedores] FOREIGN KEY ([IdProveedor]) REFERENCES [dbo].[Proveedores] ([IdProveedor]);


GO
PRINT N'Creando Clave externa [dbo].[FK_BitacoraCambios_Usuarios]...';


GO
ALTER TABLE [dbo].[BitacoraCambios] WITH NOCHECK
    ADD CONSTRAINT [FK_BitacoraCambios_Usuarios] FOREIGN KEY ([IdUsuario]) REFERENCES [dbo].[Usuarios] ([IdUsuario]);


GO
PRINT N'Creando Clave externa [dbo].[FK_BitacoraRegistro_Usuarios]...';


GO
ALTER TABLE [dbo].[BitacoraRegistro] WITH NOCHECK
    ADD CONSTRAINT [FK_BitacoraRegistro_Usuarios] FOREIGN KEY ([IdUsuario]) REFERENCES [dbo].[Usuarios] ([IdUsuario]);


GO
PRINT N'Modificando Procedimiento [dbo].[EnlacesActualizar]...';


GO
ALTER PROCEDURE [dbo].[EnlacesActualizar]
    @IdEnlace INT,
    @IdOficina INT,
	@IdRed INT,
	@IdProveedor INT,
	@Comentario VARCHAR(500)

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
PRINT N'Modificando Procedimiento [dbo].[EnlacesInsertar]...';


GO
ALTER PROCEDURE [dbo].[EnlacesInsertar]
    @IdOficina INT,
	@IdRed INT,
	@IdProveedor INT,
	@Comentario VARCHAR(500)

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
		,   Comentario
		)
		VALUES
		(
		 @IdOficina
	    , @IdRed
	    , @IdProveedor
		, @Comentario
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
PRINT N'Modificando Procedimiento [dbo].[EnlacesLista]...';


GO
ALTER PROCEDURE [dbo].[EnlacesLista]
AS BEGIN
	SET NOCOUNT ON

	SELECT
			IdEnlace
		,	IdOficina
		,   IdRed
		,   IdProveedor
		,   Comentario

	FROM dbo.Enlaces

		 order by IdEnlace
END
GO
PRINT N'Modificando Procedimiento [dbo].[EnlacesObtener]...';


GO
ALTER PROCEDURE [dbo].EnlacesObtener
@IdEnlace INT=NULL

AS BEGIN
	SET NOCOUNT ON
	SELECT	    
		    E.IdEnlace
		,   E.Comentario

		,	O.IdOficina
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
PRINT N'Modificando Procedimiento [dbo].[RedesActualizar]...';


GO
ALTER PROCEDURE [dbo].[RedesActualizar]
    @IdProveedor INT,	
    @IdRed INT,
	@Linea VARCHAR(25),
	@Gateway VARCHAR(25),
	@Interface VARCHAR(100),
    @TipoEnlace VARCHAR(100),
    @Bandwidth VARCHAR(25),
    @MedioEnlace VARCHAR(100),
	@Comentario VARCHAR(500)
AS BEGIN
SET NOCOUNT ON

	BEGIN TRANSACTION TRASA

	BEGIN TRY
	-- AQUI VA EL CODIGO
		
		UPDATE dbo.Redes SET
		IdProveedor=@IdProveedor,
		Linea=@Linea,
		Gateway=@Gateway,
		Interface=@Interface,
		TipoEnlace=@TipoEnlace,
		Bandwidth=@Bandwidth,
		MedioEnlace=@MedioEnlace,
		Comentario=@Comentario

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
    @IdProveedor INT,	
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
		    IdProveedor
		,   Linea
        ,   Gateway
		,   Interface
		,   TipoEnlace
		,	Bandwidth
		,	MedioEnlace
		,   Comentario
		)
		VALUES
		(
		    @IdProveedor
		,   @Linea
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
PRINT N'Modificando Procedimiento [dbo].[RedesLista]...';


GO
ALTER PROCEDURE [dbo].[RedesLista]
AS BEGIN
	SET NOCOUNT ON

	SELECT
			Idred
		,   IdProveedor
		,	Linea
		,   Gateway
		,   Interface
		,   TipoEnlace
		,   Bandwidth
		,   MedioEnlace

	FROM dbo.Redes
	
	ORDER BY Linea
END
GO
PRINT N'Modificando Procedimiento [dbo].[RedesObtener]...';


GO
ALTER PROCEDURE [dbo].[RedesObtener]
@IdRed INT =NULL
AS BEGIN
	SET NOCOUNT ON

	SELECT
			R.IdRed
        ,   R.Linea
		,   R.Gateway
		,   R.Interface
		,   R.TipoEnlace
		,	R.Bandwidth
		,	R.MedioEnlace
		,   R.Comentario
		
		,   P.IdProveedor
		,   P.NombreEmpresa

	FROM dbo.Redes R
		 LEFT JOIN dbo.Proveedores P
	  ON R.IdProveedor=P.IdProveedor
	WHERE
		(@IdRed IS NULL OR IdRed=@IdRed)
END
GO
PRINT N'Modificando Procedimiento [dbo].[UsuariosLista]...';


GO
ALTER PROCEDURE [dbo].UsuariosLista
AS BEGIN
	SET NOCOUNT ON

	SELECT
			IdUsuario
        ,   Identificacion
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
PRINT N'Actualizando Procedimiento [dbo].[EnlacesEliminar]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[EnlacesEliminar]';


GO
PRINT N'Actualizando Procedimiento [dbo].[RedesEliminar]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[RedesEliminar]';


GO
PRINT N'Comprobando los datos existentes con las restricciones recién creadas';


GO
USE [$(DatabaseName)];


GO
ALTER TABLE [dbo].[Enlaces] WITH CHECK CHECK CONSTRAINT [FK_Enlaces_Redes];

ALTER TABLE [dbo].[Redes] WITH CHECK CHECK CONSTRAINT [FK_Redes_Proveedores];

ALTER TABLE [dbo].[BitacoraCambios] WITH CHECK CHECK CONSTRAINT [FK_BitacoraCambios_Usuarios];

ALTER TABLE [dbo].[BitacoraRegistro] WITH CHECK CHECK CONSTRAINT [FK_BitacoraRegistro_Usuarios];


GO
PRINT N'Actualización completada.';


GO
