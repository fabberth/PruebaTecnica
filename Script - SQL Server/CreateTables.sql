-- Creación de la tabla de Autores
CREATE TABLE Autores (
    Id INT PRIMARY KEY,
    Nombre NVARCHAR(100) NOT NULL,
    FechaNacimiento DATE
);

-- Creación de la tabla de Libros
CREATE TABLE Libros (
    Id INT PRIMARY KEY,
	AutorId INT,
    Titulo NVARCHAR(100) NOT NULL,
    FechaPublicacion DATE,
    Codigo NVARCHAR(13) UNIQUE,
	CONSTRAINT FK_Libros_Autores FOREIGN KEY (AutorId) REFERENCES Autores(Id)
);

-- Creación de la tabla intermedia para la relación muchos a muchos entre Libros y Autores
CREATE TABLE LibrosAutores (
    LibroId INT NOT NULL,
    AutorId INT NOT NULL,
    FechaAsociacion DATETIME DEFAULT GETDATE(),
    CONSTRAINT PK_LibrosAutores PRIMARY KEY (LibroId, AutorId),
    CONSTRAINT FK_LibrosAutores_Libros FOREIGN KEY (LibroId) REFERENCES Libros(Id),
    CONSTRAINT FK_LibrosAutores_Autores FOREIGN KEY (AutorId) REFERENCES Autores(Id)
);

