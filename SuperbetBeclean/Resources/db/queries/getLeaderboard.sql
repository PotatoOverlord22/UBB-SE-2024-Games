CREATE OR ALTER PROCEDURE getLeaderboard
AS
BEGIN
    SELECT user_username, user_level, user_chips
    FROM Users
    ORDER BY user_chips DESC, user_level DESC, user_username ASC
END
GO
