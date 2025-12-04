-- =============================================
-- Author:		Irakli totladze
-- Create date: 02.12.2025
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[spCheckIfUserExists]
    @Username NVARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT CASE 
               WHEN EXISTS (
                    SELECT 1 
                    FROM dbo.Users 
                    WHERE Username = @Username
               )
               THEN 1 
               ELSE 0 
           END AS ExistsFlag;
END
