--Deleting the Existed Address Of User

CREATE PROC DeleteAddress
(
    @AddressId INT
)
AS
BEGIN
    BEGIN TRY
        -- Check if AddressId exists in the Address table
        IF NOT EXISTS (SELECT 1 FROM Address WHERE AddressId = @AddressId)
        BEGIN
            THROW 50001, 'AddressId does not exist.', 1;
        END

        -- If AddressId exists, delete the record
        DELETE FROM Address WHERE AddressId = @AddressId;
    END TRY
    BEGIN CATCH
        -- Handle errors
        DECLARE @ErrorMessage NVARCHAR(4000);
        DECLARE @ErrorSeverity INT;
        DECLARE @ErrorState INT;

        SELECT 
            @ErrorMessage = ERROR_MESSAGE(),
            @ErrorSeverity = ERROR_SEVERITY(),
            @ErrorState = ERROR_STATE();

        RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH
END
