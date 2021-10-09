CREATE TABLE BitacoraRegistros
(
    IdRegistro INT NOT NULL IDENTITY(1,1) 
	        CONSTRAINT PK_BitacoraRegistros PRIMARY KEY CLUSTERED (IdRegistro)
	, IdUsuario INT NOT NULL 
		CONSTRAINT FK_BitacoraRegistros_Usuarios FOREIGN KEY(IdUsuario) REFERENCES dbo.Usuarios(IdUsuario)
	, FechaHoraIngreso DATETIME2
	, FechaHoraSalida  DATETIME2
)WITH (DATA_COMPRESSION = PAGE)

