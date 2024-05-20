create table Challenge(
	challenge_id INT IDENTITY(1,1) PRIMARY KEY,
	challenge_description varchar(MAX),
	challenge_rule varchar(MAX),
	challenge_amount int,
	challenge_reward int
)