-- =============================================
-- Author:		Irakli totladze
-- Create date: 02.12.2025
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[spGetUserInfoByUserId]
    @UserId INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        Id,
        UserName,
        FullName AS UserFullName,
        JobId
    FROM dbo.Users
    WHERE Id = @UserId
END
