CREATE PROCEDURE [dbo].[OficinasObtener]
@IdOficina INT=NULL

AS BEGIN
	SET NOCOUNT ON

	SELECT
			IdOficina
		,	NombreOficina
		,   UE
		,   Provincia
		,   Comentario

	FROM dbo.Oficinas 

	WHERE
	     (@IdOficina IS NULL OR IdOficina=@IdOficina)
END
