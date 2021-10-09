CREATE PROCEDURE [dbo].BitacoraRegistrosInsertar
	@IdUsuario INT,		
	@FechaHoraIngreso DATETIME2,
	@FechaHoraSalida  DATETIME2

AS BEGIN
SET NOCOUNT ON

	BEGIN TRANSACTION TRANSA

	BEGIN TRY
	-- AQUI VA EL CODIGO
		
		INSERT INTO dbo.BitacoraRegistros
		(
		    IdUsuario
        ,   FechaHoraIngreso
		,   FechaHoraSalida
		)
		VALUES
		(
		    @IdUsuario
        ,   @FechaHoraIngreso
		,   @FechaHoraSalida
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