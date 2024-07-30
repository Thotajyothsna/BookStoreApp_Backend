--Adding Item/Book to WishList

CREATE PROCEDURE AddToWishList
(
    @BookId INT,
    @UserId INT
)
AS
BEGIN
    BEGIN TRY
        
        -- Check if the book exists
        IF NOT EXISTS (SELECT 1 FROM Books WHERE BookId = @BookId)
        BEGIN
            THROW 60003, 'The specified book does not exist.', 1;
        END

        -- Check if the user exists
        IF NOT EXISTS (SELECT 1 FROM Users WHERE UserId = @UserId)
        BEGIN
            THROW 60004, 'The specified user does not exist.', 1;
        END

        -- Check if the book is already in the user's wishlist
        IF EXISTS (SELECT 1 FROM WishList WHERE BookId = @BookId AND UserId = @UserId)
        BEGIN
            THROW 60005, 'The book is already in the user''s wishlist.', 1;
        END

        -- Start Transaction
        BEGIN TRANSACTION;

        -- Insert data into WishList table
        INSERT INTO WishList (BookId, UserId)
        VALUES (@BookId, @UserId);

        -- Commit Transaction
        COMMIT TRANSACTION;

        -- Return success message
        SELECT 'Book added to wishlist successfully.' AS Message;

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
