--Fetching all the Addresses added by User

CREATE PROCEDURE GetAllUserAddress
(
    @UserId INT
)
AS
BEGIN
    -- Initialize a variable to hold the number of addresses
    DECLARE @AddressCount INT;
    DECLARE @UserExists INT;

    BEGIN TRY
        -- Check if the UserId exists in the Users table
        SELECT @UserExists = COUNT(*) FROM SignUp WHERE UserId = @UserId;

        IF @UserExists = 0
        BEGIN
            -- Raise an error if the UserId does not exist
            THROW 50000, 'The provided UserId does not exist.', 1;
        END

        -- Check if there are any addresses for the given UserId
        SELECT @AddressCount = COUNT(*) FROM Address WHERE UserId = @UserId;

        -- If no addresses are found, raise a custom error
        IF @AddressCount = 0
        BEGIN
            THROW 50001, 'No addresses found for the given UserId.', 1;
        END

        -- Return the addresses for the given UserId
        SELECT * FROM Address WHERE UserId = @UserId;
    END TRY
    BEGIN CATCH
        -- Handle any errors that occur
        -- Capture the error details
        DECLARE @ErrorMessage NVARCHAR(4000);
        DECLARE @ErrorSeverity INT;
        DECLARE @ErrorState INT;

        SELECT
            @ErrorMessage = ERROR_MESSAGE(),
            @ErrorSeverity = ERROR_SEVERITY(),
            @ErrorState = ERROR_STATE();

        -- Raise the error with details
        THROW @ErrorMessage, @ErrorSeverity, @ErrorState;
    END CATCH
END;
