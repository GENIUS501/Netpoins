CREATE PROCEDURE [dbo].[RedesObtener]
@IdRed INT =NULL
AS BEGIN
	SET NOCOUNT ON

	SELECT
			R.IdRed
        ,   R.Linea
		,   R.Gateway
		,   R.Interface
		,   R.TipoEnlace
		,	R.Bandwidth
		,	R.MedioEnlace
		,   R.Comentario
		
	FROM dbo.Redes R
	WHERE
		(@IdRed IS NULL OR IdRed=@IdRed)
END

