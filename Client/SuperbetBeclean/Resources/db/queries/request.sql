CREATE OR ALTER PROCEDURE createRequest
    @fromUser INT,
    @toUser INT,
    @date DATE
AS
BEGIN
    INSERT INTO Request
        (request_fromUser, request_toUser, request_date)
    VALUES
        (@fromUser, @toUser, @date)
END
GO

CREATE OR ALTER PROCEDURE getRequestByID
    @request_id INT
AS
BEGIN
    SELECT *
    FROM Request
    WHERE request_id = @request_id
END
GO

CREATE OR ALTER PROCEDURE getAllRequests
AS
BEGIN
    SELECT *
    FROM Request
END
GO

CREATE OR ALTER PROCEDURE getAllRequestsByFromUserID
    @fromUser INT
AS
BEGIN
    SELECT *
    FROM Request
    WHERE request_fromUser = @fromUser
END
GO

CREATE OR ALTER PROCEDURE getAllRequestsByToUserID
    @toUser INT
AS
BEGIN
    SELECT *
    FROM Request
    WHERE request_toUser = @toUser
END
GO

CREATE OR ALTER PROCEDURE deleteOldRequests
AS
BEGIN
    DELETE FROM Request WHERE request_date < DATEADD(YEAR, -1, GETDATE())
END
