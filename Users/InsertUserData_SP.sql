--Stored Procedure for Register User Data to Insert Data into SignUp Table 

CREATE PROCEDURE InsertUserData
(
    @FullName VARCHAR(50),
    @EmailId VARCHAR(100),
    @Password VARCHAR(50),
    @MobileNumber VARCHAR(18)
)
AS
BEGIN
    BEGIN TRY
        -- Validate FullName: at least 3 characters
        IF LEN(@FullName) < 3 OR LEN(@FullName) > 50
        BEGIN
            THROW 50001, 'FullName must be between 3 and 50 characters.', 1;
        END

        -- Validate EmailId format (basic check)
        IF @EmailId NOT LIKE '%@%.%'
        BEGIN
            THROW 50002, 'Invalid EmailId format.', 1;
        END

        -- Validate Password: at least 8 characters, including letters, numbers, and at least one special character
        IF LEN(@Password) < 8 OR LEN(@Password) > 50 OR 
           @Password NOT LIKE '%[0-9]%' OR 
           @Password NOT LIKE '%[A-Za-z]%' OR 
           @Password NOT LIKE '%[^A-Za-z0-9]%'
        BEGIN
            THROW 50003, 'Password must be at least 8 characters long, including letters, numbers, and at least one special character.', 1;
        END

        -- Validate MobileNumber: must be exactly 10 digits
        IF LEN(@MobileNumber) <> 10 OR @MobileNumber NOT LIKE '%[0-9]%'
        BEGIN
            THROW 50004, 'MobileNumber must be exactly 10 digits.', 1;
        END

        -- Start Transaction
        BEGIN TRANSACTION;

        -- Insert data into SignUp table
        INSERT INTO SignUp (FullName, EmailId, Password, MobileNumber)
        VALUES (@FullName, @EmailId, @Password, @MobileNumber);

        -- Commit Transaction
        COMMIT TRANSACTION;

        -- Return success message
        SELECT 'User data inserted successfully.' AS Message;

    END TRY
    BEGIN CATCH
        -- Rollback transaction if an error occurs
        IF XACT_STATE() <> 0
        BEGIN
            ROLLBACK TRANSACTION;
        END

        -- Capture error information
        DECLARE @ErrorMessage NVARCHAR(4000);
        DECLARE @ErrorSeverity INT;
        DECLARE @ErrorState INT;

        SELECT 
            @ErrorMessage = ERROR_MESSAGE(),
            @ErrorSeverity = ERROR_SEVERITY(),
            @ErrorState = ERROR_STATE();

        -- Return error message
        RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH
END;
