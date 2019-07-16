-- Security

USE DVDLibrary
GO

--creates login
IF NOT EXISTS(
	SELECT *
	FROM master.dbo.syslogins
	WHERE name = 'DvdLibraryApp'
	)

BEGIN
CREATE LOGIN DvdLibraryApp WITH PASSWORD = 'testing123'
END
GO

--creates database account
IF NOT EXISTS(
	SELECT *
	FROM master.sys.server_principals
	WHERE name = 'DVDLibraryApp'
	)

BEGIN
CREATE USER DvdLibraryApp FOR LOGIN DvdLibraryApp
END
GO

-- grants SIUD permission
GRANT INSERT ON DVD TO DvdLibraryApp
GRANT SELECT ON DVD TO DvdLibraryApp
GRANT UPDATE ON DVD TO DvdLibraryApp
GRANT DELETE ON DVD TO DvdLibraryApp
GO

GRANT INSERT ON Ratings TO DvdLibraryApp
GRANT SELECT ON Ratings TO DvdLibraryApp
GRANT UPDATE ON Ratings TO DvdLibraryApp
GRANT DELETE ON Ratings TO DvdLibraryApp
GO

GRANT INSERT ON Director TO DvdLibraryApp
GRANT SELECT ON Director TO DvdLibraryApp
GRANT UPDATE ON Director TO DvdLibraryApp
GRANT DELETE ON Director TO DvdLibraryApp
GO

-- grants permission for stored procs
GRANT EXECUTE ON sp_DVDInsert TO DvdLibraryApp
GRANT EXECUTE ON sp_DVDSelect TO DvdLibraryApp
GRANT EXECUTE ON sp_DVDUpdate TO DvdLibraryApp
GRANT EXECUTE ON sp_DVDDelete TO DvdLibraryApp
GO

GRANT EXECUTE ON sp_DirectorInsert TO DvdLibraryApp
GRANT EXECUTE ON sp_DirectorSelect TO DvdLibraryApp
GRANT EXECUTE ON sp_DirectorUpdate TO DvdLibraryApp
GRANT EXECUTE ON sp_DirectorDelete TO DvdLibraryApp
GO

GRANT EXECUTE ON sp_RatingInsert TO DvdLibraryApp
GRANT EXECUTE ON sp_RatingSelect TO DvdLibraryApp
GRANT EXECUTE ON sp_RatingUpdate TO DvdLibraryApp
GRANT EXECUTE ON sp_RatingDelete TO DvdLibraryApp
GO

