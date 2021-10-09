CREATE PROCEDURE [dbo].OficinasInsertar
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
