/*
DROP FUNCTION fnUserList;
DROP PROCEDURE procUserList;
DROP FUNCTION fnGetStudentDetentions;
DROP PROCEDURE procGetStudentDetentions;
DROP PROCEDURE procGetComments;
DROP PROCEDURE procAddNewComment;
DROP FUNCTION fnGetDemeritInformation;
DROP PROCEDURE procGetDemeritInformation;
DROP FUNCTION fnGetStudentDemeritList;
DROP PROCEDURE procGetStudentDemeritList;
DROP FUNCTION fnGetAllDemerits;
DROP PROCEDURE procGetAllDemerits;
DROP PROCEDURE procADtoDemeritList;
DROP PROCEDURE procAddAssignedDemerit;
DROP PROCEDURE procDeleteAssignedDemerit;
DROP PROCEDURE procDeleteFromDemeritList;
DROP PROCEDURE procADTestCleanup;
DROP PROCEDURE procCommentsTestCleanup;
DROP FUNCTION fn_IsWeekDay;
DROP FUNCTION fnGetBadStudents;
DROP TRIGGER trgAddDetention;
DROP TRIGGER trgAddStudentDetention;
*/

CREATE FUNCTION fnUserList
(
	@role varchar(20)
)
RETURNS @tblUserList TABLE (userID int, userFName varchar(15), userLName varchar(15))
AS BEGIN
	INSERT INTO @tblUserList
	SELECT DU.userID,DU.userFName,DU.userLName
	FROM DUser DU
	INNER JOIN UserRole UR ON DU.userID=UR.userID
	INNER JOIN UserRoles URS ON UR.roleID=URS.roleID
	WHERE URS.roleName LIKE '%'+@role+'%';
	
	RETURN;
	
	--Written By: Neo
END;

/*
SELECT * FROM fnUserList('Teacher');
*/

GO

CREATE PROCEDURE procUserList
(
	@role varchar(20)
)
AS BEGIN
	SELECT * FROM fnUserList(@role);
END;
/*
execute procUserList @role='Teacher';
*/

GO

CREATE FUNCTION fnGetStudentDetentions
(
	@studentID int = NULL,
	@servedStatus bit = NULL
)
RETURNS @tblStudentDetentions TABLE (studentID int, detentionID int, detentionDate date, demeritID int, served bit)
AS BEGIN
	INSERT INTO @tblStudentDetentions 
	SELECT sd.studentID, d.detentionID, d.detentionDate, ad.assignedDemeritID, sd.served
	FROM DUser u
	INNER JOIN StudentDetention sd ON u.userID=sd.studentID
	INNER JOIN Detention d ON sd.detentionID=d.detentionID
	INNER JOIN AssignedDemerits ad ON sd.studentID=ad.studentID
	WHERE sd.studentID= IsNull(@studentID, sd.studentID) AND sd.served= IsNull(@servedStatus, served);
	RETURN;
	
	--Written By: Neo
END;

/*
select * from fnGetStudentDetentions(null,null);
*/

GO

CREATE PROCEDURE procGetStudentDetentions
(
	@studentID int = NULL,
	@servedStatus bit = NULL
)
AS BEGIN
	SELECT * FROM fnGetStudentDetentions(@studentID, @servedStatus);
END;

/*
execute procGetStudentDetentions @studentID=null, @servedStatus=null;
*/

GO

CREATE PROCEDURE procGetComments
(
	@assignedDemeritID int = null,
	@userID int = null
)
AS BEGIN
	SELECT commentID, commentDesc, commentTimestamp, userID
	FROM Comments 
	WHERE assignedDemeritID = IsNull(@assignedDemeritID, assignedDemeritID)
	AND userID = IsNull(@userID, userID)
	ORDER BY commentTimestamp;
	--Written By: Tommy
END;

/*
execute procGetComments @assignedDemeritID=106;
*/

GO

CREATE PROCEDURE procAddNewComment
(
	@commentDesc text,
	@assignedDemeritID int,
	@commentLink int = null,
	@userID int,
	@errorMessage varchar(100) output
)
AS BEGIN
	DECLARE @success bit;
	SET @success = 1;
	SET @errorMessage = 'None';
	
	BEGIN TRY
		insert into Comments 
		(commentDesc, assignedDemeritID, commentTimestamp, commentLink, userID)
		values (@commentDesc, @assignedDemeritID, CURRENT_TIMESTAMP, @commentLink, @userID);
	END TRY
	
	BEGIN CATCH
		SET @success = 0;
		IF (ERROR_MESSAGE() LIKE '%PRIMARY KEY%')
			SET @errorMessage = 'Primary key violation';
		ELSE IF (ERROR_MESSAGE() LIKE '%FOREIGN KEY%')
			BEGIN
				IF (ERROR_MESSAGE() LIKE '%fkCommentsToComments%')
					SET @errorMessage = 'Foreign key violation: Comment does not exist';
				ELSE IF (ERROR_MESSAGE() LIKE '%fkCommentsToDUser%')
					SET @errorMessage = 'Foreign key violation: User does not exist.';
				ELSE IF (ERROR_MESSAGE() LIKE '%fkCommentsToAssignedDemerit%')
					SET @errorMessage = 'Foreign key violation: Demerit does not exist.';

			END;
	END CATCH
	RETURN @success;
	--Written By: Tommy 
end;

/*
DECLARE @success bit, @errorMessage varchar(100);
EXECUTE @success = procAddNewComment @commentDesc='Also Added using procedure',@assignedDemeritID=107, @commentLink=null, @userID = 1,@errorMessage = @errorMessage output;
PRINT @success;
PRINT @errorMessage;
*/

GO

CREATE FUNCTION fnGetDemeritInformation
(
	@demeritID int
)

RETURNS @tblDemeritInformation TABLE (demeritDescription varchar(50), teacherID int, adTimestamp smalldatetime, studentID int)
AS BEGIN
	INSERT INTO @tblDemeritInformation
	SELECT D.demeritDescription, AD.teacherID, AD.adTimestamp, AD.studentID 
	FROM AssignedDemerits AD
	Inner Join DemeritList DL ON AD.assignedDemeritID = DL.assignedDemeritID
	INNER JOIN Demerits D ON DL.demeritID=D.demeritID
	WHERE AD.assignedDemeritID=@demeritID;
	RETURN;
	--Written By: Dan
END;

/*
select * from fnGetDemeritInformation('104');
*/

GO

CREATE PROCEDURE procGetDemeritInformation
(
	@demeritID int
)
AS BEGIN 
	SELECT * FROM fnGetDemeritInformation(@demeritID);
END;

/*
execute procGetDemeritInformation('104');
*/

GO

CREATE FUNCTION fnGetStudentDemeritList
(
	@userID int = NULL
)
RETURNS @tblStudentDemeritList table (userID int, userFName varchar(15), userLName varchar(15), assignedDemeritID int, adTimestamp smalldatetime, assignedDemeritWeight decimal(3,2), teacherID int) 
AS BEGIN
	INSERT INTO @tblStudentDemeritList 
	SELECT DU.userID, DU.userFName, DU.userLName, AD.assignedDemeritID, adTimestamp , assignedDemeritWeight, teacherID 
	FROM DUser DU 
	INNER JOIN Student s on DU.userID=s.studentID
	INNER JOIN AssignedDemerits ad ON s.studentID=ad.studentID
	WHERE DU.userID=IsNull(@userID,userID)
	RETURN;
	--Written By: Dan
END;

/*
select * from fnGetStudentDemeritList(7);
*/

GO 

CREATE PROCEDURE procGetStudentDemeritList
(
	@userID int = null
)
AS BEGIN
	SELECT * FROM fnGetStudentDemeritList(@userID);
END;

/*
execute procGetStudentDemeritList @userID=7;
*/

GO

CREATE FUNCTION fnGetAllDemerits ()
RETURNS @tblGetDemerits TABLE (demeritID int, demeritDesc varchar(50))
AS BEGIN
	INSERT INTO @tblGetDemerits 
	SELECT demeritID, demeritDescription
	FROM Demerits;
	RETURN;
	--Written By: Ryan, et. al.
END;
 
/*
select * from fnGetAllDemerits();
*/

GO

CREATE PROCEDURE procGetAllDemerits
AS BEGIN
	SELECT * FROM fnGetAllDemerits();
END;

/*
execute procGetAllDemerits;
*/

GO

CREATE PROCEDURE procADtoDemeritList
(
	@assignedDemeritID int, 
	@demeritID int,
	@errorMessage varchar(100) output
)
AS 
BEGIN
	DECLARE @successfulInsert bit;
	SET @successfulInsert = 1;
	SET @errorMessage = 'None';
	
	
	BEGIN TRY
		INSERT INTO DemeritList (demeritID, assignedDemeritID) VALUES (@demeritID, @assignedDemeritID);
	END TRY
	
	BEGIN CATCH
		SET @successfulInsert = 0;
		IF (ERROR_MESSAGE() LIKE '%PRIMARY KEY%')
			SET @errorMessage = 'Primary key violation';
		ELSE IF (ERROR_MESSAGE() LIKE '%FOREIGN KEY%')
			BEGIN
				IF (ERROR_MESSAGE() LIKE '%fkDemeritListToAssignedDemerits%')
					SET @errorMessage = 'Foreign key violation: Assigned Demerit does not exist';
				ELSE IF (ERROR_MESSAGE() LIKE '%fkDemeritListToDemerits%')
					SET @errorMessage = 'Foreign key violation: Demerit does not exist';
			END;	
	END CATCH;

	return @successfulInsert;

END;

/*

declare @successfulInsert bit, @errorMessages varchar(50);
execute @successfulInsert = procADtoDemeritList
	@assignedDemeritID = 101, 
	@demeritID = 1,
	@errorMessage = @errorMessages output;

print @successfulInsert;
print @errorMessages;
select * from DemeritList
*/

			--IF (ERROR_MESSAGE() LIKE '%PRIMARY KEY%')
			--BEGIN
			--	SET @errorMessage = 'Primary key violation';
			--END
			--ELSE IF (ERROR_MESSAGE() LIKE '%FOREIGN KEY%')
			--	BEGIN
			--		IF (ERROR_MESSAGE() LIKE '%fkDemeritListToDemerits%')
			--			BEGIN SET @errorMessage = 'Foreign key violation: Demerit does not exist'; END
			--		ELSE IF (ERROR_MESSAGE() LIKE '%fkDemeritListToAssignedDemerits%')
			--			BEGIN SET @errorMessage = 'Foreign key violation: Assigned Demerit does not exist.'; END
			--	END
			--ELSE BEGIN SET @errorMessage = ERROR_MESSAGE();END

	--Written by: Neo



GO

CREATE PROCEDURE procAddAssignedDemerit
(	
    @teacherID int,
    @studentID int,
    @adWeight decimal(3,2),
    @errorMessage varchar(100) output
)
AS BEGIN
	DECLARE @successfullinsert bit;
   
	SET @successfullinsert = 1;
	SET @errorMessage = 'None';
	
	BEGIN TRY
		INSERT INTO AssignedDemerits (teacherID, studentID, adTimestamp, assignedDemeritWeight) VALUES (@teacherID, @studentID, CURRENT_TIMESTAMP, @adWeight)
	END TRY 

	BEGIN CATCH

		SET @successfullinsert = 0;
		IF(Error_Message() like '%Primary key%')
			BEGIN
				set @errorMessage = 'Primary key violation';
			END
		ELSE IF (Error_Message()like '%Foreign Key%')
			BEGIN
				IF(Error_Message() like '%FkAssignedDemeritsToTeacher%')
					BEGIN
						SET @errorMessage ='Foreign key violation: Teacher does not exist';
					END
				ELSE IF(Error_Message() like '%FkAssignedDemeritsToStudent%')
					BEGIN
						SET @errorMessage ='Foreign key violation: Student does not exist';
					END
				ELSE BEGIN SET @errorMessage = 'OH NO'; END	
			END;
		ELSE IF (ERROR_MESSAGE() like '%NULL%')
			BEGIN 
				IF (ERROR_MESSAGE() LIKE '%Timestamp%')
				BEGIN
					SET @errorMessage = 'Timestamp cannot be null';
				END
			END
	END CATCH;
	


	RETURN @successfullinsert;
	--Written by: Tommy (insert) & Ryan (error handling)
END;

/*
select * from AssignedDemerits
declare @successfullinsert bit, @errorMessages varchar(100);

execute @successfullinsert = procAddAssignedDemerit @teacherID=3, @studentID=1,@adWeight=1, @errorMessage = @errorMessages output;
print @successfullinsert;
print @errorMessages;
*/

GO

CREATE PROCEDURE procDeleteAssignedDemerit
(
    @teacherID int,
    @studentID int,
    @assignedDemeritID int
)
AS BEGIN
	DECLARE @success bit;
	SET @success = 1;

	DELETE FROM AssignedDemerits
	WHERE teacherID = @teacherID
	AND studentID =@studentID
	AND assignedDemeritID = @assignedDemeritID;
	
	IF (@@ROWCOUNT = 0)
	SET @success = 0;
	
	DELETE FROM DemeritList
	WHERE assignedDemeritID = @assignedDemeritID;

	IF (@@ROWCOUNT = 0)
	SET @success = 0;

	RETURN @success
	--Written by: Dan
END;

/*
declare @success bit

execute @success = procDeleteAssignedDemerit @teacherID=3, @studentID=1, @assignedDemeritID=117;
print @success
*/

GO

CREATE PROCEDURE procDeleteFromDemeritList
(
	@demeritID int,
    @assignedDemeritID int
)
AS BEGIN
	DECLARE @success bit;
	SET @success = 1;

	DELETE FROM DemeritList
	WHERE demeritID = @demeritID
	AND assignedDemeritID = @assignedDemeritID;

	IF (@@ROWCOUNT = 0)
	SET @success = 0;
	RETURN @success
	--Written by: Neo
END;

/*
declare @success bit

execute @success = procDeleteFromDemeritList @demeritID=1, @assignedDemeritID=104;
print @success
*/

GO

CREATE PROCEDURE procADTestCleanup
AS BEGIN
	DECLARE @success bit;
	SET @success = 1;
	
	DELETE FROM AssignedDemerits
	WHERE CONVERT(date, adTimestamp)=CONVERT(date, CURRENT_TIMESTAMP);
	
	IF (@@ROWCOUNT = 0)
	SET @success = 0;
	RETURN @success
	--Written by: Neo
END;

/*
declare @success bit

execute @success = procADTestCleanup;
print @success
*/

GO

CREATE PROCEDURE procCommentsTestCleanup
AS BEGIN
	DECLARE @success bit;
	SET @success = 1;
	
	DELETE FROM Comments
	WHERE CONVERT(date, commentTimestamp)=CONVERT(date, CURRENT_TIMESTAMP);
	
	IF (@@ROWCOUNT = 0)
	SET @success = 0;
	RETURN @success
	--Written by: Neo
END;

/*
declare @success bit

execute @success = procCommentsTestCleanup;
print @success
*/

GO

create function fn_IsWeekDay 
(
    @date datetime 
)
returns bit 
as 
begin 

    declare @dtfirst int
    declare @dtweek int 
    declare @iswkday bit 

    set @dtfirst = @@datefirst - 1
    set @dtweek = datepart(weekday, @date) - 1

    if (@dtfirst + @dtweek) % 7 not in (5, 6)
        set @iswkday = 1 --business day
    else
        set @iswkday = 0 --weekend

    return @iswkday
    --FROM THE INTERTUBES
end;


GO

CREATE FUNCTION fnGetBadStudents
(
	@badKidsWeight decimal(3,2)
)
RETURNS @tblBadKids TABLE(studentID int, demeritWeight decimal(3,2))
AS BEGIN
	INSERT INTO @tblBadKids
	SELECT studentID, sum(assignedDemeritWeight) as demeritWeight 
	FROM AssignedDemerits 
	GROUP BY studentID 
	HAVING sum(assignedDemeritWeight) >= @badKidsWeight;
	RETURN;
	--Written by: Neo & Tommy, Inc.
END;

/*
select * from fnGetBadStudents(1.5);
*/

GO

CREATE TRIGGER trgAddDetention
ON AssignedDemerits
AFTER insert
AS BEGIN
	DECLARE @success bit;
	DECLARE @error varchar(100);
	DECLARE @studentID int;
	SELECT @studentID=studentID FROM fnGetBadStudents(1.5);
	IF(@studentID != null)
	BEGIN
		BEGIN TRY
			DECLARE @iswkday bit;
			SELECT @iswkday=iswkday FROM fn_IsWeekDay(CURRENT_TIMESTAMP+5)
			IF (@iswkday LIKE 1)
				BEGIN INSERT INTO Detention (detentionDate) values (CURRENT_TIMESTAMP+5) END
			ELSE BEGIN
				SELECT @iswkday=iswkday FROM fn_IsWeekDay(CURRENT_TIMESTAMP+6)
				IF (@iswkday LIKE 1)
					BEGIN INSERT INTO Detention (detentionDate) values (CURRENT_TIMESTAMP+6) END
				ELSE INSERT INTO Detention (detentionDate) values (CURRENT_TIMESTAMP+7)
			END;	
		END TRY
		BEGIN CATCH
			SET @success = 0;
			SET @error = ERROR_MESSAGE();
			PRINT @error;
		END CATCH;
	END;
END;

/**/

GO

CREATE TRIGGER trgAddStudentDetention
ON Detention
AFTER INSERT
AS BEGIN
	DECLARE @success bit;
	DECLARE @error varchar(100);
	DECLARE @studentID int;
	DECLARE @detentionID int;
	DECLARE @when smalldatetime;
	SELECT @studentID=studentID FROM fnGetBadStudents(1.5);
	IF(@studentID != null)
	BEGIN
	DECLARE @iswkday bit;
			SELECT @iswkday=iswkday FROM fn_IsWeekDay(CURRENT_TIMESTAMP+5)
			if (@iswkday LIKE 1) BEGIN SET @when=CURRENT_TIMESTAMP+5 END
			ELSE SELECT @iswkday=iswkday FROM fn_IsWeekDay(CURRENT_TIMESTAMP+6)
			IF (@iswkday LIKE 1) BEGIN SET @when=CURRENT_TIMESTAMP+6 END 
			ELSE SELECT @iswkday=iswkday FROM fn_IsWeekDay(CURRENT_TIMESTAMP+7)
			IF (@iswkday LIKE 1) BEGIN SET @when=CURRENT_TIMESTAMP+7 END
			
		SELECT @detentionID=detentionID from Inserted where detentionDate=@when;
		INSERT INTO StudentDetention values (@studentID, @detentionID, 0);
	END
END;