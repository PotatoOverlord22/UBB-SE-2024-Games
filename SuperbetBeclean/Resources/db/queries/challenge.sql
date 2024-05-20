CREATE OR ALTER PROCEDURE createChallenge
    --CREATE
    @challenge_description VARCHAR(MAX),
    @challenge_rule VARCHAR(MAX),
    @challenge_amount INT,
    @challenge_reward INT
AS
BEGIN
    INSERT INTO Challenge
        (challenge_description, challenge_rule, challenge_amount, challenge_reward)
    VALUES
        (@challenge_description, @challenge_rule, @challenge_amount, @challenge_reward)
END
GO

-- READ & READ ALL
CREATE OR ALTER PROCEDURE getChallengeById
    @challenge_id INT
AS
BEGIN
    SELECT *
    FROM Challenge
    WHERE challenge_id = @challenge_id
END
GO


CREATE OR ALTER PROCEDURE GetAllChallenges
AS
BEGIN
    SELECT *
    FROM Challenge
END
GO

CREATE OR ALTER PROCEDURE updateChallenge
    --UPDATE
    @challenge_id INT,
    @challenge_description VARCHAR(MAX),
    @challenge_rule VARCHAR(MAX),
    @challenge_amount INT,
    @challenge_reward INT
AS
BEGIN
    UPDATE Challenge
    SET 
        challenge_description = @challenge_description,
        challenge_rule = @challenge_rule,
        challenge_amount = @challenge_amount,
        challenge_reward = @challenge_reward
    WHERE 
        challenge_id = @challenge_id
END
GO

CREATE OR ALTER PROCEDURE deleteChallenge
    --DELETE 
    @challenge_id INT
AS
BEGIN
    DELETE FROM Challenge WHERE challenge_id = @challenge_id
END