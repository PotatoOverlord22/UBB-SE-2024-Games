create table Request(
	request_id INT IDENTITY(1,1) PRIMARY KEY,
	request_fromUser int foreign key references Users(user_id),
	request_toUser int foreign key references Users(user_id),
	request_date DATE,
	CONSTRAINT AK_Requests_CandidateKey UNIQUE(request_fromUser, request_toUser, request_date)
)