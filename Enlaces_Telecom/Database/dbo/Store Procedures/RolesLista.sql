CREATE PROCEDURE [dbo].RolesLista
AS BEGIN
	SET NOCOUNT ON
	SELECT
			IdRol
		,	Rol
		,   Descripcion

	FROM dbo.Roles
	WHERE
		Estado=1

	ORDER BY Rol
END