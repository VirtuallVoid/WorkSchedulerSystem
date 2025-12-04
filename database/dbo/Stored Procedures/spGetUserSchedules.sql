-- =============================================
-- Author:		Irakli totladze
-- Create date: 02.12.2025
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[spGetUserSchedules]
    @UserId INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        s.Id,
        s.UserId,
        u.FullName AS UserFullName,
        s.JobId,
        j.Name as JobName,
        s.ShiftTypeId,
        sh.Name as shiftTypeName,
        s.StatusId,
        st.Name as StatusName,
        s.ShiftDate,
        s.RequestDate,
        s.ApprovedBy,
        au.FullName AS ApprovedByFullName,
        s.ApprovedDate
    FROM dbo.Schedules s
    INNER JOIN dbo.Users u   ON s.UserId = u.Id
    INNER JOIN dbo.Jobs j    ON s.JobId = j.Id
    INNER JOIN dbo.ShiftCatalog sh ON s.ShiftTypeId = sh.Id
    INNER JOIN dbo.StatusCatalog st ON s.StatusId = st.Id
    LEFT JOIN dbo.Users au ON s.ApprovedBy = au.Id
    WHERE s.UserId = @UserId
    ORDER BY s.ShiftDate DESC;
END
