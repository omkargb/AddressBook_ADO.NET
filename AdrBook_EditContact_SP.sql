Create or alter procedure spEditContactDetails
(  
   @ContactId int,
   @FirstName varchar(16),
   @LastName varchar(16),
   @PhoneNumber varchar(16),
   @EmailId varchar(24)
)  
as
begin try
UPDATE ContactsTable set FirstName=@FirstName,	LastName=@LastName,	PhoneNumber=@PhoneNumber, EmailId=@EmailId
where Id=@ContactId;
End Try

BEGIN CATCH
  SELECT
    ERROR_NUMBER() AS ErrorNumber,
    ERROR_STATE() AS ErrorState,
    ERROR_PROCEDURE() AS ErrorProcedure,
    ERROR_LINE() AS ErrorLine,
    ERROR_MESSAGE() AS ErrorMessage;
END CATCH
