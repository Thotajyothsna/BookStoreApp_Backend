--Adding Item/Book to Cart

CREATE PROCEDURE AddToCart
(
    @BookId INT,
    @UserId INT
)
AS
BEGIN
    BEGIN TRY
        -- Check if BookId exists in the Books table
        IF NOT EXISTS (SELECT 1 FROM Books WHERE BookId = @BookId)
        BEGIN
            THROW 50001, 'BookId does not exist in Books table.', 1;
        END

        -- Check if UserId exists in the Users table
        IF NOT EXISTS (SELECT 1 FROM Users WHERE UserId = @UserId)
        BEGIN
            THROW 50002, 'UserId does not exist in Users table.', 1;
        END

        -- Check if the book is already in the user's cart
        IF EXISTS (SELECT 1 FROM CART WHERE BookId = @BookId AND UserId = @UserId)
        BEGIN
            THROW 50003, 'The book is already in the user''s cart.', 1;
        END

        -- Insert the book into the cart
        INSERT INTO CART(BookId, UserId)
        VALUES(@BookId, @UserId);

        -- Confirm the insert
        SELECT 'Book added to cart successfully.' AS Message;
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
