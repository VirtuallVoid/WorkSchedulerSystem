CREATE TABLE [dbo].[Schedules] (
    [Id]           INT      IDENTITY (1, 1) NOT NULL,
    [UserId]       INT      NOT NULL,
    [JobId]        INT      NOT NULL,
    [StatusId]     INT      NOT NULL,
    [ShiftDate]    DATETIME NOT NULL,
    [ShiftTypeId]  INT      NOT NULL,
    [RequestDate]  DATETIME DEFAULT (getdate()) NOT NULL,
    [ApprovedBy]   INT      NULL,
    [ApprovedDate] DATETIME NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Schedules_Admin] FOREIGN KEY ([ApprovedBy]) REFERENCES [dbo].[Users] ([Id]),
    CONSTRAINT [FK_Schedules_Jobs] FOREIGN KEY ([JobId]) REFERENCES [dbo].[Jobs] ([Id]),
    CONSTRAINT [FK_Schedules_Shifts] FOREIGN KEY ([ShiftTypeId]) REFERENCES [dbo].[ShiftCatalog] ([Id]),
    CONSTRAINT [FK_Schedules_Status] FOREIGN KEY ([StatusId]) REFERENCES [dbo].[StatusCatalog] ([Id]),
    CONSTRAINT [FK_Schedules_Users] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([Id])
);

