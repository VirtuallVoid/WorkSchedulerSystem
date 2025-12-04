-- =============================================
-- Author:		Irakli totladze
-- Create date: 02.12.2025
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[spCreateUser]
    @FullName      NVARCHAR(200),
    @Username      NVARCHAR(100),
    @PasswordHash  NVARCHAR(300),
    @RoleId        INT,
	@JobId		   INT
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO dbo.Users
    (
        FullName,
        Username,
        PasswordHash,
        RoleId,
		JobId
    )
    VALUES
    (
        @FullName,
        @Username,
        @PasswordHash,
        @RoleId,
		@JobId
    );

    SELECT SCOPE_IDENTITY() AS NewUserId;
END
