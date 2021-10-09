CREATE PROCEDURE [dbo].[RedesInsertar]
	@Linea VARCHAR(25),
	@Gateway VARCHAR(25),
	@Interface VARCHAR(100),
    @TipoEnlace VARCHAR(100),
    @Bandwidth VARCHAR(25),
    @MedioEnlace VARCHAR(100),
	@Comentario VARCHAR(500)

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
		,   Comentario
		)
		VALUES
		(

		    @Linea
        ,   @Gateway
		,   @Interface
		,   @TipoEnlace
		,	@Bandwidth
		,	@MedioEnlace
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

