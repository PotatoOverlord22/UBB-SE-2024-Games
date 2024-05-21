create table Font(
	font_id INT IDENTITY(1,1) PRIMARY KEY,
	font_name varchar(255) unique,
	font_price int,
	font_type varchar(255) unique
)