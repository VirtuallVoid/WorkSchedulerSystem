-- =============================================
-- Author:		Irakli totladze
-- Create date: 02.12.2025
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[spApproveSchedule]
    @ScheduleId INT,
    @AdminId    INT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE dbo.Schedules
    SET 
        StatusId    = 2, -- Approved
        ApprovedBy  = @AdminId,
        ApprovedDate = GETDATE()
    WHERE Id = @ScheduleId;
END
