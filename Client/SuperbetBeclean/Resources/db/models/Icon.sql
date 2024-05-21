create table Icon(
	icon_id INT IDENTITY(1,1) PRIMARY KEY,
	icon_name varchar(255) unique,
	icon_price int,
	icon_path varchar(255) unique
)