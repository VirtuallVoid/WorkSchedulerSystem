-- =============================================
-- Author:		Irakli totladze
-- Create date: 02.12.2025
-- Description:	
-- exec [dbo].[spGetAllShifts]
-- =============================================
CREATE PROCEDURE [dbo].[spGetAllShifts]
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        Id AS Id,
        Name AS ShiftType
    FROM dbo.ShiftCatalog
    ORDER BY Id;
END
