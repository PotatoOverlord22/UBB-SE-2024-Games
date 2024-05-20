create table PokerTable(
	table_id INT IDENTITY(1,1) PRIMARY KEY,
	table_name varchar(255) unique,
	table_buyIn int,
	table_playerLimit int
)