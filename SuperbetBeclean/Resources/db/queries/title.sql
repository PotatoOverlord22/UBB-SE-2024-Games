CREATE OR ALTER PROCEDURE createTitle
    @title_name VARCHAR(255),
    @title_price INT,
    @title_content VARCHAR(255)
AS
BEGIN
    INSERT INTO Title
        (title_name, title_price, title_content)
    VALUES
        (@title_name, @title_price, @title_content)
END
GO

CREATE OR ALTER PROCEDURE getTitle
    @title_name VARCHAR(255)
AS
BEGIN
    SELECT *
    FROM Title
    WHERE title_name = @title_name
END
GO

CREATE OR ALTER PROCEDURE getTitleById
    @title_id INT
AS
BEGIN
    SELECT *
    FROM Title
    WHERE title_id = @title_id
END
GO

CREATE OR ALTER PROCEDURE updateTitle
    @title_id INT,
    @title_name VARCHAR(255),
    @title_price INT,
    @title_content VARCHAR(255)
AS
BEGIN
    UPDATE Title
    SET 
        title_name = @title_name,
        title_price = @title_price,
        title_content = @title_content
    WHERE 
        title_id = @title_id
END
GO

CREATE OR ALTER PROCEDURE deleteTitleById
    @title_id INT
AS
BEGIN
    DELETE FROM Title WHERE title_id = @title_id
END
GO

CREATE OR ALTER PROCEDURE deleteTitle
    @title_name VARCHAR(255)
AS
BEGIN
    DELETE FROM Title WHERE title_name = @title_name
END