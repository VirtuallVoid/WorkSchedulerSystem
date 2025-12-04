CREATE TABLE [dbo].[Users] (
    [Id]           INT            IDENTITY (1, 1) NOT NULL,
    [Username]     NVARCHAR (50)  NOT NULL,
    [FullName]     NVARCHAR (100) NULL,
    [PasswordHash] NVARCHAR (200) NOT NULL,
    [RoleId]       INT            NOT NULL,
    [JobId]        INT            NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Users_Jobs] FOREIGN KEY ([JobId]) REFERENCES [dbo].[Jobs] ([Id]),
    CONSTRAINT [FK_Users_Roles] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[Roles] ([Id])
);

