CREATE PROCEDURE [dbo].BitacoraRegistrosObtener
@IdRegistro INT =NULL
AS BEGIN
	SET NOCOUNT ON

	SELECT
			BR.IdRegistro
        ,   BR.FechaHoraIngreso
		,   BR.FechaHoraSalida

		,   U.IdUsuario
		,   U.Nombre

	FROM dbo.BitacoraRegistros BR
	  	LEFT JOIN dbo.Usuarios U
	   ON BR.IdUsuario=U.IdUsuario
	WHERE
		(@IdRegistro IS NULL OR IdRegistro=@IdRegistro)
END
