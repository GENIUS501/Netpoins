CREATE TABLE [dbo].Enlaces
(
  IdEnlace INT NOT NULL IDENTITY(1,1) 
	        CONSTRAINT PK_Enlaces PRIMARY KEY CLUSTERED (IdEnlace)
	, IdOficina INT NOT NULL 
		CONSTRAINT FK_Enlaces_Oficinas FOREIGN KEY(IdOficina) REFERENCES dbo.Oficinas(IdOficina)
    , IdRed INT NOT NULL 
		CONSTRAINT FK_Enlaces_Redes FOREIGN KEY(IdRed) REFERENCES dbo.Redes(IdRed)
    , IdProveedor INT NOT NULL 
		CONSTRAINT FK_Enlaces_Proveedores FOREIGN KEY(IdProveedor) REFERENCES dbo.Proveedores(IdProveedor)
	, Comentario VARCHAR(500) NULL 
)WITH (DATA_COMPRESSION = PAGE)
