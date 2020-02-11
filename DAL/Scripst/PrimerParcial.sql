create database PrimerParcial
use PrimerParcial

create table Articulos
(
	ArticuloID int identity,
	Descripcion varchar(100),
	Existencia int,
	Costo decimal,
	ValorInventario decimal
)
select * from Articulos