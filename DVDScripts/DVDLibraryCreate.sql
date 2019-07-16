-- Create DB / Tables

-- use master so that we can create/recreate our database from start
USE master
GO
-- if the database exists, drop the database and all info
IF EXISTS(SELECT * 
	FROM sys.databases 
	WHERE NAME='DVDLibrary'
	)

DROP DATABASE DVDLibrary
GO
--create database
CREATE DATABASE DVDLibrary
GO
--use database
USE DVDLibrary
GO


--tables are ordered so that they table with FK (DVDs) is loaded after the aforementioned referenced PKs are created
--IE: DVDs contains a FK for directorID.  If we try to create that value before creating the PK that directorID references (Director Table ; directorID) we get an error


CREATE TABLE Director
	(
	directorID int PRIMARY KEY IDENTITY (1,1),
	firstName varchar(15) NOT NULL,
	middleName varchar(15) NULL,
	lastName varchar(20) NOT NULL,
	birthDate date NOT NULL,
	deathDate date NULL
	);
GO

CREATE TABLE Ratings
	(
	ratingID int PRIMARY KEY IDENTITY (1,1),
	rating varchar(5) NOT NULL
	);
GO

CREATE TABLE DVD
	(
	dvdID int PRIMARY KEY IDENTITY (1,1),
	title varchar(50) NOT NULL,
	releaseYear int NOT NULL,
	directorID int FOREIGN KEY REFERENCES Director(directorID) NOT NULL,
	ratingID int FOREIGN KEY REFERENCES Ratings(ratingID) NOT NULL,
	notes varchar(152) NULL
	);
GO