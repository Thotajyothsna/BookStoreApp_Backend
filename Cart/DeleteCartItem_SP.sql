--Delete Book/Item from Cart of User

CREATE PROCEDURE DeleteCartItem
(
    @CartId BIGINT
)
AS
BEGIN
    BEGIN TRY
        -- Check if CartId exists in the CART table
        IF NOT EXISTS (SELECT 1 FROM CART WHERE CartId = @CartId)
        BEGIN
            THROW 50001, 'CartId does not exist in CART table.', 1;
        END

        -- Delete the cart item
        DELETE FROM CART WHERE CartId = @CartId;

        -- Confirm the delete
        SELECT 'Cart item deleted successfully.' AS Message;
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
