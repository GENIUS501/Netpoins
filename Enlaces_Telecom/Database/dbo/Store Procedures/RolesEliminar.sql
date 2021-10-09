﻿CREATE PROCEDURE [dbo].RolesEliminar
	@IdRol INT

AS BEGIN
SET NOCOUNT ON

	BEGIN TRANSACTION TRANSA

	BEGIN TRY
	-- AQUI VA EL CODIGO
		
		DELETE FROM dbo.Roles WHERE IdRol=@IdRol
		
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
