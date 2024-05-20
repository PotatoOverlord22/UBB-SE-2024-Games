CREATE OR ALTER PROCEDURE createUserTitle
    @user_id INT,
    @title_id INT
AS
BEGIN
    INSERT INTO UserTitles
        (user_id, title_id)
    VALUES
        (@user_id, @title_id)
END
GO

CREATE OR ALTER PROCEDURE getAllUserTitles
AS
BEGIN
    SELECT *
    FROM UserTitles
END
GO

CREATE OR ALTER PROCEDURE getAllUserTitlesByUserID
    @user_id INT
AS
BEGIN
    SELECT *
    FROM UserTitles
    WHERE user_id = @user_id
END
GO

CREATE OR ALTER PROCEDURE getAllTitleNamesByUserID
    @user_id INT
AS
BEGIN
    SELECT T.title_name
    FROM UserTitles UT
        INNER JOIN Title T ON UT.title_id = T.title_id
    WHERE UT.user_id = @user_id
END
GO

CREATE OR ALTER PROCEDURE getAllTitleIdsByUserID
    @user_id INT
AS
BEGIN
    SELECT T.title_id
    FROM UserTitles UT
        INNER JOIN Title T ON UT.title_id = T.title_id
    WHERE UT.user_id = @user_id
END