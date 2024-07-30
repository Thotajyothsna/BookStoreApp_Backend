--Deleting Book By its BookId in BookTable of BookStore_DB

CREATE PROCEDURE DeleteBook
(
    @BookId INT
)
AS
BEGIN
    BEGIN TRY
        -- Check if the BookId exists before attempting to delete
        IF NOT EXISTS (SELECT 1 FROM Books WHERE BookId = @BookId)
        BEGIN
            THROW 50001, 'BookId not found in Books table.', 1;
        END

        -- Delete the book with the given BookId
        DELETE FROM Books WHERE BookId = @BookId;

        -- Check if the delete was successful
        IF @@ROWCOUNT = 0
        BEGIN
            THROW 50002, 'Delete operation failed. No rows were deleted.', 1;
        END
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
