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
Se está quitando la columna [dbo].[Proveedores].[Empresa]; puede que se pierdan datos.

Debe agregarse la columna [dbo].[Proveedores].[NombreEmpresa] de la tabla [dbo].[Proveedores], pero esta columna no tiene un valor predeterminado y no admite valores NULL. Si la tabla contiene datos, el script ALTER no funcionará. Para evitar esta incidencia, agregue un valor predeterminado a la columna, márquela de modo que permita valores NULL o habilite la generación de valores predeterminados inteligentes como opción de implementación.
*/

IF EXISTS (select top 1 1 from [dbo].[Proveedores])
    RAISERROR (N'Se detectaron filas. La actualización del esquema va a terminar debido a una posible pérdida de datos.', 16, 127) WITH NOWAIT

GO
PRINT N'Quitando Restricción DEFAULT [dbo].[DF_Proveedores_Estado]...';


GO
ALTER TABLE [dbo].[Proveedores] DROP CONSTRAINT [DF_Proveedores_Estado];


GO
PRINT N'Quitando Clave externa [dbo].[FK_Oficinas_Proveedores]...';


GO
ALTER TABLE [dbo].[Oficinas] DROP CONSTRAINT [FK_Oficinas_Proveedores];


GO
PRINT N'Iniciando recompilación de la tabla [dbo].[Proveedores]...';


GO
BEGIN TRANSACTION;

SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;

SET XACT_ABORT ON;

CREATE TABLE [dbo].[tmp_ms_xx_Proveedores] (
    [IdProveedor]   INT          IDENTITY (1, 1) NOT NULL,
    [NombreEmpresa] VARCHAR (50) NOT NULL,
    [Contacto]      VARCHAR (50) NOT NULL,
    [Telefono]      VARCHAR (10) NOT NULL,
    [Email]         VARCHAR (50) NOT NULL,
    [Estado]        BIT          CONSTRAINT [DF_Proveedores_Estado] DEFAULT (0) NOT NULL,
    CONSTRAINT [tmp_ms_xx_constraint_PK_Proveedores1] PRIMARY KEY CLUSTERED ([IdProveedor] ASC)
)
WITH (DATA_COMPRESSION = PAGE);

IF EXISTS (SELECT TOP 1 1 
           FROM   [dbo].[Proveedores])
    BEGIN
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_Proveedores] ON;
        INSERT INTO [dbo].[tmp_ms_xx_Proveedores] ([IdProveedor], [Contacto], [Telefono], [Email], [Estado])
        SELECT   [IdProveedor],
                 [Contacto],
                 [Telefono],
                 [Email],
                 [Estado]
        FROM     [dbo].[Proveedores]
        ORDER BY [IdProveedor] ASC;
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_Proveedores] OFF;
    END

DROP TABLE [dbo].[Proveedores];

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_Proveedores]', N'Proveedores';

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_constraint_PK_Proveedores1]', N'PK_Proveedores', N'OBJECT';

COMMIT TRANSACTION;

SET TRANSACTION ISOLATION LEVEL READ COMMITTED;


GO
PRINT N'Creando Clave externa [dbo].[FK_Oficinas_Proveedores]...';


GO
ALTER TABLE [dbo].[Oficinas] WITH NOCHECK
    ADD CONSTRAINT [FK_Oficinas_Proveedores] FOREIGN KEY ([IdProveedor]) REFERENCES [dbo].[Proveedores] ([IdProveedor]);


GO
PRINT N'Modificando Procedimiento [dbo].[OficinasObtener]...';


GO
ALTER PROCEDURE [dbo].[OficinasObtener]
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
		,   P.NombreEmpresa
		

	FROM dbo.Oficinas O
	 LEFT JOIN dbo.Redes R
         ON O.IdRed = R.IdRed
	 LEFT JOIN dbo.Proveedores P
         ON O.IdProveedor = P.IdProveedor
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
	@Estado BIT

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
PRINT N'Modificando Procedimiento [dbo].[ProveedoresInsertar]...';


GO
ALTER PROCEDURE [dbo].[ProveedoresInsertar]
	@NombreEmpresa VARCHAR(50),
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
		    NombreEmpresa
        ,   Contacto
		,   Telefono
		,   Email
		,	Estado
		)
		VALUES
		(
		    @NombreEmpresa
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
PRINT N'Modificando Procedimiento [dbo].[ProveedoresLista]...';


GO
ALTER PROCEDURE [dbo].[ProveedoresLista]
AS BEGIN
	SET NOCOUNT ON

	SELECT
			IdProveedor
		,	NombreEmpresa


	FROM dbo.Proveedores
	WHERE
		Estado=1
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
		,	Estado

	FROM dbo.Proveedores
	WHERE
		(@IdProveedor IS NULL OR IdProveedor=@IdProveedor)

END
GO
PRINT N'Actualizando Procedimiento [dbo].[ProveedoresEliminar]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[ProveedoresEliminar]';


GO
PRINT N'Comprobando los datos existentes con las restricciones recién creadas';


GO
USE [$(DatabaseName)];


GO
ALTER TABLE [dbo].[Oficinas] WITH CHECK CHECK CONSTRAINT [FK_Oficinas_Proveedores];


GO
PRINT N'Actualización completada.';


GO
