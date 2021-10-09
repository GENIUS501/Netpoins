CREATE TABLE [dbo].Proveedores
(
	[IdProveedor] INT NOT NULL IDENTITY(1,1) 
	        CONSTRAINT PK_Proveedores PRIMARY KEY CLUSTERED ([IdProveedor])
	, NombreEmpresa VARCHAR(50) NOT NULL
	, Contacto  VARCHAR(50) NOT NULL
	, Telefono VARCHAR (10) NOT NULL
	, Email VARCHAR(50) NOT NULL  
	, Comentario VARCHAR(500) NULL  
)WITH (DATA_COMPRESSION = PAGE)
