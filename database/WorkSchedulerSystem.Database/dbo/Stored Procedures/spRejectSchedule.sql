-- =============================================
-- Author:		Irakli totladze
-- Create date: 02.12.2025
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[spRejectSchedule]
    @ScheduleId INT,
    @AdminId    INT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE dbo.Schedules
    SET 
        StatusId    = 3, -- Rejected
        ApprovedBy  = @AdminId,
        ApprovedDate = GETDATE()
    WHERE Id = @ScheduleId;
END
