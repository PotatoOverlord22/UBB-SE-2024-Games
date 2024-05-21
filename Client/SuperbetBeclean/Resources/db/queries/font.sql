-- Create 
CREATE OR ALTER PROCEDURE createFont
    @font_name VARCHAR(255),
    @font_price INT,
    @font_type VARCHAR(255)
AS
BEGIN
    INSERT INTO Font
        (font_name, font_price, font_type)
    VALUES
        (@font_name, @font_price, @font_type)
END
GO

-- Read
CREATE OR ALTER PROCEDURE getFont
    @font_name VARCHAR(255)
AS
BEGIN
    SELECT *
    FROM Font
    WHERE font_name = @font_name
END
GO

CREATE OR ALTER PROCEDURE getFontByID
    @font_id INT
AS
BEGIN
    SELECT *
    FROM Font
    WHERE font_id = @font_id
END
GO

-- Read All
CREATE OR ALTER PROCEDURE getAllFonts
AS
BEGIN
    SELECT *
    FROM Font
END
GO

-- Update
CREATE OR ALTER PROCEDURE updateFont
    @font_id INT,
    @font_name VARCHAR(255),
    @font_price INT,
    @font_type VARCHAR(255)
AS
BEGIN
    UPDATE Font
    SET 
        font_name = @font_name,
        font_price = @font_price,
        font_type = @font_type
    WHERE 
        font_id = @font_id
END
GO

-- Delete
CREATE OR ALTER PROCEDURE deleteFont
    @font_name VARCHAR(255)
AS
BEGIN
    DELETE FROM Font WHERE font_name = @font_name
END
GO

CREATE OR ALTER PROCEDURE deleteFontById
    @font_id INT
AS
BEGIN
    DELETE FROM Font WHERE font_id = @font_id
END
GO


