CREATE PROCEDURE [dbo].[EnlacesInsertar]
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
