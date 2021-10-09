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
Se está quitando la columna [dbo].[Oficinas].[EU]; puede que se pierdan datos.

Debe agregarse la columna [dbo].[Oficinas].[IdProveedor] de la tabla [dbo].[Oficinas], pero esta columna no tiene un valor predeterminado y no admite valores NULL. Si la tabla contiene datos, el script ALTER no funcionará. Para evitar esta incidencia, agregue un valor predeterminado a la columna, márquela de modo que permita valores NULL o habilite la generación de valores predeterminados inteligentes como opción de implementación.

Debe agregarse la columna [dbo].[Oficinas].[IdRed] de la tabla [dbo].[Oficinas], pero esta columna no tiene un valor predeterminado y no admite valores NULL. Si la tabla contiene datos, el script ALTER no funcionará. Para evitar esta incidencia, agregue un valor predeterminado a la columna, márquela de modo que permita valores NULL o habilite la generación de valores predeterminados inteligentes como opción de implementación.

Debe agregarse la columna [dbo].[Oficinas].[UE] de la tabla [dbo].[Oficinas], pero esta columna no tiene un valor predeterminado y no admite valores NULL. Si la tabla contiene datos, el script ALTER no funcionará. Para evitar esta incidencia, agregue un valor predeterminado a la columna, márquela de modo que permita valores NULL o habilite la generación de valores predeterminados inteligentes como opción de implementación.
*/

IF EXISTS (select top 1 1 from [dbo].[Oficinas])
    RAISERROR (N'Se detectaron filas. La actualización del esquema va a terminar debido a una posible pérdida de datos.', 16, 127) WITH NOWAIT

GO
/*
El tipo de la columna Telefono en la tabla [dbo].[Proveedores] es  VARCHAR (50) NOT NULL, pero se va a cambiar a  VARCHAR (10) NOT NULL. Si la columna contiene datos no compatibles con el tipo  VARCHAR (10) NOT NULL, podrían producirse pérdidas de datos y errores en la implementación.
*/

IF EXISTS (select top 1 1 from [dbo].[Proveedores])
    RAISERROR (N'Se detectaron filas. La actualización del esquema va a terminar debido a una posible pérdida de datos.', 16, 127) WITH NOWAIT

GO
/*
Se está quitando la columna [dbo].[Redes].[Estado]; puede que se pierdan datos.
*/

IF EXISTS (select top 1 1 from [dbo].[Redes])
    RAISERROR (N'Se detectaron filas. La actualización del esquema va a terminar debido a una posible pérdida de datos.', 16, 127) WITH NOWAIT

GO
PRINT N'Quitando Restricción DEFAULT [dbo].[DF_Oficinas_Estado]...';


GO
ALTER TABLE [dbo].[Oficinas] DROP CONSTRAINT [DF_Oficinas_Estado];


GO
PRINT N'Quitando Restricción DEFAULT [dbo].[DF_Actividades_Estado]...';


GO
ALTER TABLE [dbo].[Redes] DROP CONSTRAINT [DF_Actividades_Estado];


GO
PRINT N'Iniciando recompilación de la tabla [dbo].[Oficinas]...';


GO
BEGIN TRANSACTION;

SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;

SET XACT_ABORT ON;

CREATE TABLE [dbo].[tmp_ms_xx_Oficinas] (
    [IdOficina]     INT           IDENTITY (1, 1) NOT NULL,
    [IdRed]         INT           NOT NULL,
    [IdProveedor]   INT           NOT NULL,
    [NombreOficina] VARCHAR (200) NOT NULL,
    [UE]            VARCHAR (10)  NOT NULL,
    [Provincia]     VARCHAR (1)   NOT NULL,
    [Estado]        BIT           CONSTRAINT [DF_Oficinas_Estado] DEFAULT (0) NOT NULL,
    CONSTRAINT [tmp_ms_xx_constraint_PK_Oficinas1] PRIMARY KEY CLUSTERED ([IdOficina] ASC)
)
WITH (DATA_COMPRESSION = PAGE);

IF EXISTS (SELECT TOP 1 1 
           FROM   [dbo].[Oficinas])
    BEGIN
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_Oficinas] ON;
        INSERT INTO [dbo].[tmp_ms_xx_Oficinas] ([IdOficina], [NombreOficina], [Provincia], [Estado])
        SELECT   [IdOficina],
                 [NombreOficina],
                 [Provincia],
                 [Estado]
        FROM     [dbo].[Oficinas]
        ORDER BY [IdOficina] ASC;
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_Oficinas] OFF;
    END

DROP TABLE [dbo].[Oficinas];

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_Oficinas]', N'Oficinas';

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_constraint_PK_Oficinas1]', N'PK_Oficinas', N'OBJECT';

COMMIT TRANSACTION;

SET TRANSACTION ISOLATION LEVEL READ COMMITTED;


GO
PRINT N'Modificando Tabla [dbo].[Proveedores]...';


GO
ALTER TABLE [dbo].[Proveedores] ALTER COLUMN [Telefono] VARCHAR (10) NOT NULL;


GO
PRINT N'Modificando Tabla [dbo].[Redes]...';


GO
ALTER TABLE [dbo].[Redes] DROP COLUMN [Estado];


GO
PRINT N'Creando Clave externa [dbo].[FK_Oficinas_Redes]...';


GO
ALTER TABLE [dbo].[Oficinas] WITH NOCHECK
    ADD CONSTRAINT [FK_Oficinas_Redes] FOREIGN KEY ([IdRed]) REFERENCES [dbo].[Redes] ([IdRed]);


GO
PRINT N'Creando Clave externa [dbo].[FK_Oficinas_Proveedores]...';


GO
ALTER TABLE [dbo].[Oficinas] WITH NOCHECK
    ADD CONSTRAINT [FK_Oficinas_Proveedores] FOREIGN KEY ([IdProveedor]) REFERENCES [dbo].[Proveedores] ([IdProveedor]);


GO
PRINT N'Creando Restricción CHECK [dbo].[CHK_Oficina_Provincia]...';


GO
ALTER TABLE [dbo].[Oficinas] WITH NOCHECK
    ADD CONSTRAINT [CHK_Oficina_Provincia] CHECK (Provincia='S' OR Provincia='H' OR Provincia='A' OR Provincia='C' OR Provincia='L' OR Provincia='G' OR Provincia='P');


GO
PRINT N'Creando Procedimiento [dbo].[OficinasActualizar]...';


GO
CREATE PROCEDURE [dbo].[OficinasActualizar]
    @IdOficina INT,
    @IdRed INT,
	@IdProveedor INT,
	@NombreOficina VARCHAR(200),
	@UE VARCHAR(10),
	@Provincia VARCHAR(1),
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
PRINT N'Creando Procedimiento [dbo].[OficinasEliminar]...';


GO
CREATE PROCEDURE [dbo].[OficinasEliminar]
  @IdOficina INT
  
AS BEGIN
SET NOCOUNT ON

	BEGIN TRANSACTION TRASA

	BEGIN TRY
	-- AQUI VA EL CODIGO
		
	DELETE FROM DBO.Oficinas WHERE IdOficina=@IdOficina


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
PRINT N'Creando Procedimiento [dbo].[OficinasInsertar]...';


GO
CREATE PROCEDURE [dbo].[OficinasInsertar]
    @IdRed INT,
	@IdProveedor INT,
	@NombreOficina VARCHAR(200),
	@UE VARCHAR(10),
	@Provincia VARCHAR(1),
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
PRINT N'Creando Procedimiento [dbo].[OficinasLista]...';


GO
CREATE PROCEDURE [dbo].[OficinasLista]
AS BEGIN
	SET NOCOUNT ON

	SELECT
			IdOficina
		,	NombreOficina
		,   UE
		,   Provincia


	FROM dbo.Oficinas
	WHERE
	     Estado=1
		 order by Estado

END
GO
PRINT N'Creando Procedimiento [dbo].[OficinasObtener]...';


GO
CREATE PROCEDURE [dbo].[OficinasObtener]
@IdOficina INT=NULL

AS BEGIN
	SET NOCOUNT ON

	SELECT
			O.IdOficina
		,	O.NombreOficina
		,   O.UE
		,   O.Provincia

		,   R.IdRed
		,	R.Linea
		,   R.Gateway
		,   R.Interface
		,   R.TipoEnlace
		,   R.Bandwidth
		,   R.MedioEnlace

		,   P.IdProveedor
		,   P.Empresa
		

	FROM dbo.Oficinas O
	 LEFT JOIN dbo.Redes R
         ON O.IdRed = R.IdRed
	 LEFT JOIN dbo.Proveedores P
         ON O.IdProveedor = P.IdProveedor
	WHERE
	     (@IdOficina IS NULL OR IdOficina=@IdOficina)

END
GO
PRINT N'Creando Procedimiento [dbo].[ProveedoresActualizar]...';


GO
CREATE PROCEDURE [dbo].[ProveedoresActualizar]
	@IdProveedor INT,
	@Empresa VARCHAR(50),
	@Contacto  VARCHAR(50),
	@Telefono VARCHAR (10),
	@Email VARCHAR(50),
	@Estado BIT

AS BEGIN
SET NOCOUNT ON

	BEGIN TRANSACTION TRASA

	BEGIN TRY
	-- AQUI VA EL CODIGO
		
		UPDATE dbo.Proveedores SET
		Empresa=@Empresa,
		Contacto=@Contacto,
		Telefono=@Telefono,
		Email=@Email,
		Estado=@Estado

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
PRINT N'Creando Procedimiento [dbo].[ProveedoresEliminar]...';


GO
CREATE PROCEDURE [dbo].[ProveedoresEliminar]
	@IdProveedor INT

AS BEGIN
SET NOCOUNT ON

	BEGIN TRANSACTION TRANSA

	BEGIN TRY
	-- AQUI VA EL CODIGO
		
		DELETE FROM dbo.Proveedores WHERE IdProveedor=@IdProveedor

		
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
PRINT N'Creando Procedimiento [dbo].[ProveedoresInsertar]...';


GO
CREATE PROCEDURE [dbo].[ProveedoresInsertar]
	@Empresa VARCHAR(50),
	@Contacto  VARCHAR(50),
	@Telefono VARCHAR (10),
	@Email VARCHAR(50),
	@Estado BIT

AS BEGIN
SET NOCOUNT ON

	BEGIN TRANSACTION TRANSA

	BEGIN TRY
	-- AQUI VA EL CODIGO
		
		INSERT INTO dbo.Proveedores
		(
		    Empresa
        ,   Contacto
		,   Telefono
		,   Email
		,	Estado
		)
		VALUES
		(
		    @Empresa
        ,   @Contacto
		,   @Telefono
		,   @Email
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
PRINT N'Creando Procedimiento [dbo].[ProveedoresLista]...';


GO
CREATE PROCEDURE [dbo].[ProveedoresLista]
AS BEGIN
	SET NOCOUNT ON

	SELECT
			IdProveedor
		,	Empresa


	FROM dbo.Proveedores
	WHERE
		Estado=1
	ORDER BY Empresa

END
GO
PRINT N'Creando Procedimiento [dbo].[ProveedoresObtener]...';


GO
CREATE PROCEDURE [dbo].[ProveedoresObtener]
@IdProveedor INT =NULL
AS BEGIN
	SET NOCOUNT ON

	SELECT
			IdProveedor
        ,   Empresa
		,   Contacto
		,   Telefono
		,   Email
		,	Estado

	FROM dbo.Proveedores
	WHERE
		(@IdProveedor IS NULL OR IdProveedor=@IdProveedor)

END
GO
PRINT N'Creando Procedimiento [dbo].[RedesActualizar]...';


GO
CREATE PROCEDURE [dbo].[RedesActualizar]
    @IdRed INT,
	@Linea VARCHAR(25),
	@Gateway VARCHAR(25),
	@Interface VARCHAR(1),
    @TipoEnlace VARCHAR(1),
    @Bandwidth VARCHAR(25),
    @MedioEnlace VARCHAR(1)

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
PRINT N'Creando Procedimiento [dbo].[RedesEliminar]...';


GO
CREATE PROCEDURE [dbo].[RedesEliminar]
	@IdRed INT

AS BEGIN
SET NOCOUNT ON

	BEGIN TRANSACTION TRANSA

	BEGIN TRY
	-- AQUI VA EL CODIGO
		
		DELETE FROM dbo.Redes WHERE IdRed=@IdRed

		
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
PRINT N'Creando Procedimiento [dbo].[RedesInsertar]...';


GO
CREATE PROCEDURE [dbo].[RedesInsertar]
	@Linea VARCHAR(25),
	@Gateway VARCHAR(25),
	@Interface VARCHAR(1),
    @TipoEnlace VARCHAR(1),
    @Bandwidth VARCHAR(25),
    @MedioEnlace VARCHAR(1)

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
PRINT N'Creando Procedimiento [dbo].[RedesLista]...';


GO
CREATE PROCEDURE [dbo].[RedesLista]
AS BEGIN
	SET NOCOUNT ON

	SELECT
			Idred
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
PRINT N'Creando Procedimiento [dbo].[RedesObtener]...';


GO
CREATE PROCEDURE [dbo].[RedesObtener]
@IdRed INT =NULL
AS BEGIN
	SET NOCOUNT ON

	SELECT
			IdRed
        ,   Linea
		,   Gateway
		,   Interface
		,   TipoEnlace
		,	Bandwidth
		,	MedioEnlace

	FROM dbo.Redes
	WHERE
		(@IdRed IS NULL OR IdRed=@IdRed)

END
GO
PRINT N'Comprobando los datos existentes con las restricciones recién creadas';


GO
USE [$(DatabaseName)];


GO
ALTER TABLE [dbo].[Oficinas] WITH CHECK CHECK CONSTRAINT [FK_Oficinas_Redes];

ALTER TABLE [dbo].[Oficinas] WITH CHECK CHECK CONSTRAINT [FK_Oficinas_Proveedores];

ALTER TABLE [dbo].[Oficinas] WITH CHECK CHECK CONSTRAINT [CHK_Oficina_Provincia];


GO
PRINT N'Actualización completada.';


GO
