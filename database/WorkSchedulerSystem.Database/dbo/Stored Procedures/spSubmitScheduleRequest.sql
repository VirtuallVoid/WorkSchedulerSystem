-- =============================================
-- Author:		Irakli totladze
-- Create date: 02.12.2025
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[spSubmitScheduleRequest]
    @UserId        INT,
    @JobId         INT,
    @ShiftTypeId   INT,
    @ShiftDate     DATETIME

AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO dbo.Schedules
    (
        UserId,
        JobId,
        ShiftTypeId,
        StatusId,
        ShiftDate,
        RequestDate
    )
    VALUES
    (
        @UserId,
        @JobId,
        @ShiftTypeId,
        1,
        @ShiftDate,
        GETDATE()
    );

	SELECT SCOPE_IDENTITY();
END
