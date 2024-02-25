IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Permissions] (
    [Id] int NOT NULL IDENTITY,
    [Guid] uniqueidentifier NOT NULL,
    [LastUpdated] datetime2 NOT NULL,
    [PermissionsEmployeeId] int NULL,
    CONSTRAINT [PK_Permissions] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Permissions_Permissions_PermissionsEmployeeId] FOREIGN KEY ([PermissionsEmployeeId]) REFERENCES [Permissions] ([Id])
);
GO

CREATE TABLE [PermissionsTypes] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NULL,
    [CreatedDate] datetime2 NOT NULL,
    CONSTRAINT [PK_PermissionsTypes] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [WorkAreas] (
    [Id] int NOT NULL IDENTITY,
    [AreaName] nvarchar(max) NULL,
    CONSTRAINT [PK_WorkAreas] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [PermissionTypePermissionsEmployee] (
    [PermissionTypesId] int NOT NULL,
    [PermisssionsEmployeesId] int NOT NULL,
    CONSTRAINT [PK_PermissionTypePermissionsEmployee] PRIMARY KEY ([PermissionTypesId], [PermisssionsEmployeesId]),
    CONSTRAINT [FK_PermissionTypePermissionsEmployee_Permissions_PermisssionsEmployeesId] FOREIGN KEY ([PermisssionsEmployeesId]) REFERENCES [Permissions] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_PermissionTypePermissionsEmployee_PermissionsTypes_PermissionTypesId] FOREIGN KEY ([PermissionTypesId]) REFERENCES [PermissionsTypes] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Employees] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NULL,
    [LastName] nvarchar(max) NULL,
    [LastUpdated] datetime2 NOT NULL,
    [WorkAreaId] int NOT NULL,
    [PermissionEmployeesId] int NULL,
    [PermissionTypeId] int NULL,
    CONSTRAINT [PK_Employees] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Employees_Permissions_PermissionEmployeesId] FOREIGN KEY ([PermissionEmployeesId]) REFERENCES [Permissions] ([Id]),
    CONSTRAINT [FK_Employees_PermissionsTypes_PermissionTypeId] FOREIGN KEY ([PermissionTypeId]) REFERENCES [PermissionsTypes] ([Id]),
    CONSTRAINT [FK_Employees_WorkAreas_WorkAreaId] FOREIGN KEY ([WorkAreaId]) REFERENCES [WorkAreas] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_Employees_PermissionEmployeesId] ON [Employees] ([PermissionEmployeesId]);
GO

CREATE INDEX [IX_Employees_PermissionTypeId] ON [Employees] ([PermissionTypeId]);
GO

CREATE UNIQUE INDEX [IX_Employees_WorkAreaId] ON [Employees] ([WorkAreaId]);
GO

CREATE INDEX [IX_Permissions_PermissionsEmployeeId] ON [Permissions] ([PermissionsEmployeeId]);
GO

CREATE INDEX [IX_PermissionTypePermissionsEmployee_PermisssionsEmployeesId] ON [PermissionTypePermissionsEmployee] ([PermisssionsEmployeesId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240225010612_Initial-Migration', N'6.0.14');
GO

COMMIT;
GO

