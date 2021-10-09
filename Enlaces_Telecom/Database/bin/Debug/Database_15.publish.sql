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
PRINT N'Modificando Tabla [dbo].[Oficinas]...';


GO
ALTER TABLE [dbo].[Oficinas] ALTER COLUMN [Comentario] VARCHAR (500) NULL;


GO
PRINT N'Modificando Tabla [dbo].[Proveedores]...';


GO
ALTER TABLE [dbo].[Proveedores] ALTER COLUMN [Comentario] VARCHAR (500) NULL;


GO
PRINT N'Actualizando Procedimiento [dbo].[OficinasActualizar]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[OficinasActualizar]';


GO
PRINT N'Actualizando Procedimiento [dbo].[OficinasEliminar]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[OficinasEliminar]';


GO
PRINT N'Actualizando Procedimiento [dbo].[OficinasInsertar]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[OficinasInsertar]';


GO
PRINT N'Actualizando Procedimiento [dbo].[OficinasLista]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[OficinasLista]';


GO
PRINT N'Actualizando Procedimiento [dbo].[OficinasObtener]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[OficinasObtener]';


GO
PRINT N'Actualizando Procedimiento [dbo].[ProveedoresActualizar]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[ProveedoresActualizar]';


GO
PRINT N'Actualizando Procedimiento [dbo].[ProveedoresEliminar]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[ProveedoresEliminar]';


GO
PRINT N'Actualizando Procedimiento [dbo].[ProveedoresInsertar]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[ProveedoresInsertar]';


GO
PRINT N'Actualizando Procedimiento [dbo].[ProveedoresLista]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[ProveedoresLista]';


GO
PRINT N'Actualizando Procedimiento [dbo].[ProveedoresObtener]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[ProveedoresObtener]';


GO
PRINT N'Actualización completada.';


GO
