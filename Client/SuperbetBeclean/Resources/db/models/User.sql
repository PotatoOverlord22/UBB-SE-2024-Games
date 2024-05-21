create table Users
(
	user_id INT IDENTITY(1,1) PRIMARY KEY,
	user_username VARCHAR(128) NOT NULL UNIQUE,
	user_currentFont int foreign key references Font(font_id),
	user_currentTitle int foreign key references Title(title_id),
	user_currentIcon int foreign key references Icon(icon_id),
	user_currentTable int foreign key references PokerTable(table_id),
	user_chips int,
	user_stack int,
	user_streak int,
	user_handsPlayed int,
	user_level int,
	user_lastLogin DATETIME
)
