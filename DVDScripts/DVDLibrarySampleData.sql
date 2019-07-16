-- Populate Tables

--load DB
USE DVDLibrary
GO

INSERT INTO Director (firstName, middleName, lastName, birthDate, deathDate)
	VALUES
	('James',null,'Wan','2/26/1977',null),
	('Wes','Earl','Craven','8/2/1939','8/30/2015'),
	('Mel',null,'Brooks','6/28/1926',null),
	('Michael','Benjamin','Bay','2/17/1965',null),
	('Ivan',null,'Reitman','10/27/1946',null)

INSERT INTO Ratings (rating)
	VALUES
	('G'),
	('PG'),
	('PG-13'),
	('R');


INSERT INTO DVD (title, releaseYear, directorID, ratingID, notes)
	VALUES
	('Scream','1996', 2, 4, 'knife weilding psychopath on his way to kill them kids'),
	('The Conjuring','2013',1,4,'family moves into a seculded, haunted farm house'),
	('Young Frankenstein','1974',3,2,'dr. Frankenstein creates a zombie. monester or not?'),
	('Nightmare on Elm Street','1984',2,4,'dream demon kills in your sleep'),
	('Transformers','2007',4,3,'two opposing robot factions show up on earth to battle'),
	('Stripes',1981,5,4,'two friends are dissatisfied with everyday life, join Army'),
	('Armageddon','1998',4,3,'a group of oil workers are sent to space to destroy a meteor'),
	('Blazing Saddles','1974',3,4,'railroad worker turned sherrif helps town against bandits'),
	('Aquaman','2018',1,3,'human born heir to atlantis stops war between ocean and land'),
	('Ghost Busters','1984',5,2,'three former parapsychology professors hunt ghosts');


---- Gets all values from All tables

--	select * from DVD
--	select * from Ratings
--	select * from Director

---- Loads necessary values from ALL tables

--	select title, releaseYear, (Director.firstName +' '+ Director.lastName) AS Director, Ratings.rating, notes 
--	from DVD 
--	Join Ratings on Ratings.ratingID = DVD.ratingID 
--	Join Director on Director.directorID = DVD.directorID