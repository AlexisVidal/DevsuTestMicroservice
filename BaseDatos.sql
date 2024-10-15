CREATE DATABASE DevsuTest
GO
USE DevsuTest
GO
CREATE TABLE Persona (
    PersonaId INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(100),
    Genero NVARCHAR(100),
    Edad INT,
    Identificacion NVARCHAR(20) NOT NULL,
    Direccion NVARCHAR(MAX),
    Telefono NVARCHAR(20), 
	UNIQUE(Identificacion)
);

CREATE TABLE Cliente (
    ClienteId INT PRIMARY KEY IDENTITY(1,1),
    PersonaId INT NOT NULL,
    Contrasena NVARCHAR(100) NOT NULL,
    Estado BIT,
    FOREIGN KEY (PersonaId) REFERENCES Persona(PersonaId),
    UNIQUE (PersonaId)
);

CREATE TABLE Cuenta (
    CuentaId INT PRIMARY KEY IDENTITY(1,1),
    NumeroCuenta NVARCHAR(20) UNIQUE NOT NULL,
    PersonaId INT,  -- Referencia a Persona, no a Cliente directamente
    TipoCuenta NVARCHAR(100),
    SaldoInicial DECIMAL(18,2),
    Estado BIT,
    FOREIGN KEY (PersonaId) REFERENCES Persona(PersonaId)
);

CREATE TABLE Movimiento (
    MovimientoId INT PRIMARY KEY IDENTITY(1,1),
    CuentaId INT,
    Fecha DATETIME2(7),
    TipoMovimiento NVARCHAR(50),
    Valor DECIMAL(18,2),
    Saldo DECIMAL(18,2),
    UNIQUE (CuentaId, Fecha),
    FOREIGN KEY (CuentaId) REFERENCES Cuenta(CuentaId)
);
