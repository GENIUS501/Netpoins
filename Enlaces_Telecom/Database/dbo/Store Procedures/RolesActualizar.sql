CREATE PROCEDURE [dbo].RolesActualizar
    @IdRol INT,
    @Rol VARCHAR(50),
	@Descripcion VARCHAR(200),
	@Estado BIT
AS BEGIN
SET NOCOUNT ON

	BEGIN TRANSACTION TRASA

	BEGIN TRY
	-- AQUI VA EL CODIGO
		
		UPDATE dbo.Roles SET
		Rol=@Rol,
		Descripcion=@Descripcion,
		Estado=@Estado

		WHERE IdRol=@IdRol
				
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