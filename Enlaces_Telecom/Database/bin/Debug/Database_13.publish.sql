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

Se está quitando la columna [dbo].[Oficinas].[IdProveedor]; puede que se pierdan datos.

Se está quitando la columna [dbo].[Oficinas].[IdRed]; puede que se pierdan datos.

Debe agregarse la columna [dbo].[Oficinas].[Comentario] de la tabla [dbo].[Oficinas], pero esta columna no tiene un valor predeterminado y no admite valores NULL. Si la tabla contiene datos, el script ALTER no funcionará. Para evitar esta incidencia, agregue un valor predeterminado a la columna, márquela de modo que permita valores NULL o habilite la generación de valores predeterminados inteligentes como opción de implementación.
*/

IF EXISTS (select top 1 1 from [dbo].[Oficinas])
    RAISERROR (N'Se detectaron filas. La actualización del esquema va a terminar debido a una posible pérdida de datos.', 16, 127) WITH NOWAIT

GO
/*
Se está quitando la columna [dbo].[Proveedores].[Estado]; puede que se pierdan datos.

Debe agregarse la columna [dbo].[Proveedores].[Comentario] de la tabla [dbo].[Proveedores], pero esta columna no tiene un valor predeterminado y no admite valores NULL. Si la tabla contiene datos, el script ALTER no funcionará. Para evitar esta incidencia, agregue un valor predeterminado a la columna, márquela de modo que permita valores NULL o habilite la generación de valores predeterminados inteligentes como opción de implementación.
*/

IF EXISTS (select top 1 1 from [dbo].[Proveedores])
    RAISERROR (N'Se detectaron filas. La actualización del esquema va a terminar debido a una posible pérdida de datos.', 16, 127) WITH NOWAIT

GO
/*
Debe agregarse la columna [dbo].[Redes].[Comentario] de la tabla [dbo].[Redes], pero esta columna no tiene un valor predeterminado y no admite valores NULL. Si la tabla contiene datos, el script ALTER no funcionará. Para evitar esta incidencia, agregue un valor predeterminado a la columna, márquela de modo que permita valores NULL o habilite la generación de valores predeterminados inteligentes como opción de implementación.
*/

IF EXISTS (select top 1 1 from [dbo].[Redes])
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
PRINT N'Quitando Clave externa [dbo].[FK_Oficinas_Proveedores]...';


GO
ALTER TABLE [dbo].[Oficinas] DROP CONSTRAINT [FK_Oficinas_Proveedores];


GO
PRINT N'Quitando Clave externa [dbo].[FK_Oficinas_Redes]...';


GO
ALTER TABLE [dbo].[Oficinas] DROP CONSTRAINT [FK_Oficinas_Redes];


GO
PRINT N'Modificando Tabla [dbo].[Oficinas]...';


GO
ALTER TABLE [dbo].[Oficinas] DROP COLUMN [Estado], COLUMN [IdProveedor], COLUMN [IdRed];


GO
ALTER TABLE [dbo].[Oficinas]
    ADD [Comentario] VARCHAR (500) NOT NULL;


GO
PRINT N'Modificando Tabla [dbo].[Proveedores]...';


GO
ALTER TABLE [dbo].[Proveedores] DROP COLUMN [Estado];


GO
ALTER TABLE [dbo].[Proveedores]
    ADD [Comentario] VARCHAR (500) NOT NULL;


GO
PRINT N'Modificando Tabla [dbo].[Redes]...';


GO
ALTER TABLE [dbo].[Redes]
    ADD [Comentario] VARCHAR (500) NOT NULL;


GO
PRINT N'Creando Tabla [dbo].[Enlaces]...';


GO
CREATE TABLE [dbo].[Enlaces] (
    [IdEnlace]    INT           IDENTITY (1, 1) NOT NULL,
    [IdOficina]   INT           NOT NULL,
    [IdRed]       INT           NOT NULL,
    [IdProveedor] INT           NOT NULL,
    [Comentario]  VARCHAR (500) NOT NULL,
    CONSTRAINT [PK_Enlaces] PRIMARY KEY CLUSTERED ([IdEnlace] ASC)
)
WITH (DATA_COMPRESSION = PAGE);


GO
PRINT N'Creando Clave externa [dbo].[FK_Enlaces_Oficinas]...';


GO
ALTER TABLE [dbo].[Enlaces] WITH NOCHECK
    ADD CONSTRAINT [FK_Enlaces_Oficinas] FOREIGN KEY ([IdOficina]) REFERENCES [dbo].[Oficinas] ([IdOficina]);


GO
PRINT N'Creando Clave externa [dbo].[FK_Enlaces_Redes]...';


GO
ALTER TABLE [dbo].[Enlaces] WITH NOCHECK
    ADD CONSTRAINT [FK_Enlaces_Redes] FOREIGN KEY ([IdRed]) REFERENCES [dbo].[Redes] ([IdRed]);


GO
PRINT N'Creando Clave externa [dbo].[FK_Enlaces_Proveedores]...';


GO
ALTER TABLE [dbo].[Enlaces] WITH NOCHECK
    ADD CONSTRAINT [FK_Enlaces_Proveedores] FOREIGN KEY ([IdProveedor]) REFERENCES [dbo].[Proveedores] ([IdProveedor]);


GO
PRINT N'Modificando Procedimiento [dbo].[OficinasActualizar]...';


GO
ALTER PROCEDURE [dbo].OficinasActualizar
    @IdOficina INT,
	@NombreOficina VARCHAR(200),
	@UE VARCHAR(10),
	@Provincia VARCHAR(100),
	@Comentario VARCHAR(500)

AS BEGIN
SET NOCOUNT ON

	BEGIN TRANSACTION TRANSA

	BEGIN TRY
	-- AQUI VA EL CODIGO
		
	UPDATE DBO.Oficinas SET
	NombreOficina=@NombreOficina,
	UE=@UE,
    Provincia=@Provincia,
	Comentario=@Comentario

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
ALTER PROCEDURE [dbo].OficinasInsertar
	@NombreOficina VARCHAR(200),
	@UE VARCHAR(10),
	@Provincia VARCHAR(100),
	@Comentario VARCHAR(500)

AS BEGIN
SET NOCOUNT ON

	BEGIN TRANSACTION TRANSA

	BEGIN TRY
	-- AQUI VA EL CODIGO
		
		INSERT INTO dbo.Oficinas 
		(
	   NombreOficina
	, UE 
	, Provincia 
	, Comentario
		)
		VALUES
		(

	   @NombreOficina
	, @UE 
	, @Provincia
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
		,   Comentario


	FROM dbo.Oficinas

		 order by NombreOficina

END
GO
PRINT N'Modificando Procedimiento [dbo].[OficinasObtener]...';


GO
ALTER PROCEDURE [dbo].[OficinasObtener]
@IdOficina INT=NULL

AS BEGIN
	SET NOCOUNT ON

	SELECT
			IdOficina
		,	NombreOficina
		,   UE
		,   Provincia
		,   Comentario

	

	FROM dbo.Oficinas 

	WHERE
	     (@IdOficina IS NULL OR IdOficina=@IdOficina)

END
GO
PRINT N'Modificando Procedimiento [dbo].[ProveedoresActualizar]...';


GO
ALTER PROCEDURE [dbo].[ProveedoresActualizar]
	@IdProveedor INT,
	@NombreEmpresa VARCHAR(50),
	@Contacto  VARCHAR(50),
	@Telefono VARCHAR (10),
	@Email VARCHAR(50),
	@Comentario VARCHAR(500)

AS BEGIN
SET NOCOUNT ON

	BEGIN TRANSACTION TRASA

	BEGIN TRY
	-- AQUI VA EL CODIGO
		
		UPDATE dbo.Proveedores SET
		NombreEmpresa=@NombreEmpresa,
		Contacto=@Contacto,
		Telefono=@Telefono,
		Email=@Email,
		Comentario=@Comentario

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
	@Email VARCHAR(50),
	@Comentario VARCHAR(500)

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
		,   Comentario
		)
		VALUES
		(
		    @NombreEmpresa
        ,   @Contacto
		,   @Telefono
		,   @Email
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
		,   Comentario

	FROM dbo.Proveedores
	WHERE
		(@IdProveedor IS NULL OR IdProveedor=@IdProveedor)

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
    @MedioEnlace VARCHAR(100),
	@Comentario VARCHAR(500)
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
PRINT N'Actualizando Procedimiento [dbo].[OficinasEliminar]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[OficinasEliminar]';


GO
PRINT N'Actualizando Procedimiento [dbo].[ProveedoresEliminar]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[ProveedoresEliminar]';


GO
PRINT N'Actualizando Procedimiento [dbo].[RedesEliminar]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[RedesEliminar]';


GO
PRINT N'Actualizando Procedimiento [dbo].[RedesInsertar]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[RedesInsertar]';


GO
PRINT N'Actualizando Procedimiento [dbo].[RedesLista]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[RedesLista]';


GO
PRINT N'Actualizando Procedimiento [dbo].[RedesObtener]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[RedesObtener]';


GO
PRINT N'Comprobando los datos existentes con las restricciones recién creadas';


GO
USE [$(DatabaseName)];


GO
ALTER TABLE [dbo].[Enlaces] WITH CHECK CHECK CONSTRAINT [FK_Enlaces_Oficinas];

ALTER TABLE [dbo].[Enlaces] WITH CHECK CHECK CONSTRAINT [FK_Enlaces_Redes];

ALTER TABLE [dbo].[Enlaces] WITH CHECK CHECK CONSTRAINT [FK_Enlaces_Proveedores];


GO
PRINT N'Actualización completada.';


GO
