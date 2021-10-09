CREATE PROCEDURE [dbo].[ProveedoresActualizar]
	@IdProveedor INT,
	@NombreEmpresa VARCHAR(50),
	@Contacto  VARCHAR(50),
	@Telefono VARCHAR (10),
	@Email VARCHAR(50),
	@Comentario VARCHAR(500)

AS BEGIN
SET NOCOUNT ON

	BEGIN TRANSACTION TRASA

	BEGIN TRY
	-- AQUI VA EL CODIGO
		
		UPDATE dbo.Proveedores SET
		NombreEmpresa=@NombreEmpresa,
		Contacto=@Contacto,
		Telefono=@Telefono,
		Email=@Email,
		Comentario=@Comentario

		WHERE IdProveedor=@IdProveedor
				
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
