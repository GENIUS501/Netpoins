CREATE PROCEDURE [dbo].[UsuariosActualizar]
    @IdUsuario INT,
	@IdRol INT,
	@Identificacion varchar(9),
	@Nombre VARCHAR(50),
	@Telefono VARCHAR(10),
	@Email  VARCHAR(50),
	@Usuario VARCHAR(50),
	@Contrasena VARCHAR(50),
	@Estado BIT

	AS BEGIN

SET NOCOUNT ON

	BEGIN TRANSACTION TRANSA

	BEGIN TRY
	-- AQUI VA EL CODIGO
		
	UPDATE DBO.Usuarios SET

	IdRol=@IdRol,
	Identificacion=@Identificacion,
    Nombre=@Nombre,
	Telefono=@Telefono,
	Email=@Email,
	Usuario=@Usuario,
	Contrasena=@Contrasena,
	Estado=@Estado	

	WHERE IdUsuario=@IdUsuario


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
