--Updating Quantity in Cart 

ALTER PROCEDURE UpdateCart
(
    @Quantity SMALLINT,
    @CartId BIGINT
)
AS
BEGIN
    BEGIN TRY
        -- Check if CartId exists in the Cart table
        IF NOT EXISTS (SELECT 1 FROM Cart WHERE CartId = @CartId)
        BEGIN
            THROW 50001, 'CartId does not exist in Cart table.', 1;
        END

        -- Check if Quantity is valid
        IF @Quantity <= 0
        BEGIN
            THROW 50002, 'Quantity must be greater than zero.', 1;
        END

        -- Update the cart item
        UPDATE Cart 
        SET Quantity = @Quantity 
        WHERE CartId = @CartId;

        -- Confirm the update
        SELECT 'Cart item updated successfully.' AS Message;
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
