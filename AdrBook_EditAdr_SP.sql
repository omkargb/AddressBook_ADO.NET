Create or alter procedure spEditAddressDetails
(  
   @ContactId int,
   @Address varchar(24),
   @City varchar(16),
   @State varchar(16),
   @ZipCode int
)  
as

begin try

UPDATE ContactsTable
set Address=@Address,	City=@City,	State=@State,	ZipCode=@ZipCode 
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
