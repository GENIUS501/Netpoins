CREATE PROCEDURE [dbo].UsuariosLista
AS BEGIN
	SET NOCOUNT ON

	SELECT
			IdUsuario
        ,   Identificacion
		,	Nombre
		,   Telefono
		,   Email
		,   Usuario
		,   Estado

	FROM dbo.Usuarios
	WHERE
		Estado=1
	ORDER BY Nombre
END