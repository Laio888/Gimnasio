CREATE TABLE Usuario (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombre VARCHAR(100) NOT NULL,
    Correo VARCHAR(150) UNIQUE NOT NULL CHECK (Correo LIKE '_%@_%._%'),
    PasswordHash VARCHAR(255) NOT NULL,
    FechaRegistro DATETIME DEFAULT GETDATE(),
    RolId INT NOT NULL
);
GO

INSERT INTO Usuario (Nombre, Correo, PasswordHash, RolId)
VALUES ('Carlos Pérez', 'carlos@gym.com', 'hash123', 2),
       ('Ana Gómez', 'ana@gym.com', 'hash456', 1);
GO

SELECT * FROM Usuario;
GO

CREATE TABLE Rol (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombre VARCHAR(50) NOT NULL,
    Descripcion VARCHAR(200)
);
GO

INSERT INTO Rol (Nombre, Descripcion)
VALUES ('Cliente', 'Acceso básico a rutinas y clases'),
       ('Coach', 'Gestión de rutinas y usuarios');
GO

SELECT * FROM Rol;
GO

CREATE TABLE Permiso (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombre VARCHAR(100) NOT NULL,
    Descripcion VARCHAR(200)
);
GO

INSERT INTO Permiso (Nombre, Descripcion)
VALUES ('CrearRutina', 'Permite crear nuevas rutinas'),
       ('EliminarEjercicio', 'Permite borrar ejercicios');
GO

SELECT * FROM Permiso;
GO

CREATE TABLE RolPermiso (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    RolId INT NOT NULL,
    PermisoId INT NOT NULL
);
GO

INSERT INTO RolPermiso (RolId, PermisoId)
VALUES (1, 1), (2, 2);
GO

SELECT * FROM RolPermiso;
GO

CREATE TABLE Coach (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombre VARCHAR(100) NOT NULL,
    Especialidad VARCHAR(100),
    FotoUrl VARCHAR(255),
    Experiencia INT,
    Correo VARCHAR(150) UNIQUE NOT NULL CHECK (Correo LIKE '_%@_%._%')
);
GO

INSERT INTO Coach (Nombre, Especialidad, FotoUrl, Experiencia, Correo)
VALUES ('Luis Torres', 'Entrenamiento Funcional', '/img/luis.jpg', 5, 'luis@gym.com'),
       ('María López', 'Yoga y Pilates', '/img/maria.jpg', 8, 'maria@gym.com');
GO

SELECT * FROM Coach;
GO

CREATE TABLE Rutina (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombre VARCHAR(100) NOT NULL,
    Descripcion TEXT,
    Nivel VARCHAR(50),
    DuracionMin INT,
    UsuarioId INT NOT NULL
);
GO

INSERT INTO Rutina (Nombre, Descripcion, Nivel, DuracionMin, UsuarioId)
VALUES ('Rutina Fuerza', 'Pesas y resistencia', 'Intermedio', 60, 1),
       ('Rutina Cardio', 'Ejercicios HIIT', 'Avanzado', 45, 2);
GO

SELECT * FROM Rutina;
GO

CREATE TABLE Ejercicio (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombre VARCHAR(100) NOT NULL,
    GrupoMuscular VARCHAR(100),
    Dificultad VARCHAR(50),
    ImagenUrl VARCHAR(255),
    Instrucciones TEXT
);
GO

INSERT INTO Ejercicio (Nombre, GrupoMuscular, Dificultad, ImagenUrl, Instrucciones)
VALUES ('Sentadillas', 'Piernas', 'Intermedio', '/img/sentadillas.jpg', 'Mantener espalda recta'),
       ('Press Banca', 'Pectorales', 'Avanzado', '/img/press.jpg', 'Usar peso adecuado');
GO

SELECT * FROM Ejercicio;
GO

CREATE TABLE EjercicioRutina (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    RutinaId INT NOT NULL,
    EjercicioId INT NOT NULL,
    Series INT,
    Repeticiones INT,
    Orden INT
);
GO

INSERT INTO EjercicioRutina (RutinaId, EjercicioId, Series, Repeticiones, Orden)
VALUES (1, 1, 4, 12, 1), (1, 2, 3, 10, 2);
GO

SELECT * FROM EjercicioRutina;
GO

CREATE TABLE AsignacionRutina (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    UsuarioId INT NOT NULL,
    RutinaId INT NOT NULL,
    FechaAsignacion DATETIME DEFAULT GETDATE(),
    Estado VARCHAR(50)
);
GO

INSERT INTO AsignacionRutina (UsuarioId, RutinaId, Estado)
VALUES (1, 1, 'Activa'), (2, 2, 'Pendiente');
GO

SELECT * FROM AsignacionRutina;
GO

CREATE TABLE Horario (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    DiaSemana VARCHAR(20),
    HoraInicio TIME,
    HoraFin TIME,
    CoachId INT
);
GO

INSERT INTO Horario (DiaSemana, HoraInicio, HoraFin, CoachId)
VALUES ('Lunes', '08:00', '09:00', 1),
       ('Martes', '18:00', '19:00', 2);
GO

SELECT * FROM Horario;
GO

CREATE TABLE Clase (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombre VARCHAR(100),
    Duracion INT,
    Cupos INT,
    CoachId INT,
    HorarioId INT
);
GO

INSERT INTO Clase (Nombre, Duracion, Cupos, CoachId, HorarioId)
VALUES ('Yoga', 60, 15, 2, 2),
       ('Crossfit', 45, 20, 1, 1);
GO

SELECT * FROM Clase;
GO

CREATE TABLE ReservaClase (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    UsuarioId INT,
    ClaseId INT,
    Estado VARCHAR(50),
    FechaReserva DATETIME DEFAULT GETDATE()
);
GO

INSERT INTO ReservaClase (UsuarioId, ClaseId, Estado)
VALUES (1, 1, 'Confirmada'), (2, 2, 'Pendiente');
GO

SELECT * FROM ReservaClase;
GO

CREATE TABLE Mensaje (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    UsuarioId INT,
    CoachId INT,
    Contenido TEXT,
    Fecha DATETIME DEFAULT GETDATE(),
    Estado VARCHAR(50)
);
GO

INSERT INTO Mensaje (UsuarioId, CoachId, Contenido, Estado)
VALUES (1, 1, 'Hola Coach, necesito ayuda con mi rutina', 'Leído'),
       (2, 2, '¿Cuándo es la próxima clase de yoga?', 'Pendiente');
GO

SELECT * FROM Mensaje;
GO

CREATE TABLE Notificacion (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    UsuarioId INT,
    Texto TEXT,
    Fecha DATETIME DEFAULT GETDATE(),
    Tipo VARCHAR(50),
    Estado VARCHAR(50)
);
GO

INSERT INTO Notificacion (UsuarioId, Texto, Tipo, Estado)
VALUES (1, 'Nueva rutina asignada', 'Rutina', 'Activa'),
       (2, 'Clase de Crossfit confirmada', 'Clase', 'Leída');
GO

SELECT * FROM Notificacion;
GO

CREATE TABLE Pago (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    UsuarioId INT,
    Monto DECIMAL(10,2),
    Fecha DATETIME DEFAULT GETDATE(),
    Metodo VARCHAR(50),
    Estado VARCHAR(50)
);
GO

INSERT INTO Pago (UsuarioId, Monto, Metodo, Estado)
VALUES (1, 100.00, 'Tarjeta', 'Aprobado'),
       (2, 80.00, 'Efectivo', 'Pendiente');
GO

SELECT * FROM Pago;
GO

CREATE TABLE Auditoria (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    UsuarioId INT,
    Accion VARCHAR(100),
    Entidad VARCHAR(100),
    EntidadId INT,
    Fecha DATETIME DEFAULT GETDATE(),
    Detalle TEXT
);
GO

INSERT INTO Auditoria (UsuarioId, Accion, Entidad, EntidadId, Detalle)
VALUES (1, 'INSERT', 'Rutina', 1, 'Usuario creó rutina de fuerza'),
       (2, 'DELETE', 'Ejercicio', 2, 'Usuario eliminó Press Banca');
GO

SELECT * FROM Auditoria;
GO