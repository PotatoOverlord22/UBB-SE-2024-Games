create table UserFonts(
	userFonts_id INT IDENTITY(1,1) unique,
	user_id int references Users(user_id),
	font_id int references Font(font_id),
	Primary Key(user_id, font_id)
)