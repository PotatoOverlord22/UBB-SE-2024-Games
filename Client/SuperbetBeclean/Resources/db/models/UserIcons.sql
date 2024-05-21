create table UserIcons(
	userIcons_id INT IDENTITY(1,1) unique,
	user_id int references Users(user_id),
	icon_id int references Icon(icon_id),
	Primary Key(user_id, icon_id)
)