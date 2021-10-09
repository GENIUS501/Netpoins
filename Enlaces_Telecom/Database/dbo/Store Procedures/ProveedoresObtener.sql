CREATE PROCEDURE [dbo].[ProveedoresObtener]
@IdProveedor INT =NULL
AS BEGIN
	SET NOCOUNT ON

	SELECT
			IdProveedor
        ,   NombreEmpresa
		,   Contacto
		,   Telefono
		,   Email
		,   Comentario

	FROM dbo.Proveedores
	WHERE
		(@IdProveedor IS NULL OR IdProveedor=@IdProveedor)

END
