CREATE PROCEDURE [dbo].[UsuariosInsertar]
	@IdRol INT,		
	@Identificacion VARCHAR(9),
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
		
		INSERT INTO dbo.Usuarios
		(
		    IdRol
        ,   Identificacion
		,   Nombre
		,   Telefono
		,	Email
		,   Usuario
		,   Contrasena
		,	Estado
		)
		VALUES
		(
		    @IdRol
        ,   @Identificacion
		,   @Nombre
		,   @Telefono
		,	@Email
		,   @Usuario
		,   @Contrasena
		,	@Estado
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

