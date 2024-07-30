--Fetching All the Books Available in Books Table of BookStore_DB

CREATE PROCEDURE GetAllBooks
AS
BEGIN
    BEGIN TRY
        -- Select all records from the Books table
        SELECT * FROM Books;
    END TRY
    BEGIN CATCH
        -- Capture and handle any errors that occur during the TRY block
        DECLARE @ErrorMessage NVARCHAR(4000);
        DECLARE @ErrorSeverity INT;
        DECLARE @ErrorState INT;

        SELECT 
            @ErrorMessage = ERROR_MESSAGE(),
            @ErrorSeverity = ERROR_SEVERITY(),
            @ErrorState = ERROR_STATE();

        -- Raise the error with the captured details
        RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH
END;
