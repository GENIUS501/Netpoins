CREATE PROCEDURE [dbo].RolesObtener
@IdRol INT =NULL
AS BEGIN
	SET NOCOUNT ON

	SELECT
			IdRol
        ,   Rol
		,	Descripcion
		,	Estado

	FROM dbo.Roles
	WHERE
		(@IdRol IS NULL OR IdRol=@IdRol)
END
