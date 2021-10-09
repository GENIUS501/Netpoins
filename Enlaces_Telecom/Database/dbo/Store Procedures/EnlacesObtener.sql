CREATE PROCEDURE [dbo].EnlacesObtener
@IdEnlace INT=NULL

AS BEGIN
	SET NOCOUNT ON
	SELECT	    
		    E.IdEnlace
		,   E.Comentario

		,	O.IdOficina
		,   O.NombreOficina
		,   O.UE
		,   O.Provincia
		,   O.Comentario

		,   R.IdRed
		,	R.Linea
		,   R.Gateway
		,	R.Interface
		,   R.TipoEnlace
		,   R.Bandwidth
		,   R.MedioEnlace
		,   R.Comentario

		,   P.IdProveedor
		,   P.NombreEmpresa

	FROM dbo.Enlaces E
	 LEFT JOIN dbo.Oficinas O
         ON E.IdOficina = O.IdOficina
	 LEFT JOIN dbo.Redes R
         ON E.IdRed = R.IdRed
    LEFT JOIN dbo.Proveedores P
         ON E.IdProveedor = P.IdProveedor
	WHERE
	     (@IdEnlace IS NULL OR IdEnlace=@IdEnlace)
END
