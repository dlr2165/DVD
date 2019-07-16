-- Stored Procedures

USE DVDLibrary
GO

--Insert
--Select
--Update
--Delete


-- Director Stored Procs

-- insert
IF EXISTS (
	SELECT *
	FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'sp_DirectorInsert'
)

BEGIN
	DROP PROCEDURE sp_DirectorInsert
END
GO

CREATE PROCEDURE sp_DirectorInsert (
	@firstName varchar(15),
	@middleName varchar(15),
	@lastName varchar(20),
	@birthDate date,
	@deathDate date
)
AS

BEGIN
DECLARE @directorID int
INSERT INTO Director (
		firstName, 
		middleName, 
		lastName, 
		birthDate, 
		deathDate)
	Values(
		@firstName,
		@middleName,
		@lastName,
		@birthDate,
		@deathDate)

SET @directorID = SCOPE_IDENTITY()
SELECT @directorID
END
GO

-- select
IF EXISTS (
	SELECT *
	FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'sp_DirectorSelect'
)

BEGIN
	DROP PROCEDURE sp_DirectorSelect
END
GO

CREATE PROCEDURE sp_DirectorSelect (
	@directorID int = 0
	)

AS
BEGIN
	IF @directorID = 0
	BEGIN
	SELECT * FROM Director
	END
	ELSE
	BEGIN
	SELECT * FROM Director
	WHERE directorID = @directorID
	END
END
GO

-- update
IF EXISTS (
	SELECT *
	FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'sp_DirectorUpdate'
)

BEGIN
	DROP PROCEDURE sp_DirectorUpdate
END
GO



CREATE PROCEDURE sp_DirectorUpdate (
	@directorID int,
	@firstName varchar(15),
	@middleName varchar(15),
	@lastName varchar(20),
	@birthDate date,
	@deathDate date
)

AS
BEGIN
	UPDATE Director
	SET
	firstName = @firstName,
	middleName = @middleName,
	lastName = @lastName,
	birthDate = @birthDate,
	deathDate = @deathDate

	WHERE @directorID = directorID
END	
GO

-- delete
IF EXISTS (
	SELECT *
	FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'sp_DirectorDelete'
)

BEGIN
	DROP PROCEDURE sp_DirectorDelete
END
GO

CREATE PROCEDURE sp_DirectorDelete 
	@directorID int

AS
BEGIN
	DELETE FROM Director
	WHERE directorID = @directorID
END
GO


-- Rating Stored Procs

-- insert
IF EXISTS (
	SELECT *
	FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'sp_RatingInsert'
	)

BEGIN
DROP PROCEDURE sp_RatingInsert
END
GO

CREATE PROCEDURE sp_RatingInsert
	@rating  varchar(5)

AS
BEGIN
DECLARE @ratingID int
INSERT INTO Ratings (
	rating)
	Values(
	@rating)

SET @ratingID = SCOPE_IDENTITY()
SELECT @ratingID
END
GO

	
-- select
IF EXISTS (
	SELECT *
	FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'sp_RatingSelect'
	)

BEGIN
DROP PROCEDURE sp_RatingSelect
END
GO

CREATE PROCEDURE sp_RatingSelect(
	@ratingID int = 0
	)

AS
BEGIN
	IF @ratingID = 0
	BEGIN
	SELECT * FROM Ratings
	END
	ELSE
	BEGIN
	SELECT * FROM Ratings
	WHERE ratingID = @ratingID
	END
END
GO


-- update
IF EXISTS (
	SELECT *
	FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'sp_RatingUpdate'
	)

BEGIN
	DROP PROCEDURE sp_RatingUpdate
END
GO

CREATE PROCEDURE sp_RatingUpdate (
	@ratingID int,
	@rating varchar(5)
)

AS
BEGIN
	UPDATE Ratings
	SET
	rating = @rating

	WHERE ratingID = @ratingID
END
GO


-- delete
IF EXISTS (
	SELECT *
	FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'sp_ratingDelete'
	)

BEGIN
DROP PROCEDURE sp_RatingDelete
END
GO

CREATE PROCEDURE sp_RatingDelete
	@ratingID int
	
AS
BEGIN
	DELETE FROM Ratings
	WHERE ratingID = @ratingID
END
GO


-- DVD Stored Procs

-- insert
IF EXISTS (
	SELECT *
	FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'sp_DVDInsert'
	)

BEGIN
DROP PROCEDURE sp_DVDInsert
END
GO

CREATE PROCEDURE sp_DVDInsert(
	@title varchar(50),
	@releaseYear int,
	@directorID int,
	@ratingID int,
	@notes varchar(152)
	)

AS
BEGIN
DECLARE @dvdID int
INSERT INTO DVD(
	title,
	releaseYear,
	directorID,
	ratingID,
	notes)
	VALUES(
	@title,
	@releaseYear,
	@directorID,
	@ratingID,
	@notes)

SET @dvdID = SCOPE_IDENTITY()
SELECT @dvdID
END
GO

-- select
IF EXISTS (
	SELECT *
	FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'sp_DVDSelect'
	)

BEGIN
DROP PROCEDURE sp_DVDSelect
END
GO

CREATE PROCEDURE sp_DVDSelect(
	@dvdID int = 0
	)

AS
BEGIN
	IF @dvdID = 0
	BEGIN
	SELECT * FROM DVD
	END
	ELSE
	BEGIN
	SELECT * FROM DVD
	WHERE dvdID = @dvdID
	END
END
GO

-- update
IF EXISTS (
	SELECT *
	FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'sp_DVDUpdate'
	)

BEGIN
DROP PROCEDURE sp_DVDUpdate
END
GO

CREATE PROCEDURE sp_DVDUpdate(
	@dvdID int,
	@title varchar(50),
	@releaseYear int,
	@directorID int,
	@ratingID int,
	@notes varchar(152)
	)

AS
BEGIN
	UPDATE DVD
	SET
	title = @title,
	releaseYear = @releaseYear,
	directorID = @directorID,
	ratingID = @ratingID,
	notes = @notes

	WHERE dvdID = @dvdID
END
GO

-- delete
IF EXISTS (
	SELECT *
	FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'sp_DVDDelete'
	)

BEGIN
DROP PROCEDURE sp_DVDDelete
END
GO

CREATE PROCEDURE sp_DVDDelete
	@dvdID int

AS
BEGIN
	DELETE FROM DVD
	WHERE dvdID = @dvdID
END
GO

-- select by ID
IF EXISTS (
	SELECT *
	FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'sp_SelectDVDByID'
	)

BEGIN
DROP PROCEDURE sp_SelectDVDByID
END
GO

CREATE PROCEDURE sp_SelectDVDByID
	@dvdID int

AS
BEGIN
	SELECT * FROM DVD
	WHERE dvdID = @dvdID
END
GO

-- select by Title
IF EXISTS (
	SELECT *
	FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'sp_SelectDVDByTitle'
	)

BEGIN
DROP PROCEDURE sp_SelectDVDByTitle
END
GO

CREATE PROCEDURE sp_SelectDVDByTitle
	@title varchar

AS
BEGIN
	SELECT * FROM DVD
	WHERE title = @title
END
GO

-- select by Release Year
IF EXISTS (
	SELECT *
	FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'sp_SelectDVDByYear'
	)

BEGIN
DROP PROCEDURE sp_SelectDVDByYear
END
GO

CREATE PROCEDURE sp_SelectDVDByYear
	@releaseYear int

AS
BEGIN
	SELECT * FROM DVD
	WHERE releaseYear = @releaseYear
END
GO

-- select by Director

 IF EXISTS (
	SELECT *
	FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'sp_SelectDVDByDirecotr'
	)

BEGIN
DROP PROCEDURE sp_SelectDVDByDirector
END
GO

CREATE PROCEDURE sp_SelectDVDByDirector
	@directorName varchar

AS
BEGIN
	SELECT title, (Director.firstName + ' ' + Director.lastName) Name
	FROM DVD
	INNER JOIN Director ON Director.directorID = DVD.directorID
	WHERE 'Director.firstName' = @directorName OR 'Director.middleName' = @directorName OR 'Director.lastName' = @directorName
END
GO

-- select by Rating
IF EXISTS (
	SELECT *
	FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'sp_SelectDVDByRating'
	)

BEGIN
DROP PROCEDURE sp_SelectDVDByRating
END
GO

CREATE PROCEDURE sp_SelectDVDByRating
	@rating varchar

AS
BEGIN
	SELECT title, Ratings.rating
	FROM DVD
	INNER JOIN Ratings ON Ratings.ratingID = DVD.ratingID
	WHERE Ratings.rating = @rating
END
GO