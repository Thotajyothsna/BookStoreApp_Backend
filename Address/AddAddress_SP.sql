--Adding Address of User into Address Table

CREATE PROC AddAddress
(
    @Address VARCHAR(200),
    @City VARCHAR(50),
    @State VARCHAR(50),
    @UserId INT,
    @AddressTypeId INT
)
AS
BEGIN
    BEGIN TRY
        -- Check if Address is not NULL
        IF @Address IS NULL
        BEGIN
            THROW 50001, 'Address cannot be NULL.', 1;
        END

        -- Check if City is not NULL
        IF @City IS NULL
        BEGIN
            THROW 50002, 'City cannot be NULL.', 1;
        END

        -- Check if State is not NULL
        IF @State IS NULL
        BEGIN
            THROW 50003, 'State cannot be NULL.', 1;
        END

        -- Check if AddressTypeId is a valid ForeignKey
        IF NOT EXISTS (SELECT 1 FROM AddressType WHERE AddressTypeId = @AddressTypeId)
        BEGIN
            THROW 50004, 'Invalid AddressTypeId.', 1;
        END

        -- If all validations pass, insert the record
        INSERT INTO Address(Address, City, State, UserId, AddressTypeId)
        VALUES (@Address, @City, @State, @UserId, @AddressTypeId);
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
