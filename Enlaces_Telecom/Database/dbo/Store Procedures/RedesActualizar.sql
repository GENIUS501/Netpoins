CREATE PROCEDURE [dbo].[RedesActualizar]
    @IdRed INT,
	@Linea VARCHAR(25),
	@Gateway VARCHAR(25),
	@Interface VARCHAR(100),
    @TipoEnlace VARCHAR(100),
    @Bandwidth VARCHAR(25),
    @MedioEnlace VARCHAR(100),
	@Comentario VARCHAR(500)
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
		MedioEnlace=@MedioEnlace,
		Comentario=@Comentario

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