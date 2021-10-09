CREATE PROCEDURE [dbo].UsuariosObtener
@IdUsuario INT =NULL
AS BEGIN
	SET NOCOUNT ON

	SELECT
			U.IdUsuario
        ,   U.Identificacion
		,   U.Nombre
		,   U.Telefono
		,	U.Email
		,   U.Usuario
		,	U.Estado

		,   R.IdRol
		,   R.Rol

	FROM dbo.Usuarios U
	  	LEFT JOIN dbo.Roles R
	   ON U.IdRol=R.IdRol
	WHERE
		(@IdUsuario IS NULL OR IdUsuario=@IdUsuario)
END