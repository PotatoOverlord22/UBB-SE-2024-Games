create table Title(
	title_id INT IDENTITY(1,1) PRIMARY KEY,
	title_name varchar(255) unique,
	title_price int,
	title_content varchar(255) unique
)