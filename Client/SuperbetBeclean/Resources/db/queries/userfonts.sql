-- Create
CREATE OR ALTER PROCEDURE createUserFont
    @user_id INT,
    @font_id INT
AS
BEGIN
    INSERT INTO UserFonts
        (user_id, font_id)
    VALUES
        (@user_id, @font_id)
END
GO

-- Read
CREATE OR ALTER PROCEDURE getAllUserFonts
AS
BEGIN
    SELECT *
    FROM UserFonts
END
GO

-- This one is used to all the relationships between the User and the Font
CREATE OR ALTER PROCEDURE getAllUserFontsByUserId
    @user_id INT
AS
BEGIN
    SELECT *
    FROM UserFonts
    WHERE user_id = @user_id
END
GO

-- These ones are used to get the names of the fonts that the user has
CREATE OR ALTER PROCEDURE getAllFontNamesByUserId
    @user_id INT
AS
BEGIN
    SELECT F.font_name
    FROM UserFonts UF
        INNER JOIN Font F ON UF.font_id = F.font_id
    WHERE UF.user_id = @user_id
END
GO
-- This will return all the font names that a user has by username
CREATE OR ALTER PROCEDURE getAllFontNamesByUser
    @user_username INT
AS
BEGIN
    SELECT F.font_name
    FROM (
        SELECT user_id
        FROM Users
        WHERE user_username = @user_username
    ) AS U
        INNER JOIN UserFonts UF ON U.user_id = UF.user_id
        INNER JOIN Font F ON UF.font_id = F.font_id
END