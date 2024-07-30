--Delete Item/Book from WishList Of User

CREATE PROCEDURE DeleteWishListItem
(
    @WishListId BIGINT
)
AS
BEGIN
    BEGIN TRY
       
        -- Check if the wishlist item exists
        IF NOT EXISTS (SELECT 1 FROM WishList WHERE WishListId = @WishListId)
        BEGIN
            THROW 70002, 'The specified wishlist item does not exist.', 1;
        END

        -- Start Transaction
        BEGIN TRANSACTION;

        -- Delete the wishlist item
        DELETE FROM WishList WHERE WishListId = @WishListId;

        -- Commit Transaction
        COMMIT TRANSACTION;

        -- Return success message
        SELECT 'Wishlist item deleted successfully.' AS Message;

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
