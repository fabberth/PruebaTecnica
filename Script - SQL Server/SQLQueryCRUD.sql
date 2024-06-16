------------------------------------------------
--
--				  INSERT
--
------------------------------------------------
-- Insertar en la tabla Autores
INSERT INTO Autores (Id, Nombre, FechaNacimiento) 
VALUES (1, 'Gabriel Garcia Marquez', '1999-09-10');

INSERT INTO Autores (Id, Nombre, FechaNacimiento) 
VALUES (2, 'Miguel de Cervantes', '1999-06-15');

INSERT INTO Autores (Id, Nombre, FechaNacimiento) 
VALUES (3, 'Carlos Ruiz Zafón', '1964-09-25');

INSERT INTO Autores (Id, Nombre, FechaNacimiento) 
VALUES (4, 'George Orwell', '1903-06-25');

INSERT INTO Autores (Id, Nombre, FechaNacimiento) 
VALUES (5, 'Antoine de Saint-Exupéry', '1900-06-29');


-- Insertar en la tabla Libros
INSERT INTO Libros (Id, AutorId,Titulo, FechaPublicacion, Codigo) 
VALUES (1, 1,'Cien Años de Soledad', '2015-06-15', '2024-06-00001');

INSERT INTO Libros (Id, AutorId, Titulo, FechaPublicacion, Codigo)
VALUES (2, 2,'Don Quijote de la Mancha', '2017-06-15', '2024-06-00002');

INSERT INTO Libros (Id, AutorId, Titulo, FechaPublicacion, Codigo) 
VALUES (3, 3, 'La Sombra del Viento', '2001-04-12', '2024-06-00003');

INSERT INTO Libros (Id, AutorId, Titulo, FechaPublicacion, Codigo) 
VALUES (4, 4, '1984', '1949-06-08', '2024-06-00004');

INSERT INTO Libros (Id, AutorId, Titulo, FechaPublicacion, Codigo) 
VALUES (5, 5, 'El Principito', '1943-04-06', '2024-06-00005');

INSERT INTO Libros (Id, AutorId,Titulo, FechaPublicacion, Codigo) 
VALUES (6, 1,'Hola Mundo', '2015-06-15', '2024-06-00006');

INSERT INTO Libros (Id, AutorId,Titulo, FechaPublicacion, Codigo) 
VALUES (7, 1,'NN', '2015-06-15', '2024-06-00007');

-- Insertar en la tabla LibrosAutores
INSERT INTO LibrosAutores (LibroId, AutorId) 
VALUES (1, 1);
INSERT INTO LibrosAutores (LibroId, AutorId) 
VALUES (6, 1);

INSERT INTO LibrosAutores (LibroId, AutorId) 
VALUES (2, 2);

INSERT INTO LibrosAutores (LibroId, AutorId) 
VALUES (3, 3);

INSERT INTO LibrosAutores (LibroId, AutorId) 
VALUES (4, 4);

INSERT INTO LibrosAutores (LibroId, AutorId) 
VALUES (5, 5);


------------------------------------------------
--
--				  SELECT
--
------------------------------------------------
-- INNER JOIN
SELECT L.Titulo, A.Nombre AS Autor
FROM Libros L
INNER JOIN Autores A ON L.AutorId = A.Id;

SELECT L.Titulo, A.Nombre AS Autor, LA.FechaAsociacion
FROM Libros L
INNER JOIN LibrosAutores LA ON L.Id = LA.LibroId
INNER JOIN Autores A ON LA.AutorId = A.Id;

---------------------------------------------------
-- LEFT JOIN
SELECT L.Titulo, A.Nombre AS Autor
FROM Libros L
LEFT JOIN Autores A ON L.AutorId = A.Id;

SELECT A.Nombre AS Autor, L.Titulo, L.Codigo, LA.FechaAsociacion
FROM Autores A
LEFT JOIN LibrosAutores LA ON A.Id = LA.AutorId
LEFT JOIN Libros L ON LA.LibroId = L.Id;

---------------------------------------------------
-- UNION
SELECT Titulo, 'Publicado antes del 2000' AS Publicacion, FechaPublicacion
FROM Libros
WHERE FechaPublicacion < '2000-01-01'
UNION
SELECT Titulo, 'Publicado después del 2000' AS Publicacion, FechaPublicacion
FROM Libros
WHERE FechaPublicacion >= '2000-01-01';


SELECT Nombre, 'Nacido antes de 1964-09-20' AS Epoca
FROM Autores
WHERE FechaNacimiento < '1964-09-20'
UNION
SELECT Nombre, 'Nacido después de 1964-09-20' AS Epoca
FROM Autores
WHERE FechaNacimiento >= '1964-09-20';

--------------------------------------------------
-- CASE
SELECT Titulo,FechaPublicacion,
       CASE
           WHEN FechaPublicacion < '1900-01-01' THEN 'Publicación muy antigua'
           WHEN FechaPublicacion BETWEEN '1900-01-01' AND '1950-12-31' THEN 'Publicación antigua'
           WHEN FechaPublicacion BETWEEN '1951-01-01' AND '2000-12-31' THEN 'Publicación reciente'
           ELSE 'Publicación moderna'
       END AS CategoriaPublicacion
FROM Libros;

SELECT Nombre,
       CASE
           WHEN FechaNacimiento < '1800-01-01' THEN 'Nacido en el siglo XVIII o antes'
           WHEN FechaNacimiento BETWEEN '1800-01-01' AND '1900-12-31' THEN 'Nacido en el siglo XIX'
           WHEN FechaNacimiento BETWEEN '1901-01-01' AND '2000-12-31' THEN 'Nacido en el siglo XX'
           ELSE 'Nacido en el siglo XXI'
       END AS EpocaNacimiento
FROM Autores;

------------------------------------------------
--
--				  UPDATE
--
------------------------------------------------

UPDATE Libros SET Titulo = 'Hola Mundo (Editado)' WHERE Titulo = 'Hola Mundo'


------------------------------------------------
--
--				  DELETE
--
------------------------------------------------

DELETE Libros WHERE Titulo = 'NN'