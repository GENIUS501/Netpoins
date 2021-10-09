CREATE PROCEDURE [dbo].[ProveedoresInsertar]
	@NombreEmpresa VARCHAR(50),
	@Contacto  VARCHAR(50),
	@Telefono VARCHAR (10),
	@Email VARCHAR(50),
	@Comentario VARCHAR(500)

AS BEGIN
SET NOCOUNT ON

	BEGIN TRANSACTION TRANSA

	BEGIN TRY
	-- AQUI VA EL CODIGO
		
		INSERT INTO dbo.Proveedores
		(
		    NombreEmpresa
        ,   Contacto
		,   Telefono
		,   Email
		,   Comentario
		)
		VALUES
		(
		    @NombreEmpresa
        ,   @Contacto
		,   @Telefono
		,   @Email
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

