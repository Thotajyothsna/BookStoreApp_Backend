--Fetching all the orders placed by User

CREATE PROCEDURE GetOrdersByUser
    @UserId INT
AS
BEGIN
    -- Start a transaction
    BEGIN TRANSACTION;

    BEGIN TRY
        -- Check if the user exists
        IF NOT EXISTS (SELECT 1 FROM SignUp WHERE UserId = @UserId)
        BEGIN
            -- Rollback the transaction and raise an error if the user does not exist
            ROLLBACK TRANSACTION;
            RAISERROR ('User with the provided UserId does not exist.', 16, 1);
            RETURN;
        END

        -- Retrieve all orders for the specified user along with book details
        SELECT 
            o.OrderId,
            o.CartId,
            o.UserId,
            o.AddressId,
            o.Quantity,
            o.OriginalPrice,
            o.DiscountRate,
            o.DiscountPrice,
            o.TotalPrice,
            o.OrderPlacedDate,
            b.BookId,
            b.BookName,
            b.AuthorName,
            b.Image,
			(o.Quantity * o.OriginalPrice) AS TotalOriginalPrice,
            (o.Quantity * o.DiscountPrice) AS TotalDiscountPrice

        FROM 
            ORDERS o
        INNER JOIN 
            CART c ON o.CartId = c.CartId
        INNER JOIN 
            BOOKS b ON c.BookId = b.BookId
        WHERE 
            o.UserId = @UserId;

        -- Commit the transaction
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        -- Rollback the transaction in case of an error
        ROLLBACK TRANSACTION;

        -- Raise the error with details
        THROW;
    END CATCH
END;
