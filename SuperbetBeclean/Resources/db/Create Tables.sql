use Team42
go


CREATE OR ALTER PROCEDURE dropAllTables
AS
BEGIN
	drop table if exists Request
	drop table if exists UserFonts
	drop table if exists UserIcons
	drop table if exists UserTitles
	drop table if exists Users
	drop table if exists Font
	drop table if exists Icon
	drop table if exists PokerTable
	drop table if exists Title
	drop table if exists Challenge
END
GO
-- for dropping the tables (due to multi-threading optimization running the procedure will always give a foreign-key error. just run the procedure like 4 times) 
--dropAllTables


create table Font(
	font_id INT IDENTITY(1,1) PRIMARY KEY,
	font_name varchar(255) unique,
	font_price int,
	font_type varchar(255) unique
)
create table Icon(
	icon_id INT IDENTITY(1,1) PRIMARY KEY,
	icon_name varchar(255) unique,
	icon_price int,
	icon_path varchar(255) unique
)
create table Title(
	title_id INT IDENTITY(1,1) PRIMARY KEY,
	title_name varchar(255) unique,
	title_price int,
	title_content varchar(255) unique
)
create table PokerTable(
	table_id INT IDENTITY(1,1) PRIMARY KEY,
	table_name varchar(255) unique,
	table_buyIn int,
	table_playerLimit int
)
create table Challenge(
	challenge_id INT IDENTITY(1,1) PRIMARY KEY,
	challenge_description varchar(MAX),
	challenge_rule varchar(MAX),
	challenge_amount int,
	challenge_reward int
)
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
create table Request(
	request_id INT IDENTITY(1,1) PRIMARY KEY,
	request_fromUser int foreign key references Users(user_id),
	request_toUser int foreign key references Users(user_id),
	request_date DATE,
	CONSTRAINT AK_Requests_CandidateKey UNIQUE(request_fromUser, request_toUser, request_date)
)
create table UserFonts(
	userFonts_id INT IDENTITY(1,1) unique,
	user_id int references Users(user_id),
	font_id int references Font(font_id),
	Primary Key(user_id, font_id)
)
create table UserIcons(
	userIcons_id INT IDENTITY(1,1) unique,
	user_id int references Users(user_id),
	icon_id int references Icon(icon_id),
	Primary Key(user_id, icon_id)
)
create table UserTitles(
	userTitles_id INT IDENTITY(1,1) unique,
	user_id int references Users(user_id),
	title_id int references Title(title_id),
	Primary Key(user_id, title_id)
)