--Creating Stored Procedure to check login data is exists or not

CREATE PROC UserExists
(
    @EmailId VARCHAR(100),
    @Password VARCHAR(50)
)
AS
BEGIN
	BEGIN TRY
		SELECT UserId
		FROM SignUp
		WHERE EmailId = @EmailId AND Password = @Password;
	END TRY
	BEGIN CATCH
		SELECT ERROR_NUMBER() AS ErrorNumber,
				   ERROR_SEVERITY() AS ErrorSeverity,
				   ERROR_STATE() AS ErrorState;
	END CATCH
END
