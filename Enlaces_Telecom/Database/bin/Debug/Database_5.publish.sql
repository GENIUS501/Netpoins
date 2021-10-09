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
PRINT N'Quitando Restricción CHECK [dbo].[CHK_Redes_MedioEnlace]...';


GO
ALTER TABLE [dbo].[Redes] DROP CONSTRAINT [CHK_Redes_MedioEnlace];


GO
PRINT N'Modificando Tabla [dbo].[Redes]...';


GO
ALTER TABLE [dbo].[Redes] ALTER COLUMN [MedioEnlace] VARCHAR (20) NOT NULL;


GO
PRINT N'Creando Restricción CHECK [dbo].[CHK_Redes_MedioEnlace]...';


GO
ALTER TABLE [dbo].[Redes] WITH NOCHECK
    ADD CONSTRAINT [CHK_Redes_MedioEnlace] CHECK (MedioEnlace='Fibra_Optica'  OR MedioEnlace='Cobre' OR MedioEnlace='Inalabrico' OR MedioEnlace='WMax');


GO
PRINT N'Actualizando Procedimiento [dbo].[OficinasObtener]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[OficinasObtener]';


GO
PRINT N'Actualizando Procedimiento [dbo].[RedesActualizar]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[RedesActualizar]';


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
ALTER TABLE [dbo].[Redes] WITH CHECK CHECK CONSTRAINT [CHK_Redes_MedioEnlace];


GO
PRINT N'Actualización completada.';


GO
