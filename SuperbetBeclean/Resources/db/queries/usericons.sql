CREATE OR ALTER PROCEDURE createUserIcon
    @user_id INT,
    @icon_id INT
AS
BEGIN
    INSERT INTO UserIcons
        (user_id, icon_id)
    VALUES
        (@user_id, @icon_id)
END
GO

CREATE OR ALTER PROCEDURE getAllUserIcons
AS
BEGIN
    SELECT *
    FROM UserIcons
END
GO

CREATE OR ALTER PROCEDURE getAllUserIconsByUserId
    @user_id INT
AS
BEGIN
    SELECT *
    FROM UserIcons
    WHERE user_id = @user_id
END
GO

CREATE OR ALTER PROCEDURE getAllIconNamesByUserId
    @user_id INT
AS
BEGIN
    SELECT I.icon_name
    FROM UserIcons UI
        INNER JOIN Icon I ON UI.icon_id = I.icon_id
    WHERE UI.user_id = @user_id
END
GO


CREATE OR ALTER PROCEDURE getAllIconNamesByUser
    @user_username VARCHAR(255)
AS
BEGIN
    SELECT I.icon_name
    FROM (
        SELECT user_id
        FROM Users
        WHERE user_username = @user_username
    ) AS U
        INNER JOIN UserIcons UI ON U.user_id = UI.user_id
        INNER JOIN Icon I ON UI.icon_id = I.icon_id
END
GO
