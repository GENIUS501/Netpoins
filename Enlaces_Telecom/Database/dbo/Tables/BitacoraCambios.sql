CREATE TABLE BitacoraCambios
(
    IdCambio INT NOT NULL IDENTITY(1,1) 
	        CONSTRAINT PK_BitacoraCambios PRIMARY KEY CLUSTERED (IdCambio)
	, IdUsuario INT NOT NULL 
		CONSTRAINT FK_BitacoraCambios_Usuarios FOREIGN KEY(IdUsuario) REFERENCES dbo.Usuarios(IdUsuario)
	, FechaHora DATETIME2
	, Tipo VARCHAR(100) NOT NULL
	, Detalle VARCHAR(500) NOT NULL
)WITH (DATA_COMPRESSION = PAGE)