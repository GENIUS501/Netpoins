CREATE PROCEDURE [dbo].[EnlacesActualizar]
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
