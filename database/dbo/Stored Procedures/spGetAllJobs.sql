-- =============================================
-- Author:		Irakli totladze
-- Create date: 02.12.2025
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[spGetAllJobs]
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        Id AS Id,
        Name AS JobName
    FROM dbo.Jobs
    ORDER BY Id;
END
