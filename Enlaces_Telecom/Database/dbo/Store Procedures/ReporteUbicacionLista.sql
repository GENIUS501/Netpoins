CREATE PROCEDURE [dbo].ReporteUbicacionLista
AS BEGIN
	SET NOCOUNT ON

	SELECT Provincia FROM dbo.Oficinas where Provincia=Provincia
		 order by IdOficina
END
