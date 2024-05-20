CREATE OR ALTER PROCEDURE createPokerTable
    @table_name VARCHAR(255),
    @table_buyIn INT,
    @table_playerLimit INT
AS
BEGIN
    INSERT INTO PokerTable
        (table_name, table_buyIn, table_playerLimit)
    VALUES
        (@table_name, @table_buyIn, @table_playerLimit)
END