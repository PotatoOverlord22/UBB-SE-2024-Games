create table UserTitles(
	userTitles_id INT IDENTITY(1,1) unique,
	user_id int references Users(user_id),
	title_id int references Title(title_id),
	Primary Key(user_id, title_id)
)