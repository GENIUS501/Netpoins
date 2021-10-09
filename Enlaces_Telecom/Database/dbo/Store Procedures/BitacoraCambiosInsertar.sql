CREATE PROCEDURE [dbo].BitacoraCambiosInsertar
	@IdUsuario INT,		
	@FechaHora DATETIME2,
	@Tipo VARCHAR(100),
	@Detalle VARCHAR(500)

AS BEGIN
SET NOCOUNT ON

	BEGIN TRANSACTION TRANSA

	BEGIN TRY
	-- AQUI VA EL CODIGO
		
		INSERT INTO dbo.BitacoraCambios
		(
		    IdUsuario
        ,   FechaHora
		,   Tipo
		,   Detalle
		)
		VALUES
		(
		    @IdUsuario
        ,   @FechaHora
		,   @Tipo
		,   @Detalle
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
