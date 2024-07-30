--Editing the Existed Address of User

CREATE PROC UpdateAddress
(
    @AddressId INT,
    @Address VARCHAR(200),
    @City VARCHAR(50),
    @State VARCHAR(50),
    @AddressTypeId INT
)
AS
BEGIN
    BEGIN TRY
        -- Check if AddressId exists in the Address table.
        -- If the AddressId does not exist, throw an error.
        IF NOT EXISTS (SELECT 1 FROM Address WHERE AddressId = @AddressId)
        BEGIN
            THROW 50001, 'AddressId does not exist.', 1;
        END

        -- Check if the Address parameter is not NULL.
        -- If Address is NULL, throw an error.
        IF @Address IS NULL
        BEGIN
            THROW 50002, 'Address cannot be NULL.', 1;
        END

        -- Check if the City parameter is not NULL.
        -- If City is NULL, throw an error.
        IF @City IS NULL
        BEGIN
            THROW 50003, 'City cannot be NULL.', 1;
        END

        -- Check if the State parameter is not NULL.
        -- If State is NULL, throw an error.
        IF @State IS NULL
        BEGIN
            THROW 50004, 'State cannot be NULL.', 1;
        END

        -- Check if AddressTypeId exists in the AddressType table.
        -- If the AddressTypeId is invalid (does not exist in the AddressType table), throw an error.
        IF NOT EXISTS (SELECT 1 FROM AddressType WHERE AddressTypeId = @AddressTypeId)
        BEGIN
            THROW 50005, 'Invalid AddressTypeId.', 1;
        END

        -- If all validations pass, update the record in the Address table.
        -- Set the new values for Address, City, State, and AddressTypeId where AddressId matches the provided AddressId.
        UPDATE Address
        SET Address = @Address, City = @City, State = @State, AddressTypeId = @AddressTypeId
        WHERE AddressId = @AddressId;
    END TRY
    BEGIN CATCH
        -- Handle any errors that occur during the execution of the TRY block.
        
        -- Declare variables to store error information.
        DECLARE @ErrorMessage NVARCHAR(4000);
        DECLARE @ErrorSeverity INT;
        DECLARE @ErrorState INT;

        -- Retrieve the error message, severity, and state from the error that occurred.
        SELECT 
            @ErrorMessage = ERROR_MESSAGE(),
            @ErrorSeverity = ERROR_SEVERITY(),
            @ErrorState = ERROR_STATE();

        -- Raise the error again using RAISERROR with the retrieved error message, severity, and state.
        -- This allows the error to be caught by the caller of the stored procedure.
        RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH
END
