--Fetching All the items/Books in Cart of particular User

CREATE PROCEDURE GetAllUserCart
(
    @UserId INT
)
AS
BEGIN
    BEGIN TRY
        -- Check if UserId exists in the Users table
        IF NOT EXISTS (SELECT 1 FROM Users WHERE UserId = @UserId)
        BEGIN
            THROW 50001, 'UserId does not exist in Users table.', 1;
        END

        -- Check if there are any items in the cart for the given UserId
        IF NOT EXISTS (SELECT 1 FROM Cart WHERE UserId = @UserId)
        BEGIN
            THROW 50002, 'No items found in the cart for the given UserId.', 1;
        END

        -- Retrieve all items in the user's cart
        SELECT 
            C.*,
            B.*,
            C.Quantity AS CartQuantity,
            B.Quantity AS BookQuantity,
            (B.DiscountPrice * C.Quantity) AS TotalPrice
        FROM Cart C
        JOIN Books B ON C.BookId = B.BookId
        WHERE C.UserId = @UserId;
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
