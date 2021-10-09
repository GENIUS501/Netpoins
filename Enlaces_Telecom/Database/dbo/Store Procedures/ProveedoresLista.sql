CREATE PROCEDURE [dbo].[ProveedoresLista]
AS BEGIN
	SET NOCOUNT ON

	SELECT
			IdProveedor
		,	NombreEmpresa


	FROM dbo.Proveedores

	ORDER BY NombreEmpresa

END