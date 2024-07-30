--Fetching the Book based on Id

CREATE PROCEDURE GetById
(
    @BookId INT
)
AS
BEGIN
    BEGIN TRY
        -- Check if the BookId exists before attempting to select
        IF NOT EXISTS (SELECT 1 FROM Books WHERE BookId = @BookId)
        BEGIN
            THROW 50001, 'BookId not found in Books table.', 1;
        END

        -- Select the book with the given BookId
        SELECT * FROM Books WHERE BookId = @BookId;
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

