USE [master]
GO
/****** Object:  Database [DZamoraProgramacionNCapas]    Script Date: 12/6/2022 1:39:31 PM ******/
CREATE DATABASE [DZamoraProgramacionNCapas]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'DZamoraProgramacionNCapas', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\DZamoraProgramacionNCapas.mdf' , SIZE = 4160KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'DZamoraProgramacionNCapas_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\DZamoraProgramacionNCapas_log.ldf' , SIZE = 1344KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [DZamoraProgramacionNCapas] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [DZamoraProgramacionNCapas].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [DZamoraProgramacionNCapas] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [DZamoraProgramacionNCapas] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [DZamoraProgramacionNCapas] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [DZamoraProgramacionNCapas] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [DZamoraProgramacionNCapas] SET ARITHABORT OFF 
GO
ALTER DATABASE [DZamoraProgramacionNCapas] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [DZamoraProgramacionNCapas] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [DZamoraProgramacionNCapas] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [DZamoraProgramacionNCapas] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [DZamoraProgramacionNCapas] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [DZamoraProgramacionNCapas] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [DZamoraProgramacionNCapas] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [DZamoraProgramacionNCapas] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [DZamoraProgramacionNCapas] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [DZamoraProgramacionNCapas] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [DZamoraProgramacionNCapas] SET  ENABLE_BROKER 
GO
ALTER DATABASE [DZamoraProgramacionNCapas] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [DZamoraProgramacionNCapas] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [DZamoraProgramacionNCapas] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [DZamoraProgramacionNCapas] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [DZamoraProgramacionNCapas] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [DZamoraProgramacionNCapas] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [DZamoraProgramacionNCapas] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [DZamoraProgramacionNCapas] SET RECOVERY FULL 
GO
ALTER DATABASE [DZamoraProgramacionNCapas] SET  MULTI_USER 
GO
ALTER DATABASE [DZamoraProgramacionNCapas] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [DZamoraProgramacionNCapas] SET DB_CHAINING OFF 
GO
ALTER DATABASE [DZamoraProgramacionNCapas] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [DZamoraProgramacionNCapas] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
EXEC sys.sp_db_vardecimal_storage_format N'DZamoraProgramacionNCapas', N'ON'
GO
USE [DZamoraProgramacionNCapas]
GO
/****** Object:  StoredProcedure [dbo].[AseguradoraAdd]    Script Date: 12/6/2022 1:39:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AseguradoraAdd]
@Nombre VARCHAR(50),
@IdUsuario INT
AS
INSERT INTO Aseguradora (Nombre, FechaCreacion, FechaModificacion, IdUsuario) VALUES (@Nombre, GETDATE(), GETDATE(), @IdUsuario);

GO
/****** Object:  StoredProcedure [dbo].[AseguradoraDelete]    Script Date: 12/6/2022 1:39:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AseguradoraDelete]
@IdAseguradora INT
AS 
DELETE FROM Aseguradora WHERE IdAseguradora = @IdAseguradora;
GO
/****** Object:  StoredProcedure [dbo].[AseguradoraGetAll]    Script Date: 12/6/2022 1:39:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AseguradoraGetAll]
AS
SELECT IdAseguradora, Aseguradora.Nombre, FechaCreacion, FechaModificacion, Aseguradora.IdUsuario, Usuario.Nombre AS 'NombreUsuario' FROM Aseguradora INNER JOIN Usuario ON Aseguradora.IdUsuario = Usuario.IdUsuario

GO
/****** Object:  StoredProcedure [dbo].[AseguradoraGetById]    Script Date: 12/6/2022 1:39:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AseguradoraGetById]
@IdAseguradora INT
AS
SELECT IdAseguradora, Aseguradora.Nombre, FechaCreacion, FechaModificacion, Aseguradora.IdUsuario, Usuario.Nombre AS 'NombreUsuario' FROM Aseguradora INNER JOIN Usuario ON Aseguradora.IdUsuario = Usuario.IdUsuario WHERE IdAseguradora = @IdAseguradora
GO
/****** Object:  StoredProcedure [dbo].[AseguradoraUpdate]    Script Date: 12/6/2022 1:39:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AseguradoraUpdate]
@IdAseguradora INT,
@Nombre VARCHAR(50),
@IdUsuario INT
AS
UPDATE Aseguradora SET Nombre = @Nombre, FechaModificacion = GETDATE(), IdUsuario = @IdUsuario WHERE IdAseguradora = @IdAseguradora

GO
/****** Object:  StoredProcedure [dbo].[ColoniaGetByIdMunicipio]    Script Date: 12/6/2022 1:39:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ColoniaGetByIdMunicipio]
@IdMunicipio INT
AS 
SELECT IdColonia, Nombre, CodigoPostal, IdMunicipio FROM Colonia WHERE IdMunicipio = @IdMunicipio
GO
/****** Object:  StoredProcedure [dbo].[DependienteAdd]    Script Date: 12/6/2022 1:39:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DependienteAdd]
@NumeroEmpleado VARCHAR(50),
@Nombre VARCHAR(50),
@ApellidoPaterno VARCHAR(50),
@ApellidoMaterno VARCHAR(50),
@FechaDeNacimiento DATE,
@EstadoCivil VARCHAR(50),
@Genero VARCHAR(50),
@Telefono VARCHAR(20),
@RFC VARCHAR(50),
@IdDependienteTipo INT
AS
INSERT INTO Dependiente (NumeroEmpleado, Nombre, ApellidoPaterno, ApellidoMaterno, FechaDeNacimiento, EstadoCivil, Genero, Telefono, RFC, IdDependienteTipo) VALUES (@NumeroEmpleado, @Nombre, @ApellidoPaterno, @ApellidoMaterno, @FechaDeNacimiento, @EstadoCivil, @Genero, @Telefono, @RFC, @IdDependienteTipo)

GO
/****** Object:  StoredProcedure [dbo].[DependienteDelete]    Script Date: 12/6/2022 1:39:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DependienteDelete]
@IdDependiente INT
AS DELETE FROM Dependiente WHERE IdDependiente = @IdDependiente

GO
/****** Object:  StoredProcedure [dbo].[DependienteGetAll]    Script Date: 12/6/2022 1:39:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DependienteGetAll]
AS
SELECT IdDependiente, NumeroEmpleado, Dependiente.Nombre, ApellidoPaterno, ApellidoMaterno, FechaDeNacimiento, EstadoCivil, Genero, Telefono, RFC, 
Dependiente.IdDependienteTipo, DependienteTipo.Nombre AS 'TipoDependiente' FROM Dependiente INNER JOIN DependienteTipo ON Dependiente.IdDependienteTipo = DependienteTipo.IdDependienteTipo

GO
/****** Object:  StoredProcedure [dbo].[DependienteGetById]    Script Date: 12/6/2022 1:39:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DependienteGetById]
@NumeroDeEmpleado VARCHAR(50)
AS
SELECT IdDependiente, NumeroEmpleado, Dependiente.Nombre, ApellidoPaterno, ApellidoMaterno, FechaDeNacimiento, EstadoCivil, Genero, Telefono, RFC, 
Dependiente.IdDependienteTipo, DependienteTipo.Nombre AS 'TipoDependiente' FROM Dependiente INNER JOIN DependienteTipo ON Dependiente.IdDependienteTipo = DependienteTipo.IdDependienteTipo WHERE NumeroEmpleado = @NumeroDeEmpleado

GO
/****** Object:  StoredProcedure [dbo].[DependienteTipoGetAll]    Script Date: 12/6/2022 1:39:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DependienteTipoGetAll]
AS
SELECT IdDependienteTipo, Nombre FROM DependienteTipo

GO
/****** Object:  StoredProcedure [dbo].[DependienteUpdate]    Script Date: 12/6/2022 1:39:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DependienteUpdate]
@IdDependiente INT,
@NumeroEmpleado VARCHAR(50),
@Nombre VARCHAR(50),
@ApellidoPaterno VARCHAR(50),
@ApellidoMaterno VARCHAR(50),
@FechaDeNacimiento DATE,
@EstadoCivil VARCHAR(50),
@Genero VARCHAR(50),
@Telefono VARCHAR(20),
@RFC VARCHAR(50),
@IdDependienteTipo INT
AS
UPDATE Dependiente SET NumeroEmpleado = @NumeroEmpleado, Nombre = @Nombre, ApellidoPaterno = @ApellidoPaterno, ApellidoMaterno = @ApellidoMaterno, FechaDeNacimiento = @FechaDeNacimiento, EstadoCivil = @EstadoCivil, Genero = @Genero, Telefono = @Telefono, RFC = @RFC, IdDependienteTipo = @IdDependienteTipo WHERE IdDependiente = @IdDependiente

GO
/****** Object:  StoredProcedure [dbo].[EmpleadoAdd]    Script Date: 12/6/2022 1:39:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[EmpleadoAdd]
@NumeroEmpleado VARCHAR(50),
@RFC VARCHAR(50),
@Nombre VARCHAR(50),
@ApellidoPaterno VARCHAR(50),
@ApellidoMaterno VARCHAR(50),
@Email VARCHAR(254),
@Telefono VARCHAR(20),
@FechaDeNacimiento DATE,
@NSS VARCHAR(20),
@FechaIngreso DATE,
@Foto VARCHAR(MAX),
@IdEmpresa INT
AS
INSERT INTO Empleado (NumeroEmpleado, RFC, Nombre, ApellidoPaterno, ApellidoMaterno, Email, Telefono, FechaDeNacimiento, NSS, FechaIngreso, Foto, IdEmpresa) VALUES (@NumeroEmpleado, @RFC, @Nombre, @ApellidoPaterno, @ApellidoMaterno, @Email, @Telefono, @FechaDeNacimiento, @NSS, @FechaIngreso, @Foto, @IdEmpresa)

GO
/****** Object:  StoredProcedure [dbo].[EmpleadoDelete]    Script Date: 12/6/2022 1:39:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[EmpleadoDelete]
@NumeroEmpleado VARCHAR(50)
AS
DELETE FROM Empleado WHERE NumeroEmpleado = @NumeroEmpleado

GO
/****** Object:  StoredProcedure [dbo].[EmpleadoGetAll]    Script Date: 12/6/2022 1:39:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[EmpleadoGetAll]
AS
SELECT NumeroEmpleado, RFC, Empleado.Nombre, ApellidoPaterno, ApellidoMaterno, Empleado.Email, Empleado.Telefono, FechaDeNacimiento, NSS, FechaIngreso, Foto, Empleado.IdEmpresa, Empresa.Nombre AS 'NombreEmpresa' FROM Empleado INNER JOIN Empresa ON Empleado.IdEmpresa = Empresa.IdEmpresa

GO
/****** Object:  StoredProcedure [dbo].[EmpleadoGetById]    Script Date: 12/6/2022 1:39:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[EmpleadoGetById]
@NumeroEmpleado VARCHAR(50)
AS
SELECT NumeroEmpleado, RFC, Empleado.Nombre, ApellidoPaterno, ApellidoMaterno, Empleado.Email, Empleado.Telefono, FechaDeNacimiento, NSS, FechaIngreso, Foto, Empleado.IdEmpresa, Empresa.Nombre AS 'NombreEmpresa' FROM Empleado INNER JOIN Empresa ON Empleado.IdEmpresa = Empresa.IdEmpresa WHERE NumeroEmpleado = @NumeroEmpleado

GO
/****** Object:  StoredProcedure [dbo].[EmpleadoUpdate]    Script Date: 12/6/2022 1:39:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[EmpleadoUpdate]
@NumeroEmpleado VARCHAR(50),
@RFC VARCHAR(50),
@Nombre VARCHAR(50),
@ApellidoPaterno VARCHAR(50),
@ApellidoMaterno VARCHAR(50),
@Email VARCHAR(254),
@Telefono VARCHAR(20),
@FechaDeNacimiento DATE,
@NSS VARCHAR(20),
@FechaIngreso DATE,
@Foto VARCHAR(MAX),
@IdEmpresa INT
AS
UPDATE Empleado SET NumeroEmpleado = @NumeroEmpleado, RFC = @RFC, Nombre = @Nombre, ApellidoPaterno = @ApellidoPaterno, ApellidoMaterno = @ApellidoMaterno, Email = @Email, Telefono = @Telefono, FechaDeNacimiento = @FechaDeNacimiento, NSS = @NSS, FechaIngreso = @FechaIngreso, Foto = @Foto, IdEmpresa = @IdEmpresa WHERE NumeroEmpleado = @NumeroEmpleado

GO
/****** Object:  StoredProcedure [dbo].[EmpresaAdd]    Script Date: 12/6/2022 1:39:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[EmpresaAdd]
@Nombre VARCHAR(50),
@Telefono VARCHAR(50),
@Email VARCHAR(254),
@DireccionWeb VARCHAR(100),
@Logo VARCHAR(MAX)
AS
INSERT INTO Empresa (Nombre, Telefono, Email, DireccionWeb, Logo) VALUES (@Nombre, @Telefono, @Email, @DireccionWeb, @Logo)

GO
/****** Object:  StoredProcedure [dbo].[EmpresaDelete]    Script Date: 12/6/2022 1:39:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[EmpresaDelete]
@IdEmpresa INT
AS
DELETE FROM Empresa WHERE IdEmpresa = @IdEmpresa
GO
/****** Object:  StoredProcedure [dbo].[EmpresaGetAll]    Script Date: 12/6/2022 1:39:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[EmpresaGetAll]
@Nombre VARCHAR(50)
AS
SELECT IdEmpresa, Nombre, Telefono, Email, DireccionWeb, Logo FROM Empresa 
WHERE Nombre LIKE '%' + @Nombre + '%' 
GO
/****** Object:  StoredProcedure [dbo].[EmpresaGetById]    Script Date: 12/6/2022 1:39:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[EmpresaGetById]
@IdEmpresa INT
AS
SELECT IdEmpresa, Nombre, Telefono, Email, DireccionWeb, Logo FROM Empresa WHERE IdEmpresa = @IdEmpresa
GO
/****** Object:  StoredProcedure [dbo].[EmpresaUpdate]    Script Date: 12/6/2022 1:39:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[EmpresaUpdate]
@IdEmpresa INT,
@Nombre VARCHAR(50),
@Telefono VARCHAR(50),
@Email VARCHAR(254),
@DireccionWeb VARCHAR(100),
@Logo VARCHAR(MAX)
AS
UPDATE Empresa SET Nombre = @Nombre, Telefono = @Telefono, Email = @Email, DireccionWeb = @DireccionWeb, Logo = @Logo WHERE IdEmpresa = @IdEmpresa
GO
/****** Object:  StoredProcedure [dbo].[EstadoGetByIdPais]    Script Date: 12/6/2022 1:39:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[EstadoGetByIdPais]
@IdPais INT
AS
SELECT IdEstado, Nombre, IdPais FROM Estado WHERE IdPais = @IdPais

GO
/****** Object:  StoredProcedure [dbo].[MunicipioGetByIdEstado]    Script Date: 12/6/2022 1:39:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[MunicipioGetByIdEstado]
@IdEstado INT
AS
SELECT IdMunicipio, Nombre, IdEstado FROM Municipio WHERE IdEstado = @IdEstado

GO
/****** Object:  StoredProcedure [dbo].[PaisGetAll]    Script Date: 12/6/2022 1:39:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[PaisGetAll]
AS
SELECT IdPais, Nombre FROM Pais

GO
/****** Object:  StoredProcedure [dbo].[RolGetAll]    Script Date: 12/6/2022 1:39:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[RolGetAll]
AS
SELECT IdRol, Rol FROM Rol

GO
/****** Object:  StoredProcedure [dbo].[UsuarioAdd]    Script Date: 12/6/2022 1:39:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UsuarioAdd]
@Nombre VARCHAR(50),
@FechaDeNacimiento DATE, 
@NumeroDeTelefono VARCHAR(20), 
@Email VARCHAR(254), 
@UserName VARCHAR(50), 
@ApellidoPaterno VARCHAR(50), 
@ApellidoMaterno VARCHAR(50), 
@Password VARCHAR (50), 
@Sexo CHAR(2), 
@Celular VARCHAR(20), 
@CURP VARCHAR(50),
@IdRol INT,
@Imagen VARCHAR(MAX),

@Calle VARCHAR(50),
@NumeroInterior VARCHAR(20),
@NumeroExterior VARCHAR(20),
@IdColonia INT

AS
INSERT INTO Usuario (Nombre, FechaDeNacimiento, NumeroDeTelefono, Email, UserName, ApellidoPaterno, ApellidoMaterno, [Password], Sexo, Celular, CURP, IdRol, Imagen, [Status]) VALUES (@Nombre, @FechaDeNacimiento, @NumeroDeTelefono, @Email, @UserName, @ApellidoPaterno, @ApellidoMaterno, @Password, @Sexo, @Celular, @CURP, @IdRol, @Imagen, 1)
INSERT INTO Direccion (Calle, NumeroInterior, NumeroExterior, IdColonia, IdUsuario) VALUES (@Calle, @NumeroInterior, @NumeroExterior, @IdColonia, @@IDENTITY)

GO
/****** Object:  StoredProcedure [dbo].[UsuarioDelete]    Script Date: 12/6/2022 1:39:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UsuarioDelete]
@IdUsuario INT
AS
DELETE FROM Direccion WHERE IdUsuario = @IdUsuario
DELETE FROM Usuario WHERE IdUsuario = @IdUsuario

GO
/****** Object:  StoredProcedure [dbo].[UsuarioGetAll]    Script Date: 12/6/2022 1:39:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UsuarioGetAll]
@Nombre VARCHAR(50),
@ApellidoPaterno VARCHAR(50),
@IdRol INT
AS
IF (@IdRol = 0)
BEGIN
SELECT Usuario.IdUsuario, Usuario.Nombre, ApellidoPaterno, ApellidoMaterno, FechaDeNacimiento, Sexo, CURP, NumeroDeTelefono, Celular, Email, UserName, [Password], Imagen, [Status],
Usuario.IdRol, Rol.Rol AS 'NombreRol',
Direccion.IdDireccion, Direccion.Calle, Direccion.NumeroExterior, Direccion.NumeroInterior,
Colonia.IdColonia, Colonia.CodigoPostal, Colonia.Nombre AS 'NombreColonia',
Municipio.IdMunicipio, Municipio.Nombre AS 'NombreMunicipio',
Estado.IdEstado, Estado.Nombre AS 'NombreEstado',
Pais.IdPais, Pais.Nombre AS 'NombrePais'  FROM Usuario 
INNER JOIN Rol ON Usuario.IdRol = Rol.IdRol
INNER JOIN Direccion ON Usuario.IdUsuario = Direccion.IdUsuario
INNER JOIN Colonia ON Direccion.IdColonia = Colonia.IdColonia
INNER JOIN Municipio ON Colonia.IdMunicipio = Municipio.IdMunicipio
INNER JOIN Estado ON Municipio.IdEstado = Estado.IdEstado
INNER JOIN Pais ON Estado.IdPais = Pais.IdPais
WHERE Usuario.Nombre LIKE '%' + @Nombre + '%' AND Usuario.ApellidoPaterno LIKE '%' + @ApellidoPaterno + '%'
END
ELSE
BEGIN
SELECT Usuario.IdUsuario, Usuario.Nombre, ApellidoPaterno, ApellidoMaterno, FechaDeNacimiento, Sexo, CURP, NumeroDeTelefono, Celular, Email, UserName, [Password], Imagen, [Status],
Usuario.IdRol, Rol.Rol AS 'NombreRol',
Direccion.IdDireccion, Direccion.Calle, Direccion.NumeroExterior, Direccion.NumeroInterior,
Colonia.IdColonia, Colonia.CodigoPostal, Colonia.Nombre AS 'NombreColonia',
Municipio.IdMunicipio, Municipio.Nombre AS 'NombreMunicipio',
Estado.IdEstado, Estado.Nombre AS 'NombreEstado',
Pais.IdPais, Pais.Nombre AS 'NombrePais'  FROM Usuario 
INNER JOIN Rol ON Usuario.IdRol = Rol.IdRol
INNER JOIN Direccion ON Usuario.IdUsuario = Direccion.IdUsuario
INNER JOIN Colonia ON Direccion.IdColonia = Colonia.IdColonia
INNER JOIN Municipio ON Colonia.IdMunicipio = Municipio.IdMunicipio
INNER JOIN Estado ON Municipio.IdEstado = Estado.IdEstado
INNER JOIN Pais ON Estado.IdPais = Pais.IdPais
WHERE Usuario.Nombre LIKE '%' + @Nombre + '%' AND Usuario.ApellidoPaterno LIKE '%' + @ApellidoPaterno + '%' AND Usuario.IdRol = @IdRol
END
GO
/****** Object:  StoredProcedure [dbo].[UsuarioGetById]    Script Date: 12/6/2022 1:39:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UsuarioGetById]
@IdUsuario INT
AS
SELECT Usuario.IdUsuario, Usuario.Nombre, ApellidoPaterno, ApellidoMaterno, FechaDeNacimiento, Sexo, CURP, NumeroDeTelefono, Celular, Email, UserName, [Password], Imagen, [Status],
Usuario.IdRol, Rol.Rol AS 'NombreRol',
Direccion.IdDireccion, Direccion.Calle, Direccion.NumeroExterior, Direccion.NumeroInterior,
Colonia.IdColonia, Colonia.CodigoPostal, Colonia.Nombre AS 'NombreColonia',
Municipio.IdMunicipio, Municipio.Nombre AS 'NombreMunicipio',
Estado.IdEstado, Estado.Nombre AS 'NombreEstado',
Pais.IdPais, Pais.Nombre AS 'NombrePais'  FROM Usuario 
INNER JOIN Rol ON Usuario.IdRol = Rol.IdRol
INNER JOIN Direccion ON Usuario.IdUsuario = Direccion.IdUsuario
INNER JOIN Colonia ON Direccion.IdColonia = Colonia.IdColonia
INNER JOIN Municipio ON Colonia.IdMunicipio = Municipio.IdMunicipio
INNER JOIN Estado ON Municipio.IdEstado = Estado.IdEstado
INNER JOIN Pais ON Estado.IdPais = Pais.IdPais WHERE Usuario.IdUsuario = @IdUsuario
GO
/****** Object:  StoredProcedure [dbo].[UsuarioUpdate]    Script Date: 12/6/2022 1:39:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UsuarioUpdate]
@IdUsuario INT,
@Nombre VARCHAR(50),
@FechaDeNacimiento DATE, 
@NumeroDeTelefono VARCHAR(20), 
@Email VARCHAR(254), 
@UserName VARCHAR(50), 
@ApellidoPaterno VARCHAR(50), 
@ApellidoMaterno VARCHAR(50), 
@Password VARCHAR (50), 
@Sexo CHAR(2), 
@Celular VARCHAR(20), 
@CURP VARCHAR(50),
@IdRol INT,
@Imagen VARCHAR(MAX),

@Calle VARCHAR(50),
@NumeroInterior VARCHAR(20),
@NumeroExterior VARCHAR(20),
@IdColonia INT
AS 
UPDATE Usuario SET Nombre = @Nombre, FechaDeNacimiento = @FechaDeNacimiento, NumeroDeTelefono= @NumeroDeTelefono, Email = @Email, UserName= @UserName, ApellidoPaterno = @ApellidoPaterno, ApellidoMaterno = @ApellidoMaterno, [Password] = @Password, Sexo = @Sexo, Celular = @Celular, CURP = @CURP, IdRol = @IdRol, Imagen = @Imagen WHERE IdUsuario = @IdUsuario
UPDATE Direccion SET Calle = @Calle, NumeroInterior = @NumeroInterior, NumeroExterior = @NumeroExterior, IdColonia = @IdColonia WHERE IdUsuario = @IdUsuario  

GO
/****** Object:  StoredProcedure [dbo].[UsuarioUpdateStatus]    Script Date: 12/6/2022 1:39:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UsuarioUpdateStatus]
@Status BIT,
@IdUsuario INT
AS
UPDATE Usuario SET [Status] = @Status WHERE IdUsuario = @IdUsuario
GO
/****** Object:  Table [dbo].[Aseguradora]    Script Date: 12/6/2022 1:39:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Aseguradora](
	[IdAseguradora] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
	[FechaCreacion] [datetime] NULL,
	[FechaModificacion] [datetime] NULL,
	[IdUsuario] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[IdAseguradora] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Colonia]    Script Date: 12/6/2022 1:39:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Colonia](
	[IdColonia] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
	[CodigoPostal] [varchar](50) NOT NULL,
	[IdMunicipio] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[IdColonia] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Dependiente]    Script Date: 12/6/2022 1:39:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Dependiente](
	[IdDependiente] [int] IDENTITY(1,1) NOT NULL,
	[NumeroEmpleado] [varchar](50) NULL,
	[Nombre] [varchar](50) NOT NULL,
	[ApellidoPaterno] [varchar](50) NOT NULL,
	[ApellidoMaterno] [varchar](50) NULL,
	[FechaDeNacimiento] [date] NULL,
	[EstadoCivil] [varchar](50) NULL,
	[Genero] [varchar](50) NULL,
	[Telefono] [varchar](20) NOT NULL,
	[RFC] [varchar](50) NULL,
	[IdDependienteTipo] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[IdDependiente] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[DependienteTipo]    Script Date: 12/6/2022 1:39:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[DependienteTipo](
	[IdDependienteTipo] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdDependienteTipo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Direccion]    Script Date: 12/6/2022 1:39:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Direccion](
	[IdDireccion] [int] IDENTITY(1,1) NOT NULL,
	[Calle] [varchar](50) NOT NULL,
	[NumeroInterior] [varchar](20) NOT NULL,
	[NumeroExterior] [varchar](20) NOT NULL,
	[IdColonia] [int] NULL,
	[IdUsuario] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[IdDireccion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Empleado]    Script Date: 12/6/2022 1:39:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Empleado](
	[NumeroEmpleado] [varchar](50) NOT NULL,
	[RFC] [varchar](50) NULL,
	[Nombre] [varchar](50) NOT NULL,
	[ApellidoPaterno] [varchar](50) NOT NULL,
	[ApellidoMaterno] [varchar](50) NULL,
	[Email] [varchar](254) NOT NULL,
	[Telefono] [varchar](20) NOT NULL,
	[FechaDeNacimiento] [date] NULL,
	[NSS] [varchar](20) NULL,
	[FechaIngreso] [date] NULL,
	[Foto] [varchar](max) NULL,
	[IdEmpresa] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[NumeroEmpleado] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Empresa]    Script Date: 12/6/2022 1:39:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Empresa](
	[IdEmpresa] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](50) NULL,
	[Telefono] [varchar](50) NULL,
	[Email] [varchar](254) NULL,
	[DireccionWeb] [varchar](100) NULL,
	[Logo] [varchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[IdEmpresa] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Estado]    Script Date: 12/6/2022 1:39:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Estado](
	[IdEstado] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
	[IdPais] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[IdEstado] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Municipio]    Script Date: 12/6/2022 1:39:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Municipio](
	[IdMunicipio] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
	[IdEstado] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[IdMunicipio] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Pais]    Script Date: 12/6/2022 1:39:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Pais](
	[IdPais] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdPais] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Rol]    Script Date: 12/6/2022 1:39:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Rol](
	[IdRol] [int] IDENTITY(1,1) NOT NULL,
	[Rol] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[IdRol] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Usuario]    Script Date: 12/6/2022 1:39:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Usuario](
	[IdUsuario] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
	[FechaDeNacimiento] [date] NOT NULL,
	[NumeroDeTelefono] [varchar](20) NOT NULL,
	[Email] [varchar](254) NOT NULL,
	[UserName] [varchar](50) NOT NULL,
	[ApellidoPaterno] [varchar](50) NOT NULL,
	[ApellidoMaterno] [varchar](50) NULL,
	[Password] [varchar](50) NOT NULL,
	[Sexo] [char](2) NOT NULL,
	[Celular] [varchar](20) NULL,
	[CURP] [varchar](50) NULL,
	[IdRol] [int] NULL,
	[Imagen] [varchar](max) NULL,
	[Status] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdUsuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [Email] UNIQUE NONCLUSTERED 
(
	[IdUsuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[UserName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[Aseguradora]  WITH CHECK ADD FOREIGN KEY([IdUsuario])
REFERENCES [dbo].[Usuario] ([IdUsuario])
GO
ALTER TABLE [dbo].[Colonia]  WITH CHECK ADD FOREIGN KEY([IdMunicipio])
REFERENCES [dbo].[Municipio] ([IdMunicipio])
GO
ALTER TABLE [dbo].[Dependiente]  WITH CHECK ADD FOREIGN KEY([IdDependienteTipo])
REFERENCES [dbo].[DependienteTipo] ([IdDependienteTipo])
GO
ALTER TABLE [dbo].[Dependiente]  WITH CHECK ADD FOREIGN KEY([NumeroEmpleado])
REFERENCES [dbo].[Empleado] ([NumeroEmpleado])
GO
ALTER TABLE [dbo].[Direccion]  WITH CHECK ADD FOREIGN KEY([IdColonia])
REFERENCES [dbo].[Colonia] ([IdColonia])
GO
ALTER TABLE [dbo].[Direccion]  WITH CHECK ADD FOREIGN KEY([IdUsuario])
REFERENCES [dbo].[Usuario] ([IdUsuario])
GO
ALTER TABLE [dbo].[Empleado]  WITH CHECK ADD FOREIGN KEY([IdEmpresa])
REFERENCES [dbo].[Empresa] ([IdEmpresa])
GO
ALTER TABLE [dbo].[Estado]  WITH CHECK ADD FOREIGN KEY([IdPais])
REFERENCES [dbo].[Pais] ([IdPais])
GO
ALTER TABLE [dbo].[Municipio]  WITH CHECK ADD FOREIGN KEY([IdEstado])
REFERENCES [dbo].[Estado] ([IdEstado])
GO
ALTER TABLE [dbo].[Usuario]  WITH CHECK ADD FOREIGN KEY([IdRol])
REFERENCES [dbo].[Rol] ([IdRol])
GO
USE [master]
GO
ALTER DATABASE [DZamoraProgramacionNCapas] SET  READ_WRITE 
GO
