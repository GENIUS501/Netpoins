CREATE PROCEDURE [dbo].[OficinasLista]
AS BEGIN
	SET NOCOUNT ON

	SELECT
			IdOficina
		,	NombreOficina
		,   UE
		,   Provincia
		,   Comentario

	FROM dbo.Oficinas

		 order by NombreOficina
END
