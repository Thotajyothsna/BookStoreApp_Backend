--when a User PlaceOrder ,Inserrting data into Orders table and deleting that particular record from cart

CREATE PROCEDURE PlaceOrder
    @CartId INT,
    @UserId INT,
    @AddressId INT
AS
BEGIN
    -- Start a transaction
    BEGIN TRANSACTION;

    BEGIN TRY
        -- Declare variables to hold quantity values
        DECLARE @CartQuantity INT;
        DECLARE @BookId INT;
        DECLARE @BookQuantity INT;

        -- Check if the user exists
        IF NOT EXISTS (SELECT 1 FROM USERS WHERE UserId = @UserId)
        BEGIN
            -- Rollback the transaction and raise an error if the user does not exist
            ROLLBACK TRANSACTION;
            RAISERROR ('User with the provided UserId does not exist.', 16, 1);
            RETURN;
        END

        -- Check if the address exists
        IF NOT EXISTS (SELECT 1 FROM Address WHERE AddressId = @AddressId)
        BEGIN
            -- Rollback the transaction and raise an error if the address does not exist
            ROLLBACK TRANSACTION;
            RAISERROR ('Address with the provided AddressId does not exist.', 16, 1);
            RETURN;
        END

        -- Retrieve the quantity from the CART table and corresponding BookId
        SELECT 
            @CartQuantity = c.Quantity,
            @BookId = c.BookId
        FROM 
            CART c
        WHERE 
            c.CartId = @CartId;

        -- Retrieve the available quantity from the BOOKS table
        SELECT 
            @BookQuantity = b.DiscountPrice  -- Assuming DiscountPrice represents available stock
        FROM 
            BOOKS b
        WHERE 
            b.BookId = @BookId;

        -- Check if the quantity in the cart exceeds the available quantity
        IF @CartQuantity > @BookQuantity
        BEGIN
            -- Rollback the transaction and raise an error if quantity is insufficient
            ROLLBACK TRANSACTION;
            RAISERROR ('Quantity in cart exceeds available stock in books table.', 16, 1);
            RETURN;
        END

        -- Insert order data into ORDERS table from CART and BOOKS tables
        INSERT INTO ORDERS (CartId, UserId, AddressId, Quantity, OriginalPrice, DiscountRate, DiscountPrice, TotalPrice, OrderPlacedDate)
        SELECT 
            c.CartId,
            @UserId,
            @AddressId,
            c.Quantity,
            b.OriginalPrice,
            b.DiscountRate,
            b.DiscountPrice,
            (c.Quantity * b.DiscountPrice) AS TotalPrice,
            GETDATE() AS OrderPlacedDate
        FROM 
            CART c
        INNER JOIN 
            BOOKS b ON c.BookId = b.BookId
        WHERE
            c.CartId = @CartId;

        -- Delete the specific cart record
        DELETE FROM CART
        WHERE CartId = @CartId;

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
