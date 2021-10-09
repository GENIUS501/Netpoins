CREATE PROCEDURE [dbo].RolesInsertar
    @Rol VARCHAR(50), 
	@Descripcion VARCHAR(200),
	@Estado BIT

AS BEGIN
SET NOCOUNT ON

	BEGIN TRANSACTION TRANSA

	BEGIN TRY
	-- AQUI VA EL CODIGO
		
		INSERT INTO dbo.Roles
		(
		    Rol
        ,   Descripcion
		,	Estado
		)
		VALUES
		(
		    @Rol
		,	@Descripcion
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
