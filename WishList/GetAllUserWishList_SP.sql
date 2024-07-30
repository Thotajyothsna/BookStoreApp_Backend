--Getting All The WishList Items/Books of User

CREATE PROCEDURE GetAllUserWishList
(
    @UserId INT
)
AS
BEGIN
    BEGIN TRY
        
        -- Check if the user exists
        IF NOT EXISTS (SELECT 1 FROM Users WHERE UserId = @UserId)
        BEGIN
            THROW 80002, 'The specified user does not exist.', 1;
        END

        -- Fetch the user's wishlist with book details
        SELECT * 
        FROM WishList W
        JOIN Books B ON W.BookId = B.BookId
        WHERE W.UserId = @UserId;

    END TRY
    BEGIN CATCH
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
