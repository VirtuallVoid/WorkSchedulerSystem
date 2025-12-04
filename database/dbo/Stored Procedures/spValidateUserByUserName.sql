-- =============================================
-- Author:		Irakli totladze
-- Create date: 02.12.2025
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[spValidateUserByUserName]
(
    @UserName NVARCHAR(100)
)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        u.Id,
        u.FullName,
        u.Username,
        u.PasswordHash,
        u.Id AS RoleId,
        r.Name AS RoleName
    FROM [dbo].[Users] u
    JOIN [dbo].[Roles] r ON u.RoleId = r.Id
    WHERE u.Username = @UserName;
END
