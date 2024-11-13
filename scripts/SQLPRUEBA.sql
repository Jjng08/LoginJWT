create table usuario(
	idusuario int  primary key identity,
	nombre varchar(50),
	correo varchar(50),
	clave varchar(50)
	)

create table productos(
	idproducto int primary key identity,
	nombre varchar(50),
	marca varchar(50),
	precio decimal(10,2)
	)

insert into productos(nombre,marca,precio)
values('Laptop Gamer', 'ROG', 94000)

select * from productos;