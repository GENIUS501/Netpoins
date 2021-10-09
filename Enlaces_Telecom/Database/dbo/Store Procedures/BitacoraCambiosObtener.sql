CREATE PROCEDURE [dbo].BitacoraCambiosObtener
@IdCambio INT =NULL
AS BEGIN
	SET NOCOUNT ON

	SELECT
			BC.IdCambio
        ,   BC.FechaHora
		,   BC.Tipo
		,   BC.Detalle

		,   U.IdUsuario
		,   U.Nombre

	FROM dbo.BitacoraCambios BC
	  	LEFT JOIN dbo.Usuarios U
	   ON BC.IdUsuario=U.IdUsuario
	WHERE
		(@IdCambio IS NULL OR IdCambio=@IdCambio)
END
