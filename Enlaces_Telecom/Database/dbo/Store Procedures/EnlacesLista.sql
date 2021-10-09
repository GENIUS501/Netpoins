CREATE PROCEDURE [dbo].[EnlacesLista]
AS BEGIN
	SET NOCOUNT ON

	SELECT
			IdEnlace
		,	IdOficina
		,   IdRed
		,   IdProveedor
		,   Comentario

	FROM dbo.Enlaces
		 order by IdEnlace
END
