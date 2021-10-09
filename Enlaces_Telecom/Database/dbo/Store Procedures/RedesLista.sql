CREATE PROCEDURE [dbo].[RedesLista]
AS BEGIN
	SET NOCOUNT ON

	SELECT
			Idred
		,	Linea
		,   Gateway
		,   Interface
		,   TipoEnlace
		,   Bandwidth
		,   MedioEnlace

	FROM dbo.Redes
	
	ORDER BY Linea
END
