/*
Archivo de consulta de Microsoft SQL, para la creación del
rol Admin en la Base de Datos AWGestionDB, dentro de la tabla AspNetRoles,
y asignación de este Rol 

Abrir este archivo con Microsoft SQL Server Management Studio,
conectar con el servicio de bases de datos destino,
dar Ctrl + A para seleccionar todo el contenido de este archivo de consulta
y dar clic en Ejecutar (o la tecla F5) para ejecutar los comandos.
*/

USE AWGestionDB;

DECLARE @usuarioCreado VARCHAR(MAX);
SET @usuarioCreado = 'Admin1'; /* Aquí va el nombre de usuario registrado previamente en la Aplicación Web */

INSERT INTO AspNetRoles VALUES ('1', 'Admin', 'ADMIN', '1');
INSERT INTO AspNetUserRoles (UserId, RoleId) 
	SELECT us.Id, rol.Id FROM AspNetUsers us, AspNetRoles rol 
		WHERE us.UserName LIKE 'Admin1'
		AND rol.Name LIKE 'Admin';
