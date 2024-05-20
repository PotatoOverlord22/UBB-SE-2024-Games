-- Font data
INSERT INTO Font (font_name, font_price, font_type)
VALUES ('Arial', 10, 'Sans-serif'),
       ('Times New Roman', 15, 'Serif'),
       ('Verdana', 12, 'Display'),
	   ('Helvetica', 18, 'Script'),
       ('Calibri', 14, 'Retro'),
       ('Georgia', 16, 'Modern'),
       ('Courier New', 20, 'Monospace');

-- Icon data
INSERT INTO Icon (icon_name, icon_price, icon_path)
VALUES ('demo_avatar', 5, 'C:\Users\danla\Source\Repos\UBB-SE-2024-Team-42-Part-2\assets\demo_avatar.jpg');

-- Title data
INSERT INTO Title (title_name, title_price, title_content)
VALUES ('Beginner', 0, 'Novice Gambler'),
       ('High Roller', 50, 'Big Spender'),
       ('Poker Pro', 25, 'Professional Player'),
	   ('Lucky Charm', 30, 'Fortunate Gambler'),
       ('Rookie', 5, 'Newbie Player'),
       ('VIP', 100, 'Very Important Player'),
       ('Bluff Master', 35, 'Skilled Bluffer');

-- PokerTable data
INSERT INTO PokerTable (table_name, table_buyIn, table_playerLimit)
VALUES ('Table1', 100, 2),
       ('Table2', 200, 8),
       ('Table3', 500, 4),
	   ('Table4', 300, 8),
       ('Table5', 1000, 10),
       ('Table6', 500, 3),
       ('Table7', 2000, 4);

-- Challenge data
INSERT INTO Challenge (challenge_description, challenge_rule, challenge_amount, challenge_reward)
VALUES ('Win 5 hands in a row', 'Consecutive wins', 5, 100),
       ('Achieve a Royal Flush', 'Rare poker hand', 1, 500),
       ('Play 100 hands', 'Cumulative hands played', 100, 50),
	   ('Double Up', 'Double your chips in a session', 1, 200),
       ('Triple 7s', 'Get three 7s in a hand', 3, 300),
       ('All In Master', 'Win a hand with an All In bet', 1, 150),
       ('Royal Showdown', 'Get a Royal Flush in a showdown', 1, 1000);

-- Users data
INSERT INTO Users (user_username, user_currentFont, user_currentTitle, user_currentIcon, user_currentTable, user_chips, user_stack, user_streak, user_handsPlayed, user_level, user_lastLogin)
VALUES ('player1', 1, 1, 1, 1, 100000, 500, 3, 200, 10, '2024-04-28'),
       ('player2', 2, 2, 1, 2, 50000, 250, 1, 50, 5, '2024-04-27'),
       ('player3', 3, 3, 1, 3, 20000, 1000, 7, 500, 15, '2024-04-26'),
	   ('player4', 4, 4, 1, 4, 150000, 750, 5, 300, 12, '2024-04-27'),
       ('player5', 3, 6, 1, 3, 250000, 1250, 10, 700, 20, '2024-04-26'),
       ('player6', 2, 5, 1, 2, 180000, 900, 6, 400, 15, '2024-04-28'),
       ('player7', 1, 3, 1, 1, 30000, 1500, 8, 1000, 25, '2024-04-29'),
	   ('player8', 1, null, 1, 1, 10, 0, 0, 0 , 1, '2024-04-29'),
	   ('player9', 1, 1, 1, 1, 0, 0, 0, 0 , 1, null);

-- Request data
INSERT INTO Request (request_fromUser, request_toUser, request_date)
VALUES (1, 2, '2024-04-28'),
       (2, 3, '2024-04-27'),
       (3, 1, '2024-04-26'),
	   (4, 5, '2024-04-27'),
       (5, 6, '2024-04-26'),
       (6, 7, '2024-04-27'),
       (7, 4, '2024-04-29');

-- UserFonts data
INSERT INTO UserFonts (user_id, font_id)
VALUES (1, 1),
       (2, 2),
       (3, 3),
	   (4, 4),
       (5, 3),
       (6, 2),
       (7, 1);

-- UserIcons data
INSERT INTO UserIcons (user_id, icon_id)
VALUES (1, 1),
       (2, 1),
       (3, 1),
	   (4, 1),
       (5, 1),
       (6, 1),
       (7, 1);

-- UserTitles data
INSERT INTO UserTitles (user_id, title_id)
VALUES (1, 1),
       (2, 2),
       (3, 3),
	   (4, 4),
       (5, 6),
       (6, 5),
       (7, 3);